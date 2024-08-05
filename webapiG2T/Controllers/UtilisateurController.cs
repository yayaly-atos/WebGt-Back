using Microsoft.AspNetCore.Mvc;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [ApiController]
    [Route("webapig2t/[controller]")]
    public class UtilisateurController : ControllerBase
    {

        private readonly IUtIlisateurService _utIlisateurService;
    

        public UtilisateurController(IUtIlisateurService utIlisateurService)
        {
            _utIlisateurService = utIlisateurService;
           
        }
        [HttpGet("agents")]
        public async Task<IActionResult> GetAgents()
        {
            var agents = await _utIlisateurService.GetUsersAgent();
            if (agents == null || agents.Count == 0)
            {
                return NotFound(new { Message = "Pas d'agents touves" });
            }
            return Ok(agents);
        }
    }
}
