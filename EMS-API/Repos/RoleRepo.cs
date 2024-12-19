using EMS_API.Data;
using EMS_API.Models;

namespace EMS_API.Repos
{
    public class RoleRepo
    {
        private readonly EMSDbContext _context;

        public RoleRepo(EMSDbContext context)
        {
            _context = context;
        }

        public Role? GetRoleById(Guid id)
        {
            return _context.Roles.Find(id);
        }
        public Role? GetRoleByName(string name)
        {
            return _context.Roles.FirstOrDefault(r => r.Name == name);
        }

        public ICollection<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
