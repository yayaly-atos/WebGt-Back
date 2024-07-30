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

        public async Task<Canal> CreateCanalAsync(Canal newCanal)
        {
            _context.Canaux.Add(newCanal);
            await _context.SaveChangesAsync();
            return newCanal;
        }

        public async Task<bool> UpdateCanalAsync(Canal updatedCanal)
        {
            var canal = await _context.Canaux.FindAsync(updatedCanal.Id);
            if (canal == null)
            {
                return false;
            }

            _context.Entry(canal).CurrentValues.SetValues(updatedCanal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
