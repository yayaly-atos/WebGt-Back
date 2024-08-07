using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapiG2T.Services.Interfaces;

namespace G2T.Controllers
{
    [ApiController]
    [Route("webapig2t/[controller]")]
    [Authorize]
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
                return NotFound("le numero de telephone n'existe pas");
            }

            return Ok(contact);
        }
        [HttpGet("by-id/{idContact}")]
        public async Task<IActionResult> GetContactById(string idContact)
        {
            var contact = await _contactService.GetContactByIDAsync(idContact);

            if (contact == null)
            {
                return NotFound("l'id de contact n'existe pas");
            }

            return Ok(contact);
        }
    }
}
