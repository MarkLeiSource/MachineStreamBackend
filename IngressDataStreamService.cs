using MachineStreamBackend.Dtos;
using MachineStreamBackend.Services;
using MachineStreamBackend.Utils;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;

namespace MachineStreamBackend
{
    public class IngressDataStreamService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly string _url;
        private IEventService EventService { get; set; }
        public IngressDataStreamService(ILogger<IngressDataStreamService> logger, IConfiguration configuration, IEventService eventService)
        {
            _logger = logger;
            _url = configuration["ConnectionStrings:MessageService"];
            EventService = eventService;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("IngressDataStreamService starting");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var client = new ClientWebSocket();
                    client.ConnectAsync(new Uri(_url), stoppingToken).GetAwaiter().GetResult();
                    List<byte> bs = new List<byte>();
                    var buffer = new byte[1024 * 4];
                    WebSocketReceiveResult result = client.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken).GetAwaiter().GetResult();
                    while (!result.CloseStatus.HasValue)
                    {
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            bs.AddRange(buffer.Take(result.Count));

                            if (result.EndOfMessage)
                            {
                                string eventMsg = Encoding.UTF8.GetString(bs.ToArray(), 0, bs.Count);
                                if (!string.IsNullOrEmpty(eventMsg))
                                {
                                    var messageDto = JsonConvert.DeserializeObject<EventMessageDto>(eventMsg);
                                    if (messageDto != null && messageDto.Payload != null)
                                    {
                                        if (EventStatusValidateUtil.IsStatusValid(messageDto.Payload.Status))
                                        {
                                            EventService.SaveEvent(messageDto.Payload);
                                        }
                                        else
                                        {
                                            _logger.LogWarning("Event with unknown status recieved.");
                                        }
                                    }
                                }
                                _logger.LogDebug($"Recieved message: {eventMsg}");
                                bs = new List<byte>();
                            }
                        }
                        result = client.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken).GetAwaiter().GetResult();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("An error occurred while making websocket request.",ex);
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                }
            }
            _logger.LogInformation("IngressDataStreamService stopping");
            return Task.CompletedTask;
        }
    }
}
