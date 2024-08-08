using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [ApiController]
    [Route("webapig2t/[controller]")]
    
    public class UtilisateurController : ControllerBase
    {

        private readonly IUtIlisateurService _utIlisateurService;
        private readonly IAuthenticationService _authService;


        public UtilisateurController(IUtIlisateurService utIlisateurService, IAuthenticationService authService)
        {
            _utIlisateurService = utIlisateurService;
            _authService = authService;
        }

        [Authorize(Roles = "Superviseur")]
        [HttpGet("agents-by-entite")]
        public async Task<IActionResult> GetAgentsByEntite()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userEntiteIdString = _authService.DecodeTokenAndGetUEntiteId(token);
            var agents = await _utIlisateurService.GetUsersAgentByEntite(userEntiteIdString);
            if (agents == null || agents.Count == 0)
            {
                return NotFound(new { Message = "Pas d'agents touves" });
            }
            return Ok(agents);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [HttpGet("user-by-id/{idUser}")]
        public async Task<IActionResult> GetUserById(string idUser)
        {
            var agent = await _utIlisateurService.GetUserBYId(idUser);
            if (agent == null)
            {
                return NotFound(new { Message = "Pas d'utulisateur touve" });
            }
            return Ok(agent);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<IActionResult> GetAdmin()
        {
            var admins = await _utIlisateurService.GetAdminById();
            if (admins == null || admins.Count == 0)
            {
                return NotFound(new { Message = "Pas d'adminitrateurs touves" });
            }
            return Ok(admins);
        }

    }
}
