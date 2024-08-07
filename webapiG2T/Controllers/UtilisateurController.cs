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
    

        [HttpGet("agents-by-entite/{entiteID}")]
        public async Task<IActionResult> GetAgentsByEntite(int entiteID)
        {
            var agents = await _utIlisateurService.GetUsersAgentByEntite(entiteID);
            if (agents == null || agents.Count == 0)
            {
                return NotFound(new { Message = "Pas d'agents touves" });
            }
            return Ok(agents);
        }

        [HttpGet("agents")]
        public async Task<IActionResult> GetAgents()
        {
            var agents = await _utIlisateurService.GetAgents();
            if (agents == null || agents.Count == 0)
            {
                return NotFound(new { Message = "Pas d'agents touves" });
            }
            return Ok(agents);
        }

        [HttpGet("teleconseiller")]
        public async Task<IActionResult> GetTelleconseiller()
        {
            var agents = await _utIlisateurService.GetUsersTeleconseiller();
            if (agents == null || agents.Count == 0)
            {
                return NotFound(new { Message = "Pas de teleconseillers touves" });
            }
            return Ok(agents);
        }
        [HttpGet("Superviseur")]
        public async Task<IActionResult> GetSuperviseur()
        {
            var agents = await _utIlisateurService.GetUsersTeleconseiller();
            if (agents == null || agents.Count == 0)
            {
                return NotFound(new { Message = "Pas de superviseurs touves" });
            }
            return Ok(agents);
        }

        [HttpGet("agents-by-id/{agentID}")]
        public async Task<IActionResult> GetAgentsByEntite(string agentID)
        {
            var agents = await _utIlisateurService.GetAgentById(agentID);
            if (agents == null )
            {
                return NotFound(new { Message = "Pas d'agent touve" });
            }
            return Ok(agents);
        }

    }
}
