using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Controllers;
using webapiG2T.Models;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class SlaService : ISLaService
    {

        private readonly DataContext _context;

        public SlaService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Sla>> GetAllSla()
        {

            return await _context.Priorite.ToListAsync();

        }

        public async Task<Sla> GetSlaById(int id)
        {
            return await _context.Priorite.FindAsync(id);
        }

        public async Task<Sla> CreateSlaAsync(Sla newSla)
        {
            _context.Priorite.Add(newSla);
            await _context.SaveChangesAsync();
            return newSla;
        }

        public async Task UpdateSlaAsync(Sla updatedSla)
        {
            _context.Entry(updatedSla).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSlaAsync(int id)
        {
            var sla = await _context.Priorite.FindAsync(id);
            if (sla != null)
            {
                _context.Priorite.Remove(sla);
                await _context.SaveChangesAsync();
            }
        }
    }
}
