using MachineStreamBackend.Dtos;
using MachineStreamBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MachineStreamBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineEventController : ControllerBase
    {
        private readonly ILogger<MachineEventController> _logger;
        private IEventService EventService { get; set; }
        public MachineEventController(ILogger<MachineEventController> logger, IEventService eventService)
        {
            _logger = logger;
            EventService = eventService;
        }

        [HttpGet(Name = "GetEvent")]
        public IEnumerable<EventDto> Get([FromQuery] EventRequestQueryDto queryDto)
        {
            DateTime fromTime;
            DateTime toTime;
            if (!DateTime.TryParse(queryDto.from, out fromTime))
            {
                _logger.LogWarning($"Unknown from time pattern: {queryDto.from}");
                fromTime = DateTime.UtcNow.Date;
            }
            if (!DateTime.TryParse(queryDto.to, out toTime))
            {
                _logger.LogWarning($"Unknown to time pattern: {queryDto.to}");
                toTime = DateTime.MaxValue;
            }

            return EventService.GetEvents(fromTime, toTime, queryDto.machine_id, queryDto.page_index, queryDto.page_size);
        }
    }
}
