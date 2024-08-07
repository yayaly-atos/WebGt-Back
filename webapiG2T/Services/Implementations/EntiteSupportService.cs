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

        public async Task<EntiteSupport> GetEntiteByIdAsync(int id)
        {
            return await _context.EntitesSupports.FindAsync(id);
        }

        public async Task<EntiteSupport> CreateEntiteAsync(EntiteSupport newEntite)
        {
            _context.EntitesSupports.Add(newEntite);
            await _context.SaveChangesAsync();
            return newEntite;
        }

        public async Task UpdateEntiteAsync(EntiteSupport updatedEntite)
        {
            _context.Entry(updatedEntite).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntiteAsync(int id)
        {
            var entite = await _context.EntitesSupports.FindAsync(id);
            if (entite != null)
            {
                _context.EntitesSupports.Remove(entite);
                await _context.SaveChangesAsync();
            }
        }

    }
}
