using System.ComponentModel.DataAnnotations;

namespace EMS_API.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public bool Status { get; set; } // 1-on, 0-off
        [Required]
        public string MAC { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;    // kitchen, living room, bedroom, etc.

        public ICollection<ProfileDevice> ProfileDevices { get; set; } = new List<ProfileDevice>();
        // public ICollection<Log> Logs { get; set; } = new List<Log>();

    }
}
