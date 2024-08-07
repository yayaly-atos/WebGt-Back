using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Models.Dto;
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
        public async Task<List<SousMotifDtoReturn>> GetAllSousMotifsAsync()
        {
            var sousMotifs = await _context.SousMotifs
       .Include(sm => sm.Motif) 
       .ToListAsync();
            var sousMotifsDto = sousMotifs.Select(sm => new SousMotifDtoReturn
            {
                SousMotifId= sm.Id,
                MotifId = sm.Motif.Id,
                Nom = sm.Nom,
                MotifNom = sm.Motif.Nom 
            }).ToList();

            return sousMotifsDto;
        }

        public async Task<SousMotif> CreateSousMotifAsync(SousMotifDto newSousMotif)
        {
            var sousMotif = new SousMotif
            {
                Nom = newSousMotif.Nom,
                MotifId = newSousMotif.MotifId
            };

            _context.SousMotifs.Add(sousMotif);
            await _context.SaveChangesAsync();

            return sousMotif;
        }

        public async Task<bool> UpdateSousMotifAsync(SousMotifDto updatedSousMotif)
        {
            var sousMotif = await _context.SousMotifs
                                          .SingleOrDefaultAsync(sm => sm.Nom == updatedSousMotif.Nom);
            if (sousMotif == null)
            {
                return false;
            }

          
            _context.Entry(sousMotif).CurrentValues.SetValues(updatedSousMotif);

          
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
