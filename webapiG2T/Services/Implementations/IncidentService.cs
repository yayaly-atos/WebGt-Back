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
        private readonly ISousMotifService _sousMotifService;
        private readonly IMotifService _motifService;
        private readonly ICanalService _canalService;

        public IncidentService(DataContext context, ISousMotifService sousMotifService, IMotifService motifService, ICanalService canalService)
        {
            _context = context;
            _sousMotifService = sousMotifService;
            _motifService = motifService;
            _canalService = canalService;
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

            return await MapToIncidentDtoAsync(incident);
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

            return await MapToIncidentDtoAsync(incident);
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

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
        }

        public async Task<IncidentDto> CreateIncidentAsync(CreateIncidentDtocs incidentDto)
        {
            var incident = await MapToIncidentAsync(incidentDto);
            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();

            return await MapToIncidentDtoAsync(incident);
        }

        private async Task<IncidentDto> MapToIncidentDtoAsync(Incident incident)
        {
            if (incident == null)
            {
                return null;
            }

            return new IncidentDto
            {
                Id = incident.Id,
                CanalNom = await _canalService.GetCanalNomByIdAsync(incident.Canal.Id),
                MotifNom = await _motifService.GetMotifNomByIdAsync(incident.Motif.Id),
                SousMotifNom = await _sousMotifService.GetSousMotifNomByIdAsync(incident.SousMotif.Id),
                Description = incident.Description,
                Commentaire = incident.Commentaire,
                StatutIncident = incident.StatutIncident,
                ContactId = incident.Contact.Id,
                ServiceId = incident.Service.Id,
                TeleconseillerId = incident.Teleconseiller.Id
            };
        }

        private async Task<Incident> MapToIncidentAsync(CreateIncidentDtocs dto)
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
