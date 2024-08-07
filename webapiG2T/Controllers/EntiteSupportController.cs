using G2T.Models;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Services.Implementations;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [Route("webapig2t/[controller]")]
    [ApiController]
    public class EntiteSupportController : ControllerBase
    {
        private readonly IEntiteSupport _EntiteService;

        public EntiteSupportController(IEntiteSupport entiteService)
        {
            _EntiteService = entiteService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllEntites()
        {
            var entite = await _EntiteService.GetAllEntitesAsync();
            if (entite.Count == 0)
            {
                return NotFound("Aucun entite n'a été trouvé.");
            }
            return Ok(entite);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EntiteSupport>> GetEntiteById(int id)
        {
            var entite = await _EntiteService.GetEntiteByIdAsync(id);
            if (entite == null)
            {
                return NotFound($"L'entité avec l'ID {id} n'a pas été trouvée.");
            }
            return Ok(entite);
        }

       
        [HttpPost]
        public async Task<ActionResult<EntiteSupport>> CreateEntite([FromBody] EntiteSupport newEntite)
        {
            if (newEntite == null)
            {
                return BadRequest("L'entité ne peut pas être nulle.");
            }

            var createdEntite = await _EntiteService.CreateEntiteAsync(newEntite);
            return CreatedAtAction(nameof(GetEntiteById), new { id = createdEntite.Id }, createdEntite);
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEntite(int id, [FromBody] EntiteSupport updatedEntite)
        {
            if (updatedEntite == null || id != updatedEntite.Id)
            {
                return BadRequest("Les données de mise à jour ne sont pas valides.");
            }

            var entite = await _EntiteService.GetEntiteByIdAsync(id);
            if (entite == null)
            {
                return NotFound($"L'entité avec l'ID {id} n'a pas été trouvée.");
            }

            await _EntiteService.UpdateEntiteAsync(updatedEntite);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEntite(int id)
        {
            var entite = await _EntiteService.GetEntiteByIdAsync(id);
            if (entite == null)
            {
                return NotFound($"L'entité avec l'ID {id} n'a pas été trouvée.");
            }

            await _EntiteService.DeleteEntiteAsync(id);
            return NoContent();
        }
    }

}
   

