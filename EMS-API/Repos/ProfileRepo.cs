using EMS_API.Data;
using EMS_API.Models;

namespace EMS_API.Repos
{
    public class ProfileRepo
    {
        private readonly EMSDbContext _context;
        public ProfileRepo(EMSDbContext context)
        {
            _context = context;
        }
        public bool Insert(Profile profile)
        {
            _context.Profiles.Add(profile);
            return Save();
        }
        public bool Update(Profile profile)
        {
            _context.Profiles.Update(profile);
            return Save();
        }
        public bool Delete(Profile profile)
        {
            _context.Profiles.Remove(profile);
            return Save();
        }
        public Profile? GetProfileById(Guid id)
        {
            return _context.Profiles.Find(id);
        }
        public Profile? GetProfileByEmail(string email)
        {
            return _context.Profiles.FirstOrDefault(p => p.Email == email);
        }
        public Profile? GetProfileByName(string name)
        {
            return _context.Profiles.FirstOrDefault(p => p.FirstName == name || p.LastName == name);
        }
        public List<Profile> GetProfiles()
        {
            return _context.Profiles.ToList();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
