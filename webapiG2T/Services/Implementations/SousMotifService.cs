using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class SousMotifService : ISousMotifService
    {
        private readonly DataContext _context;

        public SousMotifService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetSousMotifNomByIdAsync(int id)
        {
            var sousMotif = await _context.SousMotifs.FindAsync(id);
            return sousMotif?.Nom;
        }
        public async Task<List<SousMotif>> GetAllSousMotifsAsync()
        {
            return await _context.SousMotifs.ToListAsync();
        }
    }
}
