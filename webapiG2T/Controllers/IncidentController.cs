using Azure;
using G2T.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Dto;
using webapiG2T.Models.Forms;    
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [Route("webapig2t/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;
        private readonly IAuthenticationService _authService;

        public IncidentController(IIncidentService incidentService, IAuthenticationService authService)
        {
            _incidentService = incidentService;
            _authService = authService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetInncidentAll()
        {
            var incident = await _incidentService.GetIncidentAllAsync();

            if (incident == null)
            {
                return NotFound("Aucun incident n'a été trouvé.");
            }

            return Ok(incident);
        }

        [Authorize(Roles = "Agent,Superviseur")]
        [HttpGet("agent-resolu")]
        public async Task<IActionResult> GetIncidentResoluByAgent()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _authService.DecodeTokenAndGetUserId(token);
            var incident = await _incidentService.GetIncidentsResoluByAgeNT(userId);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident résolu n'a été trouvé avec l'identifiant de l'agent fourni.");
            }
            return Ok(incident);
        }

        [Authorize(Roles = "Agent,Superviseur")]
        [HttpGet("agent-ouvert")]
        public async Task<IActionResult> GetIncidentOuvertByAgent()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _authService.DecodeTokenAndGetUserId(token);
            var incident = await _incidentService.GetIncidentsOuvertByAgeNT(userId);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident ouvert n'a été trouvé avec l'identifiant de l'agent fourni.");
            }
            return Ok(incident);
        }
        [Authorize(Roles = "Agent,Superviseur")]
        [HttpGet("agent-nonouvert")]
        public async Task<IActionResult> GetIncidentNonOuvertByAgent()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _authService.DecodeTokenAndGetUserId(token);
            var incident = await _incidentService.GetIncidentsNonOuvertByAgeNT(userId);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident non ouvert n'a été trouvé avec l'identifiant de l'agent fourni.");
            }
            return Ok(incident);
        }




        [Authorize(Roles = "Teleconseiller,Superviseur")]
        [HttpGet("by-phone-and-id/{phoneNumber}/{incidentId}")]
        public async Task<IActionResult> GetIncidentByPhoneNumberAndId(string phoneNumber, int incidentId)
        {
            var incident = await _incidentService.GetIncidentByPhoneNumberAndIdAsync(phoneNumber, incidentId);

            if (incident == null)
            {
                return NotFound("Aucun incident n'a été trouvé avec les paramètres fournis.");
            }

            return Ok(incident);
        }
        [Authorize(Roles = "Teleconseiller")]
        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetIncidentByPhoneNumber(string phoneNumber)
        {
            var incident = await _incidentService.GetIncidentsByPhoneNumberAsync(phoneNumber);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident n'a été trouvé avec le numéro de téléphone fourni.");
            }
            return Ok(incident);
        }
        [Authorize(Roles = "Teleconseiller,Agent,Superviseur")]
        [HttpGet("id/{incidentId}")]
        public async Task<IActionResult> GetIncidentById(int incidentId)
        {
            var incident = await _incidentService.GetIncidentByIDAsync(incidentId);
            if (incident == null)
            {
                return NotFound("Aucun incident n'a été trouvé avec l'identifiant fourni.");
            }
            return Ok(incident);
        }
        [Authorize(Roles = "Teleconseiller,Superviseur")]
        [HttpPost("incident")]
        public async Task<IActionResult> CreateIncident([FromBody] CreateIncidentDtocs incidentDto)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _authService.DecodeTokenAndGetUserId(token);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdIncident = await _incidentService.CreateIncidentAsync(incidentDto, userId);
            return CreatedAtAction(nameof(GetIncidentById), new { incidentId = createdIncident.Id }, createdIncident);
        }
      

        [Authorize(Roles = "Agent,Superviseur")]
        [HttpGet("incident-agent")]
        public async Task<IActionResult> GetIncidentByAgent()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            String userId = _authService.DecodeTokenAndGetUserId(token);
            var incident = await _incidentService.GetIncidentsByAgent(userId);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident n'a été trouvé avec l'identifiant de l'agent fourni."+userId);
            }
            return Ok(incident);
        }



        [Authorize(Roles = "Agent,Superviseur")]
        [HttpPut("demandeEscalade")]
        public async Task<ActionResult>  DemandeEscalade([FromBody] EscaladeIncidentModel model)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _authService.DecodeTokenAndGetUserId(token);
            var updatedIncident = await _incidentService.DemandeEscalade(model.IncidentId,model.Commentaire, userId);

            if (updatedIncident == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, updatedIncident);
            }

            return StatusCode(StatusCodes.Status200OK, updatedIncident);
        }
        [Authorize(Roles = "Superviseur")]
        [HttpGet("superviseur")]
        public async Task<IActionResult> GetIncidentBySuperviseur()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userEntiteIdString = _authService.DecodeTokenAndGetUEntiteId(token);
            if (!int.TryParse(userEntiteIdString, out int userEntiteId))
            {
                return BadRequest("L'ID de l'entité récupéré du token n'est pas valide.");
            }
            var incident = await _incidentService.GetIncidentsBySuperviseur(userEntiteId);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident n'a été trouvé pour l'entite.");
            }
            return Ok(incident);
        }
        [Authorize(Roles = "Superviseur")]
        [HttpGet("superviseur-resolu")]
        public async Task<IActionResult> GetIncidentResoluBySuperviseur()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userEntiteIdString = _authService.DecodeTokenAndGetUEntiteId(token);
            if (!int.TryParse(userEntiteIdString, out int userEntiteId))
            {
                return BadRequest("L'ID de l'entité récupéré du token n'est pas valide.");
            }
            var incident = await _incidentService.GetIncidentsResoluBySuperviseur(userEntiteId);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident résolu n'a été trouvé pour l'entité.");
            }
            return Ok(incident);
        }

        [Authorize(Roles = "Superviseur")]
        [HttpGet("superviseur-ouvert")]
        public async Task<IActionResult> GetIncidentOuvertBySuperviseur()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userEntiteIdString = _authService.DecodeTokenAndGetUEntiteId(token);
            if (!int.TryParse(userEntiteIdString, out int userEntiteId))
            {
                return BadRequest("L'ID de l'entité récupéré du token n'est pas valide.");
            }
            var incident = await _incidentService.GetIncidentsOuvertBySuperviseur(userEntiteId);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident  n'a été trouvé pour l'entité.");
            }
            return Ok(incident);
        }

        [Authorize(Roles = "Superviseur")]
        [HttpGet("superviseur-nonouvert")]
        public async Task<IActionResult> GetIncidentNonOuvertBySuperviseur()
        {

            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userEntiteIdString = _authService.DecodeTokenAndGetUEntiteId(token);
            if (!int.TryParse(userEntiteIdString, out int userEntiteId))
            {
                return BadRequest("L'ID de l'entité récupéré du token n'est pas valide.");
            }
            var incident = await _incidentService.GetIncidentsNonOuvertBySuperviseur(userEntiteId);
            if (incident.Count == 0)
            {
                return NotFound("\"Aucun incident résolu n'a été trouvé pour l'entité.");
            }
            return Ok(incident);
        }

        [Authorize(Roles = "Superviseur")]
        [HttpGet("superviseur-non-attribue")]
        public async Task<IActionResult> GetIncidentNonAttribueBySuperviseur()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userEntiteIdString = _authService.DecodeTokenAndGetUEntiteId(token);
            if (!int.TryParse(userEntiteIdString, out int userEntiteId))
            {
                return BadRequest("L'ID de l'entité récupéré du token n'est pas valide.");
            }
            var incident = await _incidentService.GetIncidentsNonAttribueBySuperviseur(userEntiteId);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident non attribue n'a été trouvé pour l'entité.");
            }
            return Ok(incident);
        }

        [Authorize(Roles = "Superviseur")]
        [HttpGet("superviseur-a-escalade")]
        public async Task<IActionResult> GetIncidentAEscaladeBySuperviseur()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userEntiteIdString = _authService.DecodeTokenAndGetUEntiteId(token);
            if (!int.TryParse(userEntiteIdString, out int userEntiteId))
            {
                return BadRequest("L'ID de l'entité récupéré du token n'est pas valide.");
            }
            var incident = await _incidentService.GetIncidentsAEscaladeBySuperviseur(userEntiteId);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident à escaladé n'a été trouvé pour l'entité.");
            }
            return Ok(incident);
        }

        [Authorize(Roles = "Superviseur")]
        [HttpPut("TakeIncident/{idIncident}/{agentID}")]
        public async Task<ActionResult> TakeIncident(int idIncident,String agentID)
        {

            var incident = await _incidentService.TakeIncident(idIncident, agentID);
            if (incident != null)

            {
                return StatusCode(StatusCodes.Status200OK, incident);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, incident);
        }

        [Authorize(Roles = "Superviseur")]
        [HttpPut("EscaladeIncident")] 
        public async Task<ActionResult> EcaladeIncident([FromBody] EscaladeIncidentModel model)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _authService.DecodeTokenAndGetUserId(token);
            var incident = await _incidentService.EscaladeIncident(model.IncidentId, model.Commentaire, userId);
            if (incident != null)

            {
                return StatusCode(StatusCodes.Status200OK, incident);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, incident);
        }


        [Authorize(Roles = "Agent,Superviseur")]
        [HttpPut("StartResolution/{incidentID}")]
        public async Task<ActionResult> StartResolutionIncident(int incidentID)
        {
            var incident = await _incidentService.StartResolutionIncident(incidentID);
            if (incident != null)

            {
                return StatusCode(StatusCodes.Status200OK, incident);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, incident);
        }


        [Authorize(Roles = "Agent,Superviseur")]
        [HttpPut("EndResolution")]
        public async Task<ActionResult> EndResolutionIncident([FromBody] EscaladeIncidentModel model)
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _authService.DecodeTokenAndGetUserId(token);
            var incident = await _incidentService.EndResolutionIncident(model.IncidentId,model.Commentaire, userId);
            if (incident != null)

            {
                return StatusCode(StatusCodes.Status200OK, incident);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, incident);
        }



    }





}
