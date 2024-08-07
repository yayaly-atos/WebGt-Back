using G2T.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
  
    [Route("webapig2t/[controller]")]
    [ApiController]
    [Authorize]
    public class MotifController : ControllerBase
    {
        private readonly IMotifService _motifService;
        public MotifController(IMotifService motifService)
        {
            _motifService = motifService;

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetMotifNom(int id)
        {
            var motifNom = await _motifService.GetMotifNomByIdAsync(id);
            if (motifNom == null)
            {
                return NotFound();
            }
            return Ok(motifNom);
        }

        [HttpGet]
        public async Task<ActionResult<List<Motif>>> GetAllMotifs()
        {
            var motifs = await _motifService.GetAllMotifsAsync();
            return Ok(motifs);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Motif>> CreateMotif([FromBody] Motif newMotif)
        {
            if (newMotif == null)
            {
                return BadRequest("Le motif ne peut pas être vide.");
            }

            var createdMotif = await _motifService.CreateMotifAsync(newMotif);
            return CreatedAtAction(nameof(GetMotifNom), new { id = createdMotif.Id }, createdMotif);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateMotif( [FromBody] Motif updatedMotif)
        {


            var result = await _motifService.UpdateMotifAsync( updatedMotif);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
