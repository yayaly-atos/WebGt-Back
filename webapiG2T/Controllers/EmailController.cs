using Microsoft.AspNetCore.Mvc;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
        {
            try
            {
                await _emailService.SendEmailAsync(model.To, model.Subject, model.Body, $"<strong>{model.Body}</strong>");
                return Ok("Email envoyé avec succès");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de l'envoi de l'e-mail : {ex.Message}");
            }
        }
    }

    public class EmailModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
