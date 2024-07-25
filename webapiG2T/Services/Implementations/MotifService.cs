using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class MotifService : IMotifService
    {
        private readonly DataContext _context;

        public MotifService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetMotifNomByIdAsync(int id)
        {
            var motif = await _context.Motifs.FindAsync(id);
            return motif?.Nom;
        }
        public async Task<List<Motif>> GetAllMotifsAsync()
        {
            return await _context.Motifs.ToListAsync();
        }
    }
}
