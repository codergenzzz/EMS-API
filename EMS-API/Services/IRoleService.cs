using EMS_API.Models;

namespace EMS_API.Services
{
    public interface IRoleService
    {
        public Role GetRoleById(Guid id);
        public Role GetRoleByName(string name);

        public ICollection<Role> GetRoles();
    }
}
