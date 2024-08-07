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

    }
   
}
