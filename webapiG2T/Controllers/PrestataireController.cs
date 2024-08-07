using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{

    [Route("webapig2t/[controller]")]
    [ApiController]
    [Authorize]
    public class PrestataireController : ControllerBase
    {
        private readonly IprestataireService _prestataireService;
        public PrestataireController(IprestataireService prestataireService)
        {
            _prestataireService = prestataireService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Prestataire>>> GetAllPrestataires()
        {
            var prestataires = await _prestataireService.GetAllPrestatairesAsync();
            if (prestataires.Count == 0)
            {
                return NotFound("Aucun prestataire n'a été trouvé.");
            }
            return Ok(prestataires);
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestataire>> GetPrestataireById(int id)
        {
            var prestataire = await _prestataireService.GetPrestataireByIdAsync(id);
            if (prestataire == null)
            {
                return NotFound($"Le prestataire avec l'ID {id} n'a pas été trouvé.");
            }
            return Ok(prestataire);
        }

        
        [HttpPost]
        public async Task<ActionResult<Prestataire>> CreatePrestataire([FromBody] Prestataire newPrestataire)
        {
            if (newPrestataire == null)
            {
                return BadRequest("Le prestataire ne peut pas être nul.");
            }

            var createdPrestataire = await _prestataireService.CreatePrestataireAsync(newPrestataire);
            return CreatedAtAction(nameof(GetPrestataireById), new { id = createdPrestataire.Id }, createdPrestataire);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePrestataire(int id, [FromBody] Prestataire updatedPrestataire)
        {
            if (updatedPrestataire == null || id != updatedPrestataire.Id)
            {
                return BadRequest("Les données de mise à jour ne sont pas valides.");
            }

            var prestataire = await _prestataireService.GetPrestataireByIdAsync(id);
            if (prestataire == null)
            {
                return NotFound($"Le prestataire avec l'ID {id} n'a pas été trouvé.");
            }

            await _prestataireService.UpdatePrestataireAsync(updatedPrestataire);
            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePrestataire(int id)
        {
            var prestataire = await _prestataireService.GetPrestataireByIdAsync(id);
            if (prestataire == null)
            {
                return NotFound($"Le prestataire avec l'ID {id} n'a pas été trouvé.");
            }

            await _prestataireService.DeletePrestataireAsync(id);
            return NoContent();
        }
    }
}
