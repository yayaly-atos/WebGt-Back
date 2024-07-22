using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapiG2T.Models;
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

        public async Task<IncidentDto> GetIncidentByIDAsync(int incidentId)
        {
            var incident = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.Canal)
                .Include(i => i.Motif)
                .Include(i => i.SousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Where(i => i.Id == incidentId)
                .FirstOrDefaultAsync();

            return MapToIncidentDto(incident);
        }

        public async Task<IncidentDto> GetIncidentByPhoneNumberAndIdAsync(string phoneNumber, int incidentId)
        {
            var incident = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.Canal)
                .Include(i => i.Motif)
                .Include(i => i.SousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Where(i => i.Id == incidentId && i.Contact.Telephone == phoneNumber)
                .FirstOrDefaultAsync();

            return MapToIncidentDto(incident);
        }

        public async Task<List<IncidentDto>> GetIncidentsByPhoneNumberAsync(string phoneNumber)
        {
            var incidents = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.Canal)
                .Include(i => i.Motif)
                .Include(i => i.SousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Where(i => i.Contact.Telephone == phoneNumber)
                .ToListAsync();

            return incidents.Select(i => MapToIncidentDto(i)).ToList();
        }
        public async Task<IncidentDto> CreateIncidentAsync(IncidentDto incidentDto)
        {
            var incident = await MapToIncident(incidentDto);
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();


            return MapToIncidentDto(incident);
        }

        private static IncidentDto MapToIncidentDto(Incident incident)
        {
            if (incident == null)
            {
                return null;
            }

            return new IncidentDto
            {
                Id = incident.Id,
                CanalId = incident.Canal.Id, 
                MotifId = incident.Motif.Id, 
                SousMotifId = incident.SousMotif.Id, 
                Description = incident.Description, 
                Commentaire = incident.Commentaire,
                StatutIncident = incident.StatutIncident, 
                ContactId = incident.Contact.Id, 
                ServiceId = incident.Service.Id, 
                TeleconseillerId = incident.Teleconseiller.Id 
            };
        }
        private  async Task<Incident> MapToIncident(IncidentDto dto)
        {
         

            return new Incident
            {

                Description = dto.Description,
                Commentaire = dto.Commentaire,
                StatutIncident = dto.StatutIncident,
                Contact = await _context.Contacts.FindAsync(dto.ContactId),
                Canal = await _context.Canaux.FindAsync(dto.CanalId),
                Motif = await _context.Motifs.FindAsync(dto.MotifId),
                SousMotif = await _context.SousMotifs.FindAsync(dto.SousMotifId),
                Service = await _context.Services.FindAsync(dto.ServiceId),
                Teleconseiller = await _context.Teleconseillers.FindAsync(dto.TeleconseillerId)
            };
        }
    }

}
