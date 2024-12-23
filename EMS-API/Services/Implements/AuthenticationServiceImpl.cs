using EMS_API.Dtos.Request;
using EMS_API.Dtos.Response;
using EMS_API.Models;
using EMS_API.Repos;

namespace EMS_API.Services.Implements
{
    public class AuthenticationServiceImpl : IAuthenticationService
    {
        private readonly AccountRepo _accountRepo;
        private readonly RoleRepo _roleRepo;
        private readonly IJwtService _jwtService;
        public AuthenticationServiceImpl(AccountRepo accountRepo, RoleRepo roleRepo, IJwtService jwtService)
        {
            _accountRepo = accountRepo;
            _roleRepo = roleRepo;
            _jwtService = jwtService;
        }


        public AuthenticationResponse Login(LoginRequest request)
        {
            var user = _accountRepo.GetAccountByUsername(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return new AuthenticationResponse
                {
                    Token = null,
                    IsAuthenticated = false,
                    ExpiresIn = 0,
                    Errors = new List<string> { "Invalid username or password" }
                };
            }
            var role = _roleRepo.GetRoleById(user.RoleId).Name;

            var token = _jwtService.GenerateToken(user, role);

            return new AuthenticationResponse
            {
                Token = token,
                IsAuthenticated = true,
                ExpiresIn = 3600
            };
        }

        public bool Register(RegisterRequest request)
        {
            var user = _accountRepo.GetAccountByUsername(request.Username);

            if (user != null)
            {
                return false;
            }

            //if (request.Role == "Admin")
            //{
            //    return false;
            //}

            var role = _roleRepo.GetRoleByName(request.Role);
            if (role == null)
            {
                throw new Exception("Role not found"); // Hoặc xử lý phù hợp
            }

            var newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = role.Id
            };

            var result = _accountRepo.Insert(newAccount);

            if (!result)
            {
                return false;
            }

            return true;
        }
    }
}
