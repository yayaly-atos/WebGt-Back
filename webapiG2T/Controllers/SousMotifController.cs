using G2T.Models;
using Microsoft.AspNetCore.Mvc;
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

    }
}
