using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class ProfileDevice
    {
        [ForeignKey("Profile")]
        public Guid ProfileID { get; set; }
        public Profile Profile { get; set; } = null!;

        [ForeignKey("Device")]
        public Guid DeviceID { get; set; }
        public Device Device { get; set; } = null!;
    }
}
