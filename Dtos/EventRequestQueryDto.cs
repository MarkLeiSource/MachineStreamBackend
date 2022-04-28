
namespace MachineStreamBackend.Dtos
{
    public class EventRequestQueryDto
    {
        public string? from { get; set; }
        public string? to { get; set; }
        public string? machine_id { get; set; }
        public int? page_index { get; set; }
        public int? page_size { get; set; }
    }
}
