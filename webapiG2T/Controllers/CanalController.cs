using G2T.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
  
    [Route("webapig2t/[controller]")]
    [ApiController]
    public class CanalController : ControllerBase
    {
        private readonly ICanalService _canalService;

        public CanalController(ICanalService canalService)
        {
            _canalService = canalService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetCanalNom(int id)
        {
            var canalNom = await _canalService.GetCanalNomByIdAsync(id);
            if (canalNom == null)
            {
                return NotFound();
            }
            return Ok(canalNom);
        }

        [HttpGet]
        public async Task<ActionResult<List<Canal>>> GetAllCanaux()
        {
            var canaux = await _canalService.GetAllCanauxAsync();
            return Ok(canaux);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Canal>> CreateCanal([FromBody] Canal newCanal)
        {
            if (newCanal == null)
            {
                return BadRequest("Le canal ne peut pas être nul.");
            }

            var createdCanal = await _canalService.CreateCanalAsync(newCanal);
            return CreatedAtAction(nameof(GetCanalNom), new { id = createdCanal.Id }, createdCanal);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCanal( [FromBody] Canal updatedCanal)
        {
            

            var result = await _canalService.UpdateCanalAsync(updatedCanal);

            if (result)
            {
                return Ok(updatedCanal);
            }

            return NotFound();
        }
    }
}
