using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class EntiteSupportService : IEntiteSupport
    {
        private readonly DataContext _context;
        public EntiteSupportService(DataContext context) {
            _context = context;
        }

        public async Task<List<EntiteSupport>> GetAllEntitesAsync()
        {
            return await _context.EntitesSupports.ToListAsync();
        }

    }
}
