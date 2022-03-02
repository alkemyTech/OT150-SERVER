using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IEmailBusiness
    {
        Task SendEmailWithTemplateAsync(string ToEmail, string mailTitle, string mailBody, string mailContact);
        Task SendEmailAsync(string email, string subject, string message);
        Task Execute(string apiKey, string subject, string message, string email);
    }
}
