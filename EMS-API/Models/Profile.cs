using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class Profile
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Length(10, 10)]
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateOnly Birthdate { get; set; }

        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;

        public ICollection<ProfileDevice> ProfileDevices { get; set; } = new List<ProfileDevice>();
    }
}
