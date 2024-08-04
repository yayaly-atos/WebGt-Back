using G2T.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("webapig2t/[controller]")]
    public class SlaController : ControllerBase
    {
        private readonly ISLaService _slaserice;
        public SlaController(ISLaService sLaService)
        {
             _slaserice= sLaService;
        }

   
        [HttpGet]
        public async Task<ActionResult<List<Sla>>> GetAllCanaux()
        {
            var sla = await _slaserice.GetAllSla();
            return Ok(sla);
        }
    }
}
