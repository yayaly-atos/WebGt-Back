using G2T.Data;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Models;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class RevoquerTokenService : IRevoquerTokenService
    {
        private readonly DataContext _context;

        public RevoquerTokenService(DataContext context)
        {
            _context = context;
        }

        public async Task RevoquerTokenAsync(string token)
        {
            var t = _context.RevoquerTokens.FirstOrDefault(t => t.Token == token);
            if(t != null)
            {
                t.DateRevoquer = DateTime.UtcNow;
                t.IsRevoquer = true;
                _context.RevoquerTokens.Update(t);
                var response = await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> EstRevoquerTokenAsync(string token)
        {
            return await _context.RevoquerTokens.AnyAsync(rt => rt.Token == token);
        }

        public async Task AddToken(string id, string token, DateTime expire)
        {
            var t = new RevoquerToken { Id = id, Token = token, DateRevoquer = expire };
            _context.RevoquerTokens.Add(t);
            await _context.SaveChangesAsync();
        }
    }
}
