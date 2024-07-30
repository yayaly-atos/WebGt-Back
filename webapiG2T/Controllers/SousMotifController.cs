using G2T.Models;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Dto;
using webapiG2T.Services.Implementations;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [Route("webapig2t/[controller]")]
    [ApiController]
    public class SousMotifController:ControllerBase
    {

        private readonly ISousMotifService _sousMotifService;
        public SousMotifController(ISousMotifService sousMotifService)
        {
            _sousMotifService = sousMotifService;

        
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetSousMotifNom(int id)
        {
            var sousMotifNom = await _sousMotifService.GetSousMotifNomByIdAsync(id);
            if (sousMotifNom == null)
            {
                return NotFound();
            }
            return Ok(sousMotifNom);
        }

        [HttpGet]
        public async Task<ActionResult<List<SousMotif>>> GetAllSousMotifs()
        {
            var sousMotifs = await _sousMotifService.GetAllSousMotifsAsync();
            return Ok(sousMotifs);
        }

        [HttpPost]
        public async Task<ActionResult<SousMotif>> CreateSousMotif([FromBody] SousMotifDto newSousMotif)
        {
            if (newSousMotif == null)
            {
                return BadRequest("SousMotif cannot be null.");
            }

            var createdSousMotif = await _sousMotifService.CreateSousMotifAsync(newSousMotif);
            return CreatedAtAction(nameof(GetSousMotifNom), new { id = createdSousMotif.Id }, createdSousMotif);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSousMotif( [FromBody] SousMotifDto updatedSousMotif)
        {
        

            var result = await _sousMotifService.UpdateSousMotifAsync(updatedSousMotif);

            if (result)
            {
                return NoContent();
            }

            return NotFound("verifiez le sous-motif il n'existe pas");
        }

    }
}
