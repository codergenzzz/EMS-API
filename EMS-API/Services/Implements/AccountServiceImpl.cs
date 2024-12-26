using EMS_API.Dtos.Request;
using EMS_API.Repos;

namespace EMS_API.Services.Implements
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly AccountRepo _accountRepo;

        public AccountServiceImpl(AccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public bool ResetPassword(ResetPasswordRequest request)
        {
            var account = _accountRepo.GetAccountByUsername(request.Username);

            if (account == null)
            {
                return false;
            }

            account.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            return _accountRepo.Update(account);
        }
    }
}
