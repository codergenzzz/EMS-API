using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Profile Profile { get; set; } = null!;
    }
}
