using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Models.Dto;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class IncidentService : IIncidentService
    {
        private readonly DataContext _context;

        public IncidentService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Incident>> GetAllIncidentsAsync()
        {
            return await _context.Incidents
                 .Include(i => i.Canal)
                .Include(i => i.Motif)
                .Include(i => i.SousMotif)
                .Include(i => i.EntiteEnCharge)
                .Include(i => i.Contact)
                .ToListAsync();
        }

        public async Task<IncidentDto> GetIncidentByPhoneNumberAndIdAsync(string phoneNumber, int incidentId)
        {
            var incident = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.Canal)
                .Include(i => i.Motif)
                .Include(i => i.SousMotif)
                .Include(i => i.EntiteEnCharge)
                .Where(i => i.Id == incidentId && i.Contact.Telephone == phoneNumber)
                .Select(i => MapToIncidentDto(i))
                .FirstOrDefaultAsync();
            return incident;

        }
        public async Task<List<IncidentDto>> GetIncidentsByPhoneNumberAsync(string phoneNumber)
        {
            var incidents = await _context.Incidents
                  .Include(i => i.Contact)
                  .Include(i => i.Canal)
                  .Include(i => i.Motif)
                  .Include(i => i.SousMotif)
                  .Include(i => i.EntiteEnCharge)
                  .Where(i => i.Contact.Telephone == phoneNumber)
                  .Select(i => MapToIncidentDto(i))
                  .ToListAsync();

            return incidents;
        }
        public async Task<IncidentDto> GetIncidentByIDAsync(int incidentId)
        {
            var incident = await _context.Incidents
                   .Include(i => i.Contact)
                   .Include(i => i.Canal)
                   .Include(i => i.Motif)
                   .Include(i => i.SousMotif)
                   .Include(i => i.EntiteEnCharge)
                   .Where(i => i.Id == incidentId)
                   .Select(i => MapToIncidentDto(i))
                   .FirstOrDefaultAsync();

            return incident;

        }


        private static IncidentDto MapToIncidentDto(Incident i)
        {
            return new IncidentDto
            {
                Id = i.Id,
                Canal = i.Canal.Nom,
                Motif = i.Motif.Nom,
                SousMotif = i.SousMotif.Nom,
                Description = i.Description,
                Commentaire = i.Commentaire,
                StatutIncident = i.StatutIncident,
                EntiteEnCharge = i.EntiteEnCharge.NomEntiteEnCharge,
                Contact = new ContactDto
                {
                    Id = i.Contact.Id,
                    Nom = i.Contact.Nom,
                    Prenom = i.Contact.Prenom,
                    Telephone = i.Contact.Telephone,
                    Adresse = i.Contact.Adresse,
                    StatutContact = i.Contact.StatutContact
                }
            };
        }
        private async Task<Incident> MapToIncident(IncidentDto dto)
        {
            return new Incident
            {
                Id = dto.Id,
                Canal = await _context.Canaux.FirstOrDefaultAsync(c => c.Nom == dto.Canal),
                Motif = await _context.Motifs.FirstOrDefaultAsync(m => m.Nom == dto.Motif),
                SousMotif = await _context.SousMotifs.FirstOrDefaultAsync(sm => sm.Nom == dto.SousMotif),
                Description = dto.Description,
                Commentaire = dto.Commentaire,
                StatutIncident = dto.StatutIncident,
                EntiteEnCharge = await _context.EntiteEnCharges.FirstOrDefaultAsync(e => e.NomEntiteEnCharge == dto.EntiteEnCharge),
                Contact = await MapToContact(dto.Contact)
            };
        }
        private async Task<Contact> MapToContact(ContactDto dto)
        {

            var existingContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Telephone == dto.Telephone);
            if (existingContact != null)
            {
                return existingContact;
            }

            return new Contact
            {
                Id = dto.Id,
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                Telephone = dto.Telephone,
                Adresse = dto.Adresse,
                StatutContact = dto.StatutContact
            };
        }
    }
}
