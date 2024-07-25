using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using webapiG2T.Services.Interfaces;

namespace G2T.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("by-phone/{phoneNumber}")]
        public async Task<IActionResult> GetContactByPhoneNumber(string phoneNumber)
        {
            var contact = await _contactService.GetContactByPhoneNumberAsync(phoneNumber);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
    }
}
