using G2T.Models;
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
    }
}
