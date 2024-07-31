using G2T.Data;
using G2T.Models;

using G2T.Models.enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapiG2T.Models.Dto;
using webapiG2T.Models.enums;
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
                .Include(I => I.Superviseur)
                .Include(i => i.NiveauDurgence)
                .Include(i => i.Canal)
                .Include(i => i.sousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Include(i => i.EntiteSupport)
                .Where(i => i.Id == incidentId)
                .FirstOrDefaultAsync();

            return await MapToIncidentDtoAsync(incident);
        }

        public async Task<IncidentDto> GetIncidentByPhoneNumberAndIdAsync(string phoneNumber, int incidentId)
        {
            var incident = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.NiveauDurgence)
                .Include(i => i.Canal)
                .Include(i => i.sousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Include(i => i.EntiteSupport)
                .Where(i => i.Id == incidentId && i.Contact.Telephone == phoneNumber)
                .FirstOrDefaultAsync();

            return await MapToIncidentDtoAsync(incident);
        }

        public async Task<List<IncidentDto>> GetIncidentsByPhoneNumberAsync(string phoneNumber)
        {
            var incidents = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.NiveauDurgence)
                .Include(i => i.Canal)
                .Include(i => i.sousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Include(i => i.EntiteSupport)
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

        public async Task<Incident> DemandeEscalade(int incidentId)
        {
            var incident = await _context.Incidents
                        .Where(i => i.Id == incidentId)
                        .FirstOrDefaultAsync();
            if (incident.Escalade == false)
            {
                incident.Escalade = true;


                await _context.SaveChangesAsync();

                return  incident;
            }
            return null;
           
        }

        public async Task<IncidentDto> UpdateIncident(int incidentId, CreateIncidentDtocs incidentDto)
        {
            var incident = await _context.Incidents.FindAsync(incidentId);

            if (incident == null)
            {
                return null;
            }

            incident.CommentaireCloture = incidentDto.CommentaireCloture;
            incident.StatutIncident = incidentDto.StatutIncident ;
            incident.DateResolution = incidentDto.DateResolution;
            incident.CommentaireAgent = incidentDto.CommentaireAgent;
            incident.CommentaireTeleconseiller = incidentDto.CommentaireTeleconseiller;
            incident.CommentaireEscalade=incidentDto.CommentaireEscalade;
            incident.DateAffectation = incidentDto.DateAffectation;
            incident.DateRelance = incidentDto.DateRelance;
            incident.DateEscalade = incidentDto.DateEscalade;
            incident.Escalade= incidentDto.Escalade;
            


            await _context.SaveChangesAsync();

            return await GetIncidentByIDAsync(incidentId);
        }

        public async Task<List<IncidentDto>> GetIncidentsByAgent(String idAgent)
        {
            var incidents = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.NiveauDurgence)
                .Include(i => i.Canal)
                .Include(i => i.sousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Include(i => i.EntiteSupport)
                .Where(i => i.Agent.Id == idAgent && i.StatutIncident == "Nouveau")
                .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
        }

        public async Task<int> GetNumberOfIncidentsByAgent(string idAgent)
        {
            var count = await _context.Incidents
                .Where(i => i.Agent.Id == idAgent && i.StatutIncident == "Nouveau")
                .CountAsync();

            return count;
        }

        public async Task<int> GetNumberOfIncidentsResoluByAgent(string idAgent)
        {
            var count = await _context.Incidents
                .Where(i => i.Agent.Id == idAgent && i.StatutIncident == "Resolu")
                .CountAsync();

            return count;
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
                NomCanal = incident.Canal?.Nom,
                sousMotif = incident.sousMotif?.Nom, 
                Description = incident.Description,
                StatutIncident = incident.StatutIncident,
                DateAffectation = incident.DateAffectation,
                DateCreation = incident.DateCreation,
                DateEscalade = incident.DateEscalade,
                DateEcheance = incident.DateEcheance,
                DateRelance = incident.DateRelance,
                DateResolution = incident.DateResolution,
                Escalade = incident.Escalade,
                AgentId = incident.Agent?.Id, 
                SuperviseurId = incident.Superviseur?.Id, 
                TeleconseillerId = incident.Teleconseiller?.Id, 
                ContactId = incident.Contact.Id,
                NomService = incident.Service?.NomService,
                TypeService = incident.Service?.TypeService,
                NiveauDurgenceId = incident.NiveauDurgence.Id, 
                EntiteSupportId = incident.EntiteSupport.Id, 
                CommentaireEscalade = incident.CommentaireEscalade,
                CommentaireAgent = incident.CommentaireAgent,
                CommentaireCloture = incident.CommentaireCloture,
                CommentaireTeleconseiller = incident.CommentaireTeleconseiller
            };
        }

        private async Task<Incident> MapToIncidentAsync(CreateIncidentDtocs dto)
        {
            return new Incident
            {
                Description = dto.Description,
                StatutIncident = dto.StatutIncident,
                DateAffectation = dto.DateAffectation,
                DateCreation = dto.DateCreation,
                DateEscalade = dto.DateEscalade,
                DateEcheance = dto.DateEcheance,
                DateRelance = dto.DateRelance,
                DateResolution = dto.DateResolution,
                Escalade = dto.Escalade,
                CommentaireEscalade = dto.CommentaireEscalade,
                CommentaireAgent = dto.CommentaireAgent,
                CommentaireCloture = dto.CommentaireCloture,
                CommentaireTeleconseiller = dto.CommentaireTeleconseiller,
                Contact = await _context.Contacts.FindAsync(dto.ContactId),
                Canal = await _context.Canaux.FindAsync(dto.CanalId),
                sousMotif = await _context.SousMotifs.FindAsync(dto.MotifId),
       
                Service = await _context.Services.FindAsync(dto.ServiceId),
                NiveauDurgence = await _context.Priorite.FindAsync(dto.NiveauDurgenceId),
                EntiteSupport = await _context.EntitesSupports.FindAsync(dto.EntiteSupportId),
                Agent = dto.AgentId != null ? await _context.Utilisateurs.FindAsync(dto.AgentId) : null,
                Superviseur = await _context.Utilisateurs.FindAsync(dto.SuperviseurId),
                Teleconseiller = await _context.Utilisateurs.FindAsync(dto.TeleconseillerId)
            };
        }
    }
}
