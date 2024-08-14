using System.Net.Mail;
using System.Net;
using webapiG2T.Services.Interfaces;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace webapiG2T.Services.Implementations
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("doumzoro@gmail.com", "Assane Doumbouya");
            var to = new EmailAddress(toEmail);

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("L'envoi de l'e-mail a échoué." + response.StatusCode);
            }
        }
    }
}
