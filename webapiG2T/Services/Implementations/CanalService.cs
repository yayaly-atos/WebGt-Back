using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class CanalService : ICanalService
    {
        private readonly DataContext _context;

        public CanalService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetCanalNomByIdAsync(int id)
        {
            var canal = await _context.Canaux.FindAsync(id);
            return canal?.Nom;
        }
        public async Task<List<Canal>> GetAllCanauxAsync()
        {
            return await _context.Canaux.ToListAsync();
        }
    }
}
