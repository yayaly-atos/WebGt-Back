using G2T.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
  
    [Route("webapig2t/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceController : ControllerBase
    {

        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Service service)
        {
            if (service == null)
            {
                return BadRequest();
            }

            await _serviceService.CreateServiceAsync(service);
            return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update( [FromBody] Service service)
        {
            

            var existingService = await _serviceService.GetServiceByIdAsync(service.Id);
            if (existingService==null )
            {
                return NotFound();
            }

            await _serviceService.UpdateServiceAsync(service);
            return Ok(service);
        }

       
    }
}
