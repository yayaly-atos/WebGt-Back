using G2T.Data;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Models;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class PrestataireService : IprestataireService
    {
        private readonly DataContext _context;

        public PrestataireService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Prestataire>> GetAllPrestatairesAsync()
        {
            return await _context.Prestataires.ToListAsync();
        }

        public async Task<Prestataire> GetPrestataireByIdAsync(int id)
        {
            return await _context.Prestataires.FindAsync(id);
        }

        public async Task<Prestataire> CreatePrestataireAsync(Prestataire newPrestataire)
        {
            _context.Prestataires.Add(newPrestataire);
            await _context.SaveChangesAsync();
            return newPrestataire;
        }

        public async Task UpdatePrestataireAsync(Prestataire updatedPrestataire)
        {
            _context.Entry(updatedPrestataire).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePrestataireAsync(int id)
        {
            var prestataire = await _context.Prestataires.FindAsync(id);
            if (prestataire != null)
            {
                _context.Prestataires.Remove(prestataire);
                await _context.SaveChangesAsync();
            }
        }
    }
}
