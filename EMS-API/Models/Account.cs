using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public Profile Profile { get; set; } = null!;
        public RefreshToken Token { get; set; } = null!;

    }
}
