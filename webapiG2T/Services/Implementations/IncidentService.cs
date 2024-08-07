using G2T.Data;
using G2T.Models;

using G2T.Models.enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using webapiG2T.Models;
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

        public async Task<IncidentDto> CreateIncidentAsync(CreateIncidentDtocs incidentDto, String id)
        {
         
           
            var priorite = await _context.Priorite
           .Where(p => p.Id == incidentDto.NiveauDurgenceId)
           .FirstOrDefaultAsync();
         
            var incident = await MapToIncidentAsync(incidentDto);
            incident.DateEcheance = DateTime.Now.Add(TimeSpan.Parse(priorite.Latence));
            incident.DateCreation = DateTime.Now;
            incident.EntiteSupport = await _context.EntitesSupports
              .OrderBy(es => es.Id)
             .FirstOrDefaultAsync();
            int idEntite = incident.EntiteSupport.Id;
            incident.Superviseur = await _context.Utilisateurs
           .Where(u => u.EntiteSupportId == idEntite)
           .FirstOrDefaultAsync();
            incident.StatutIncident = "nouveau";
            incident.Teleconseiller = await _context.Utilisateurs.FindAsync(id);

            _context.Incidents.Add(incident);
            var historiqueIncident = new HistoriqueIncident
            {
                IncidentId = incident.Id,
                Nature = "Création",

                ValeurNouveau = incidentDto.CommentaireTeleconseiller,
                DateHistorique = DateTime.Now,
                Utilisateur = await _context.Utilisateurs.FindAsync(id)
            };

          
            _context.Historiques.Add(historiqueIncident);
            await _context.SaveChangesAsync();

            return await MapToIncidentDtoAsync(incident);
        }

        public async Task<Response> DemandeEscalade(int incidentId, String commentaire,String Id)
        {
            var incident = await _context.Incidents
                        .Where(i => i.Id == incidentId)
                        .FirstOrDefaultAsync();

            if(incident == null)
                return new Response
                {
                    Status = "Erreur",
                    Message = "L'incident avec l'identifiant n'existe pas."
                };
            if (incident.Escalade == false)
            {
               

                var historiqueIncident = new HistoriqueIncident
                {
                    IncidentId = incident.Id,
                    Nature = "Demande Escalade",
                    ValeurPrecedente=incident.CommentaireEscalade,
                    ValeurNouveau = commentaire,
                    DateHistorique = DateTime.Now,
                    Utilisateur = await _context.Utilisateurs.FindAsync(Id)

                };
                incident.Escalade = true;
                incident.CommentaireAgent = commentaire;

                // Ajouter l'historique à la base de données
                _context.Historiques.Add(historiqueIncident);
                await _context.SaveChangesAsync();


                await _context.SaveChangesAsync();


                return new Response
                {
                    Status = "Succes",
                    Message = "la demande de escalade a été effectuée avec succès."
                };
            }
            else
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "L'incident a été déjà eu une demande de escalade."
                };
            }
            return null;
           
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
            
              
                CommentaireTeleconseiller = dto.CommentaireTeleconseiller,
                Contact = await _context.Contacts.FindAsync(dto.ContactId),
                Canal = await _context.Canaux.FindAsync(dto.CanalId),
                sousMotif = await _context.SousMotifs.FindAsync(dto.SousMotifId),
       
                Service = await _context.Services.FindAsync(dto.ServiceId),
                NiveauDurgence = await _context.Priorite.FindAsync(dto.NiveauDurgenceId),
              
               
               
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
                    Message = "L'incident non trouvé."
                };
            }

            var agent = await _context.Utilisateurs.FindAsync(idAgent);

            if (agent == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "L'agent non trouvé."
                };
            }
            string idAgentBefore = incident.Agent != null ? incident.Agent.Id : null;


            incident.Agent = agent;

            try
            {
                var historiqueIncident = new HistoriqueIncident
                {
                    IncidentId = incident.Id,
                    Nature = "Création",
                    ValeurPrecedente = idAgentBefore,
                    ValeurNouveau = idAgent,
                    DateHistorique = DateTime.Now,
                 

                };

                _context.Historiques.Add(historiqueIncident);
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
                Message = "L'incident mis à jour avec succès."
            };
        }

        public async Task<Response> EscaladeIncident(int incidentI,String commentaire, String Id)
        {
            var incident = await _context.Incidents
         .Include(i => i.EntiteSupport)
         .FirstOrDefaultAsync(i => i.Id == incidentI);
            if (incident == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "L'incident non trouvé."
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
                    Message = "L'entité à escalader n'existe pas."
                };
            }
            var CommentBefore = incident.CommentaireEscalade;
            incident.EntiteSupport = entite;
            incident.CommentaireEscalade = commentaire;
            incident.Superviseur = await _context.Utilisateurs
          .Where(u => u.EntiteSupportId == entite.Id)
          .FirstOrDefaultAsync();

            var historiqueIncident = new HistoriqueIncident
            {
                IncidentId = incident.Id,
                Nature = "Escalade",
               ValeurPrecedente= CommentBefore,
                ValeurNouveau = commentaire,
                DateHistorique = DateTime.Now,
                Utilisateur = await _context.Utilisateurs.FindAsync(Id)

            };

             _context.Historiques.Add(historiqueIncident);
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
                Message = "L'incident escaladé avec succès."
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
                    Message = "L'incident non trouvé."
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
        public async Task<Response> EndResolutionIncident(int incidentID, String commentaire, String Id)
        {
            var incident = await _context.Incidents.FindAsync(incidentID);

            if (incident == null)
            {
                return new Response
                {
                    Status = "Erreur",
                    Message = "L'incident non trouvé."
                };
            }

            var CommentBefore=incident.CommentaireCloture;
            incident.StatutIncident = "resolu";
            incident.CommentaireCloture = commentaire;
            try
            {
                var historiqueIncident = new HistoriqueIncident
                {
                    IncidentId = incident.Id,
                    Nature = "Fin resolution",
                    ValeurPrecedente = CommentBefore,
                    ValeurNouveau = commentaire,
                    DateHistorique = DateTime.Now,
                    Utilisateur = await _context.Utilisateurs.FindAsync(Id)

                };


                _context.Historiques.Add(historiqueIncident);
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
                Message = "L'incident clôturé."
            };
        }




    }
}
