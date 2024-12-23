using EMS_API.Models;

namespace EMS_API.Services
{
    public interface IJwtService
    {
        public string GenerateToken(Account account, string role);
    }
}
