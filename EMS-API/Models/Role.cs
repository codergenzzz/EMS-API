namespace EMS_API.Models
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
