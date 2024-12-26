using EMS_API.Dtos.Request;

namespace EMS_API.Services
{
    public interface IAccountService
    {
        public bool ResetPassword(ResetPasswordRequest request);
    }
}
