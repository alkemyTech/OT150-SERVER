using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IEmailBusiness
    {
        Task SendEmail(string email);
    }
}
