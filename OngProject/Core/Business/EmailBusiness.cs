using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class EmailBusiness : IEmailBusiness
    {
        private readonly IConfiguration configuration;

        public EmailBusiness(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task SendEmailWithTemplateAsync(string ToEmail, string mailTitle, string mailBody, string mailContact)
        {
            
            string template = File.ReadAllText(configuration["MailParams:PathTemplate"]);
            template = template.Replace(configuration["MailParams:ReplaceMailTitle"], mailTitle);
            template = template.Replace(configuration["MailParams:ReplaceMailBody"], mailBody);
            template = template.Replace(configuration["MailParams:ReplaceMailContact"], mailContact);
            await SendEmailAsync(ToEmail, mailTitle, template);
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(configuration["MailParams:SendGridKey"], subject, message, email);
        }
        //
        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(configuration["MailParams:FromMail"], configuration["MailParams:FromMailDescription"]),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);
            return client.SendEmailAsync(msg);
        }
    }
}

