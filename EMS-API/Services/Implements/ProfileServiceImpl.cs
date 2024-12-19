using EMS_API.Models;
using EMS_API.Repos;

namespace EMS_API.Services.Implements
{
    public class ProfileServiceImpl : IProfileService
    {
        private readonly ProfileRepo _profileRepo;

        public ProfileServiceImpl(ProfileRepo profileRepo)
        {
            _profileRepo = profileRepo;
        }

        public void Delete(Guid profileId)
        {
            var profile = GetProfileById(profileId);
            if (profile != null)
            {
                _profileRepo.Delete(profile);
                _profileRepo.Save();
            }
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
    }
}
