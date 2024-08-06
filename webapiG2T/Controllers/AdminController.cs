using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Forms;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [ApiController]
    [Route("webapig2t/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

     
        [HttpPost("Ajouter_Role")]
        public async Task<IActionResult> AjouterRole([FromBody] RoleModel roleName)
        {
            var response = _adminService.CreateRole(roleName);

            if(response.Result.Status != "error")
            {
                return Ok(response.Result.Message);
            }
            
            return BadRequest(response.Result);
        }

      
    }
}
