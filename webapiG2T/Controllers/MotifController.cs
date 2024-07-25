using G2T.Models;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{

    [Route("webapig2t/[controller]")]
    [ApiController]
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
    }
}
