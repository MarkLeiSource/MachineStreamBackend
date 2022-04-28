namespace MachineStreamBackend.Dtos
{
    public class EventMessageDto
    {
        public string Topic { get; set; }
        public EventDto Payload { get; set; }
        public string Ref { get; set; }
        public string Event { get; set; }
    }
}
