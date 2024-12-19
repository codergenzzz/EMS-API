using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class Log
    {
        public Guid LogId { get; set; }
        [Required]
        public string Message { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Device")]
        public Guid DeviceId { get; set; }
        public Device Device { get; set; } = null!;

    }
}
