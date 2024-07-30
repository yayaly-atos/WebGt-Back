using G2T.Models;
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
        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetIncidentByPhoneNumber(string phoneNumber)
        {
            var incident = await _incidentService.GetIncidentsByPhoneNumberAsync(phoneNumber);
            if (incident.Count==0)
            {
                return NotFound("Aucun incident trouvé avec le numéro de téléphone fourni.");
            }
            return Ok(incident);
        }

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

      


    }
}
