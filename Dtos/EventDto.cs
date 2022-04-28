namespace MachineStreamBackend.Dtos
{
    public class EventDto
    {
        public string Machine_id { get; set; }
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
    }
}
