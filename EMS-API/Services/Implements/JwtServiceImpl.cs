using EMS_API.Models;
using EMS_API.Repos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EMS_API.Services.Implements
{
    public class JwtServiceImpl : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly TokenRepo _tokenRepo;

        public JwtServiceImpl(IConfiguration configuration, TokenRepo tokenRepo)
        {
            _configuration = configuration;
            _tokenRepo = tokenRepo;
        }
        public string GenerateToken(Account account, string role)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", account.Id.ToString()),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.Name, account.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var existingToken = _tokenRepo.GetByAccountId(account.Id);
            if (existingToken != null)
            {
                // Xóa token cũ
                _tokenRepo.Delete(existingToken);
            }

            // Tạo và chèn token mới
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                AccountId = account.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMonths(1),
                IsUsed = "false",
                IsRevoked = "false",
                Token = RandomString(35) + Guid.NewGuid()
            };

            _tokenRepo.Insert(refreshToken);



            return jwtToken;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
