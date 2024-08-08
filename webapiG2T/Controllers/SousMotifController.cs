using G2T.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Dto;
using webapiG2T.Services.Implementations;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    
    [Route("webapig2t/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<List<SousMotifDtoReturn>>> GetAllSousMotifs()
        {
            var sousMotifs = await _sousMotifService.GetAllSousMotifsAsync();
            return Ok(sousMotifs);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<SousMotif>> CreateSousMotif([FromBody] SousMotifDto newSousMotif)
        {
            if (newSousMotif == null)
            {
                return BadRequest("Le sous-motif ne peut pas être vide.");
            }

            var createdSousMotif = await _sousMotifService.CreateSousMotifAsync(newSousMotif);
            return CreatedAtAction(nameof(GetSousMotifNom), new { id = createdSousMotif.Id }, createdSousMotif);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateSousMotif( [FromBody] SousMotifDto updatedSousMotif)
        {
        

            var result = await _sousMotifService.UpdateSousMotifAsync(updatedSousMotif);

            if (result)
            {
                return Ok(updatedSousMotif);
            }

            return NotFound("Vérifiez le sous-motif, il n'existe plus");
        }

    }
}
