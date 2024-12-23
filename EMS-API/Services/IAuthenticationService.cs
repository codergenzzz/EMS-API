using EMS_API.Dtos.Request;
using EMS_API.Dtos.Response;

namespace EMS_API.Services
{
    public interface IAuthenticationService
    {
        public AuthenticationResponse Login(LoginRequest request);
        public bool Register(RegisterRequest request);
    }
}
