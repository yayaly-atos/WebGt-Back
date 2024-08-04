using G2T.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Dto;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [Route("webapig2t/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetInncidentAll()
        {
            var incident = await _incidentService.GetIncidentAllAsync();

            if (incident == null)
            {
                return NotFound("Aucun incident trouve.");
            }

            return Ok(incident);
        }

        [Authorize(Roles = "Agent")]
        [HttpGet("agent-resolu/{idAgent}")]
        public async Task<IActionResult> GetIncidentResoluByAgent(string idAgent)
        {
            var incident = await _incidentService.GetIncidentsResoluByAgeNT(idAgent);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident resolu trouvé avec l'id  de l'agent fourni.");
            }
            return Ok(incident);
        }

        [Authorize(Roles = "Agent")]
        [HttpGet("agent-ouvert/{idAgent}")]
        public async Task<IActionResult> GetIncidentOuvertByAgent(string idAgent)
        {
            var incident = await _incidentService.GetIncidentsOuvertByAgeNT(idAgent);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident ouvert trouvé avec l'id  de l'agent fourni.");
            }
            return Ok(incident);
        }
        [Authorize(Roles = "Agent")]
        [HttpGet("agent-nonouvert/{idAgent}")]
        public async Task<IActionResult> GetIncidentNonOuvertByAgent(string idAgent)
        {
            var incident = await _incidentService.GetIncidentsNonOuvertByAgeNT(idAgent);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident non ouvert trouvé avec l'id  de l'agent fourni.");
            }
            return Ok(incident);
        }




        [Authorize(Roles = "Teleconseiller")]
        [HttpGet("by-phone-and-id/{phoneNumber}/{incidentId}")]
        public async Task<IActionResult> GetIncidentByPhoneNumberAndId(string phoneNumber, int incidentId)
        {
            var incident = await _incidentService.GetIncidentByPhoneNumberAndIdAsync(phoneNumber, incidentId);

            if (incident == null)
            {
                return NotFound("Aucun incident trouvé avec les paramètres fournis.");
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
                return NotFound("Aucun incident trouvé avec le numéro de téléphone fourni.");
            }
            return Ok(incident);
        }
        [Authorize(Roles = "Teleconseiller")]
        [HttpGet("id/{incidentId}")]
        public async Task<IActionResult> GetIncidentById(int incidentId)
        {
            var incident = await _incidentService.GetIncidentByIDAsync(incidentId);
            if (incident == null)
            {
                return NotFound("Aucun incident trouvé avec l'ID fourni.");
            }
            return Ok(incident);
        }
        [Authorize(Roles = "Teleconseiller")]
        [HttpPost("incident")]
        public async Task<IActionResult> CreateIncident([FromBody] CreateIncidentDtocs incidentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdIncident = await _incidentService.CreateIncidentAsync(incidentDto);
            return CreatedAtAction(nameof(GetIncidentById), new { incidentId = createdIncident.Id }, createdIncident);
        }
        [Authorize(Roles = "Teleconseiller,Superviseur,Agent")]
        [HttpPut("changeStatsus/{id}")]
        public async Task<ActionResult<IncidentDto>> UpdateIncidentCommentAndStatus(int id, [FromBody] CreateIncidentDtocs dto)
        {
            var updatedIncident = await _incidentService.UpdateIncident(id, dto);

            if (updatedIncident == null)
            {
                return NotFound();
            }

            return Ok(updatedIncident);
        }

        [Authorize(Roles = "Agent")]
        [HttpGet("agent/{idAgent}")]
        public async Task<IActionResult> GetIncidentByAgent(string idAgent)
        {
            var incident = await _incidentService.GetIncidentsByAgent(idAgent);
            if (incident.Count == 0)
            {
                return NotFound("Aucun incident trouvé avec l'id  de l'agent fourni.");
            }
            return Ok(incident);
        }



        [Authorize(Roles = "Agent")]
        [HttpPut("demandeEscalade/{id}")]
        public async Task<ActionResult<IncidentDto>>  DemandeEscalade(int id)
        {
            var updatedIncident = await _incidentService.DemandeEscalade(id);

            if (updatedIncident == null)
            {
                return NotFound("l'incident avec l'id n'existe pas ou a ete deja escalade");
            }

            return Ok(updatedIncident);
        }


    }

}
