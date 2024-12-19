using EMS_API.Models;
using EMS_API.Repos;

namespace EMS_API.Services.Implements
{
    public class RoleServiceImpl : IRoleService
    {
        private readonly RoleRepo _roleRepo;

        public RoleServiceImpl(RoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public Role GetRoleById(Guid id)
        {
            var role = _roleRepo.GetRoleById(id);
            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }
            return role;
        }

        public Role GetRoleByName(string name)
        {
            var role = _roleRepo.GetRoleByName(name);
            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }
            return role;
        }

        public ICollection<Role> GetRoles()
        {
            return _roleRepo.GetRoles();
        }
    }
}
