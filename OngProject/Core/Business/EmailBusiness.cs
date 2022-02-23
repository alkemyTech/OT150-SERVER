using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class EmailBusiness : IEmailBusiness
    {
        private readonly IConfiguration configuration;

        public EmailBusiness (IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task SendEmail(string email)
        {
            var apiKey = configuration.GetSection("ApiKey").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("grupo150csharp@gmail.com", "Prueba");
            var to = new EmailAddress(email);
            var subject = "Bienvenido";
            var htmlContent = "<h1> Mail de bienvenida</h1>";
            var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
