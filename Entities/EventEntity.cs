using System.ComponentModel.DataAnnotations;

namespace MachineStreamBackend.Entities
{
    public class EventEntity
    {
        [MaxLength(50), Required]
        public string Machine_id { get; set; }

        [MaxLength(50), Required]
        public string Id { get; set; }

        [MaxLength(20), Required]
        public string Status { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
