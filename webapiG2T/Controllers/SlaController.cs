using G2T.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models;
using webapiG2T.Services.Implementations;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
   
    [ApiController]
    [Route("webapig2t/[controller]")]
    [Authorize]
    public class SlaController : ControllerBase
    {
        private readonly ISLaService _slaserice;
        public SlaController(ISLaService sLaService)
        {
             _slaserice= sLaService;
        }

   
        [HttpGet]
        public async Task<ActionResult<List<Sla>>> GetAllSla()
        {
            var sla = await _slaserice.GetAllSla();
            return Ok(sla);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSla(int id, [FromBody] Sla updatedSla)
        {
            if (updatedSla == null || id != updatedSla.Id)
            {
                return BadRequest("Les données de mise à jour ne sont pas valides.");
            }

            var sla = await _slaserice.GetSlaById(id);
            if (sla == null)
            {
                return NotFound();
            }

            await _slaserice.UpdateSlaAsync(updatedSla);
            return NoContent();
        }

    
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSla(int id)
        {
            var sla = await _slaserice.GetSlaById(id);
            if (sla == null)
            {
                return NotFound();
            }

            await _slaserice.DeleteSlaAsync(id);
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Sla>> CreateSla([FromBody] Sla newSla)
        {
            if (newSla == null)
            {
                return BadRequest("Le SLA ne peut pas être nul.");
            }

            var createdSla = await _slaserice.CreateSlaAsync(newSla);
            return CreatedAtAction(nameof(GetSlaById), new { id = createdSla.Id }, createdSla);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Sla>> GetSlaById(int id)
        {
            var sla = await _slaserice.GetSlaById(id);
            if (sla == null)
            {
                return NotFound();
            }
            return Ok(sla);
        }

    }
}

