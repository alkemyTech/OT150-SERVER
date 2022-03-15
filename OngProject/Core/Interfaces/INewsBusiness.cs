using OngProject.Core.Helper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface INewsBusiness
    {
        PagedList<NewsDto> GetAllNews(PaginationParams paginationParams);
        NewsDto GetNews(int id);
        Response<NewsModel> NewsPost(NewsPostDto newsPost);
        Task<Response<NewsDto>> Update(int id, NewsUpdateDto newsUpdate);
        Task<Response<NewsModel>> DeleteNews(int id);
    }
}