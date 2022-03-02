using OngProject.Core.Models.DTOs;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        NewsDto GetNews(int id);
    }
}