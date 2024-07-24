using G2T.Data;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Models;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class RevoquerTokenService //: IRevoquerTokenService
    {
        private readonly DataContext _context;

    //    public RevoquerTokenService(DataContext context)
    //    {
    //        _context = context;
    //    }

    //    public async Task RevoquerTokenAsync(string token)
    //    {
    //        var revoquerToken = new RevoquerToken { Token = token, DateRevoquer = DateTime.UtcNow };
    //        _context.RevoquerTokens.Add(revoquerToken);
    //        await _context.SaveChangesAsync();
    //    }

    //    public async Task<bool> EstRevoquerTokenAsync(string token)
    //    {
    //        return await _context.RevoquerTokens.AnyAsync(rt => rt.Token == token);
    //    }
    }
}
