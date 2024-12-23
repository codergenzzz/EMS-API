using System.ComponentModel.DataAnnotations.Schema;

namespace EMS_API.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string JwtId { get; set; } = string.Empty;
        public string IsUsed { get; set; } = string.Empty;
        public string IsRevoked { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; } = null!;
    }
}
