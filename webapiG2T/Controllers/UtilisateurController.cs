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
    

        public UtilisateurController(IUtIlisateurService utIlisateurService)
        {
            _utIlisateurService = utIlisateurService;
           
        }
        [HttpGet("agents/{entiteID}")]
        public async Task<IActionResult> GetAgents(int entiteID)
        {
            var agents = await _utIlisateurService.GetUsersAgent(entiteID);
            if (agents == null || agents.Count == 0)
            {
                return NotFound(new { Message = "Aucun agent n'a été trouvé" });
            }
            return Ok(agents);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("teleconseiller")]
        public async Task<IActionResult> GetTeleconseiller()
        {
            var agents = await _utIlisateurService.GetUsersTeleconseiller();
            if (agents == null || agents.Count == 0)
            {
                return NotFound(new { Message = "Aucun téléconseiller n'a été trouvé" });
            }
            return Ok(agents);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Superviseur")]
        public async Task<IActionResult> GetSuperviseur()
        {
            var agents = await _utIlisateurService.GetUsersSuperviseur();
            if (agents == null || agents.Count == 0)
            {
                return NotFound(new { Message = "Aucun superviseur n'a été trouvé" });
            }
            return Ok(agents);
        }
    }
}
