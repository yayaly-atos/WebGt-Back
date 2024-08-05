using G2T.Data;
using G2T.Models;

using G2T.Models.enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapiG2T.Models.Dto;
using webapiG2T.Models.enums;
using webapiG2T.Models.Forms;
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

        public async Task<List<IncidentDto>> GetIncidentAllAsync()
        {
            var incidents = await _context.Incidents
                .Include(i => i.Contact)
                .Include(I => I.Superviseur)
                .Include(i => i.NiveauDurgence)
                .Include(i => i.Canal)
                .Include(i => i.sousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Include(i => i.EntiteSupport)
                .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;

        }

        public async Task<List<IncidentDto>> GetIncidentsResoluByAgeNT(String idAgent)
        {
            var incidents = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.NiveauDurgence)
                .Include(i => i.Canal)
                .Include(i => i.sousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Include(i => i.EntiteSupport)
                .Where(i => i.Agent.Id == idAgent && i.StatutIncident == "resolu")
                .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
        }

        public async Task<List<IncidentDto>> GetIncidentsOuvertByAgeNT(String idAgent)
        {
            var incidents = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.NiveauDurgence)
                .Include(i => i.Canal)
                .Include(i => i.sousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Include(i => i.EntiteSupport)
                .Where(i => i.Agent.Id == idAgent && i.StatutIncident == "encours")
                .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
        }

        public async Task<List<IncidentDto>> GetIncidentsNonOuvertByAgeNT(String idAgent)
        {
            var incidents = await _context.Incidents
                .Include(i => i.Contact)
                .Include(i => i.NiveauDurgence)
                .Include(i => i.Canal)
                .Include(i => i.sousMotif)
                .Include(i => i.Teleconseiller)
                .Include(i => i.Service)
                .Include(i => i.EntiteSupport)
                .Where(i => i.Agent.Id == idAgent && i.StatutIncident == "nouveau")
                .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
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

        public async Task<Response> DemandeEscalade(int incidentId, String commentaire  )
        {
            var incident = await _context.Incidents
                        .Where(i => i.Id == incidentId)
                        .FirstOrDefaultAsync();

            if(incident == null)
                return new Response
                {
                    Status = "Erreur",
                    Message = "l'incident avec l'id n'existe pas."
                };
            if (incident.Escalade == false)
            {
                incident.Escalade = true;
                incident.CommentaireAgent = commentaire;
                


                await _context.SaveChangesAsync();

                return new Response
                {
                    Status = "Erreur",
                    Message = "la demande de escalade est effectue avec succes."
                };
            }
            else
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "Incident a ete deja eu une demande de escalade."
                };
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
                .Where(i => i.Agent.Id == idAgent)
                .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
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

        async Task<List<IncidentDto>>  IIncidentService.GetIncidentsBySuperviseur(int EntiteId)
        {
            var incidents = await _context.Incidents
              .Include(i => i.Contact)
              .Include(i => i.NiveauDurgence)
              .Include(i => i.Canal)
              .Include(i => i.sousMotif)
              .Include(i => i.Teleconseiller)
              .Include(i => i.Service)
              .Include(i => i.EntiteSupport)
              .Where(i => i.EntiteSupport.Id == EntiteId)
              .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
        }

        async Task<List<IncidentDto>> IIncidentService.GetIncidentsResoluBySuperviseur(int EntiteId)
        {
            var incidents = await _context.Incidents
               .Include(i => i.Contact)
               .Include(i => i.NiveauDurgence)
               .Include(i => i.Canal)
               .Include(i => i.sousMotif)
               .Include(i => i.Teleconseiller)
               .Include(i => i.Service)
               .Include(i => i.EntiteSupport)
               .Where(i => i.EntiteSupport.Id == EntiteId && i.StatutIncident == "nouveau")
               .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
        }

        async Task<List<IncidentDto>> IIncidentService.GetIncidentsOuvertBySuperviseur(int EntiteId)
        {
            var incidents = await _context.Incidents
               .Include(i => i.Contact)
               .Include(i => i.NiveauDurgence)
               .Include(i => i.Canal)
               .Include(i => i.sousMotif)
               .Include(i => i.Teleconseiller)
               .Include(i => i.Service)
               .Include(i => i.EntiteSupport)
               .Where(i => i.EntiteSupport.Id == EntiteId && i.StatutIncident == "encours")
               .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
        }

        async Task<List<IncidentDto>> IIncidentService.GetIncidentsNonOuvertBySuperviseur(int EntiteId)
        {
            var incidents = await _context.Incidents
               .Include(i => i.Contact)
               .Include(i => i.NiveauDurgence)
               .Include(i => i.Canal)
               .Include(i => i.sousMotif)
               .Include(i => i.Teleconseiller)
               .Include(i => i.Service)
               .Include(i => i.EntiteSupport)
               .Where(i => i.EntiteSupport.Id == EntiteId && i.StatutIncident == "nouveau")
               .ToListAsync();

            var incidentDtos = new List<IncidentDto>();
            foreach (var incident in incidents)
            {
                incidentDtos.Add(await MapToIncidentDtoAsync(incident));
            }
            return incidentDtos;
        }
        public async Task<Response> TakeIncident(int incidentId, string idAgent)
        {
            var incident = await _context.Incidents.FindAsync(incidentId);

            if (incident == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "Incident non trouvé."
                };
            }

            var agent = await _context.Utilisateurs.FindAsync(idAgent);

            if (agent == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "Agent non trouvé."
                };
            }

         
            incident.Agent = agent;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
              
                return new Response
                {
                    Status = "Erreur",
                    Message = $"Une erreur est survenue lors de l'enregistrement des modifications : {ex.Message}"
                };
            }

           
            return new Response
            {
                Status = "Succès",
                Message = "Incident mis à jour avec succès."
            };
        }

        public async Task<Response> EscaladeIncident(int incidentI,String commentaire)
        {
            var incident = await _context.Incidents
         .Include(i => i.EntiteSupport)
         .FirstOrDefaultAsync(i => i.Id == incidentI);
            if (incident == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "Incident non trouvé."
                };
            }

            if (incident.EntiteSupport == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "L'incident n'a pas d'entité de support associée."
                };
            }

            var entite = await _context.EntitesSupports.FindAsync(incident.EntiteSupport.Id + 1);
            if (entite == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "Entité à escalader n'existe pas."
                };
            }

            incident.EntiteSupport = entite;
            incident.CommentaireEscalade = commentaire;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = $"Une erreur est survenue lors de l'enregistrement des modifications : {ex.Message}"
                };
            }

            return new Response
            {
                Status = "Succès",
                Message = "Incident escaladé avec succès."
            };
        }

       public async Task<Response> StartResolutionIncident(int incidentID)
        {
            var incident = await _context.Incidents.FindAsync(incidentID);

            if (incident == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "Incident non trouvé."
                };
            }

          
            incident.StatutIncident = "encours";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = $"Une erreur est survenue lors de l'enregistrement des modifications : {ex.Message}"
                };
            }

            return new Response
            {
                Status = "Succès",
                Message = "Incident en cours de resolution."
            };


        }
        public async Task<Response> EndResolutionIncident(int incidentID, String commentaire)
        {
            var incident = await _context.Incidents.FindAsync(incidentID);

            if (incident == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "Incident non trouvé."
                };
            }


            incident.StatutIncident = "resolu";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = $"Une erreur est survenue lors de l'enregistrement des modifications : {ex.Message}"
                };
            }

            return new Response
            {
                Status = "Succès",
                Message = "Incident cloture."
            };
        }




    }
}
