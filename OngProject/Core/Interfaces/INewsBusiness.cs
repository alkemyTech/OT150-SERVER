using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        NewsDto GetNews(int id);
        Task<Response<NewsModel>> NewsPost(NewsPostDto newsPost);
    }
}