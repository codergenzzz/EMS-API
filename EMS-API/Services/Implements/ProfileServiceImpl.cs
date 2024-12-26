using EMS_API.Models;
using EMS_API.Repos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EMS_API.Services.Implements
{
    public class ProfileServiceImpl : IProfileService
    {
        private readonly ProfileRepo _profileRepo;
        private readonly IConfiguration _configuration;
        private readonly AccountRepo _accountRepo;
        public ProfileServiceImpl(ProfileRepo profileRepo, IConfiguration configuration, AccountRepo accountRepo)
        {
            _profileRepo = profileRepo;
            _configuration = configuration;
            _accountRepo = accountRepo;
        }

        public string ChangePassword(string token, string oldPassword, string newPassword)
        {
            try
            {
                var secretKey = "adbaddd979cf3052c2f7f3c2d5a423a4bcbc6a7adc50cbb9a2206f2d7b474c68";
                var key = Encoding.UTF8.GetBytes(secretKey);

                // Validate and decode the token
                var handler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!))
                };

                var principal = handler.ValidateToken(token, validationParameters, out var validatedToken);

                // Extract user information from the token (e.g., userId or username)
                var jwtToken = validatedToken as JwtSecurityToken;
                var username = jwtToken?.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

                // Verify old password and update to new password
                if (username != null && VerifyOldPassword(username, oldPassword)) // Implement VerifyOldPassword
                {
                    UpdatePassword(username, newPassword); // Implement UpdatePassword
                    return "Password changed successfully";
                }

                return "Invalid old password";
            }
            catch (SecurityTokenException)
            {
                return "Invalid token";
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        public void UpdatePassword(string username, string newPassword)
        {
            var account = _accountRepo.GetAccountByUsername(username);
            if (account == null)
            {
                throw new ArgumentException("Account not found");
            }

            // Hash the new password before storing it
            account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _accountRepo.Update(account);
            _accountRepo.Save();
        }

        public bool VerifyOldPassword(string username, string oldPassword)
        {
            var account = _accountRepo.GetAccountByUsername(username);
            if (account == null)
            {
                return false;
            }

            // Assuming passwords are stored as hashed values
            return BCrypt.Net.BCrypt.Verify(oldPassword, account.Password);
        }

        public bool Delete(Guid profileId)
        {
            var profile = GetProfileById(profileId);
            if (profile != null)
            {
                _profileRepo.Delete(profile);
                _profileRepo.Save();
                return true;
            }

            return false;
        }

        public Profile? GetProfileByEmail(string email)
        {
            return _profileRepo.GetProfileByEmail(email);
        }

        public Profile? GetProfileById(Guid id)
        {
            return _profileRepo.GetProfileById(id);
        }

        public Profile? GetProfileByName(string name)
        {
            return _profileRepo.GetProfileByName(name);
        }

        public ICollection<Profile> GetProfiles()
        {
            return _profileRepo.GetProfiles();
        }

        public Profile Insert(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }
            _profileRepo.Insert(profile);
            _profileRepo.Save();
            return profile;
        }

        public Profile Update(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
            }
            _profileRepo.Update(profile);
            _profileRepo.Save();
            return profile;
        }

        void IProfileService.Delete(Guid profileId)
        {
            throw new NotImplementedException();
        }
    }
}
