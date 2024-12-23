using EMS_API.Data;
using EMS_API.Models;

namespace EMS_API.Repos
{
    public class TokenRepo
    {
        private readonly EMSDbContext _context;

        public TokenRepo(EMSDbContext context)
        {
            _context = context;
        }

        public bool Insert(RefreshToken token)
        {
            token.JwtId = Guid.NewGuid().ToString();
            _context.Tokens.Add(token);
            return Save();
        }

        public bool Delete(RefreshToken token)
        {
            _context.Tokens.Remove(token);
            return Save();
        }

        public RefreshToken? GetByAccountId(Guid accountId)
        {
            return _context.Tokens.FirstOrDefault(t => t.AccountId == accountId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
