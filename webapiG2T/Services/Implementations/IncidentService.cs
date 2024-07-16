using G2T.Data;
using G2T.Models;
using G2T.Models.enums;
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
                .Include(i => i.Entite)
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
                .Include(i => i.Entite)
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
                  .Include(i => i.Entite)
                  .Where(i => i.Contact.Telephone == phoneNumber)
                  .Select(i => MapToIncidentDto(i))
                  .ToListAsync();

            return incidents;
        }
        public async Task<IncidentDto> GetIncidentByIDAsync(int incidentId)
        {
            var incident = await _context.Incidents
                   .Include(i => i.Contact)
                   .ThenInclude(c => c.Compte)
                   .Include(i => i.Canal)
                   .Include(i => i.Motif)
                   .Include(i => i.SousMotif)
                   .Include(i => i.Entite)
                   .Where(i => i.Id == incidentId)
                   .Select(i => MapToIncidentDto(i))
                   .FirstOrDefaultAsync();

            return incident;

        }
        public async Task<IncidentDto> CreateIncidentAsync(IncidentDto incidentDto)
        {
            var incident = await MapToIncident(incidentDto);
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();


            return MapToIncidentDto(incident);
        }

        public async Task<String> UpdateIncidentStatusAsync(int id, string StatutIncident)
        {
            try
            {
                var existingIncident = await _context.Incidents
                    .Include(i => i.Contact)
                    .ThenInclude(c => c.Compte)
                    .Include(i => i.Canal)
                    .Include(i => i.Motif)
                    .Include(i => i.SousMotif)
                    .Include(i => i.Entite)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (existingIncident == null)
                {
                    return null;
                }
             
                existingIncident.StatutIncident = StatutIncident;         
                await _context.SaveChangesAsync();
                return $"Le statut de l'incident numéro {id} a été changé en {StatutIncident} avec succès.";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the incident.", ex);
            }
        }
        public async Task<String> UpdateIncidentCommentAsync(int id, string commentaire)
        {
            try
            {
                var existingIncident = await _context.Incidents
                    .Include(i => i.Contact)
                    .ThenInclude(c => c.Compte)
                    .Include(i => i.Canal)
                    .Include(i => i.Motif)
                    .Include(i => i.SousMotif)
                    .Include(i => i.Entite)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (existingIncident == null)
                {
                    return null;
                }

                existingIncident.Commentaire = commentaire;
                await _context.SaveChangesAsync();
                return $"Le commentaire de l'incident numéro {id} a été changé  avec succès.";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the incident.", ex);
            }
        }
        public async Task<IncidentDto> UpdateIncidenEscaladetAsync(int id)
        {
            var existingIncident = await _context.Incidents
              .Include(i => i.Contact)
              .ThenInclude(c => c.Compte)
              .Include(i => i.Canal)
              .Include(i => i.Motif)
              .Include(i => i.SousMotif)
              .Include(i => i.Entite)
              .FirstOrDefaultAsync(i => i.Id == id);
            if (existingIncident == null)
            {
                return null;
            }
            int newEntiteEnChargeId = existingIncident.Entite.Id + 1;

          
            var newEntiteEnCharge = await _context.Entite.FindAsync(newEntiteEnChargeId);
            if (newEntiteEnCharge == null)
            {
               
                return null;
            }

            existingIncident.Entite= newEntiteEnCharge;
            await _context.SaveChangesAsync();

            return MapToIncidentDto(existingIncident);

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
                Entite = i.Entite.NomEntite,
 
                Contact = new ContactDto
                {
                    Id = i.Contact.Id,
                    Nom = i.Contact.Nom,
                    Prenom = i.Contact.Prenom,
                    Telephone = i.Contact.Telephone,
                    Adresse = i.Contact.Adresse,
                    StatutContact = i.Contact.StatutContact,
                    CompteId = i.Contact.Compte.Id  
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
                Entite = await _context.Entite.FirstOrDefaultAsync(e => e.NomEntite == dto.Entite),
                
                Contact = await MapToContact(dto.Contact)
            };
        }
        private async Task<Contact> MapToContact(ContactDto dto)
        {
            var existingContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Telephone == dto.Telephone);
            if (existingContact != null)
            {
                
                   var existingCompte = await _context.Comptes.FirstOrDefaultAsync(c => c.Id == dto.CompteId);
                    if (existingCompte != null)
                    {
                        existingContact.Compte = existingCompte;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Le compte avec l'ID {dto.CompteId} n'existe pas.");
                    }
                
                return existingContact;
            }    
            return new Contact
            {
                Id = dto.Id,
                Nom = dto.Nom,
                Prenom = dto.Prenom,
                Telephone = dto.Telephone,
                Adresse = dto.Adresse,
                StatutContact = dto.StatutContact,     
                Compte = await _context.Comptes.FirstOrDefaultAsync(c => c.Id == dto.CompteId) 
            };
        }
    }
}
