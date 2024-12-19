using EMS_API.Models;

namespace EMS_API.Services
{
    public interface IProfileService
    {
        public Profile? GetProfileById(Guid id);
        public Profile? GetProfileByEmail(string email);
        public Profile? GetProfileByName(string name);
        public ICollection<Profile> GetProfiles();
        public Profile Insert(Profile profile);
        public Profile Update(Profile profile);
        public void Delete(Guid profileId);
    }
}
