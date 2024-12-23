using EMS_API.Data;
using EMS_API.Models;

namespace EMS_API.Repos
{
    public class AccountRepo
    {
        private readonly EMSDbContext _context;
        public AccountRepo(EMSDbContext context)
        {
            _context = context;
        }
        public bool Insert(Account account)
        {
            _context.Accounts.Add(account);
            return Save();
        }
        public bool Update(Account account)
        {
            _context.Accounts.Update(account);
            return Save();
        }
        public bool Delete(Account account)
        {
            _context.Accounts.Remove(account);
            return Save();
        }
        public Account? GetAccountById(Guid id)
        {
            return _context.Accounts.Find(id);
        }
        public Account? GetAccountByUsername(string username)
        {
            return _context.Accounts.FirstOrDefault(a => a.Username == username);
        }
        public List<Account> GetAccountByRole(string roleName)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                return new List<Account>();
            }
            Guid roleId = role.Id;
            return _context.Accounts.Where(a => a.RoleId == roleId).ToList();
        }
        public List<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
