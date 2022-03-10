using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper entityMapper = new EntityMapper();
        private readonly IConfiguration _configuration;

        public NewsBusiness(IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public NewsDto GetNews(int id)
        {
            var New = _unitOfWork.NewsModelRepository.GetById(id);
            return entityMapper.NewsModeltoNewsDto(New);
        }
        public Response<NewsModel> NewsPost(NewsPostDto newsPost)
        {
            Response<NewsModel> response = new Response<NewsModel>();
            List<string> intermediate_list = new List<string>();
            var news = entityMapper.NewsPostDtoToNewsModel(newsPost);
            var category = _unitOfWork.CategorieModelRepository.GetById(news.CategorieId);
            if (category == null)
            {
                intermediate_list.Add("500");
                response.Errors = intermediate_list.ToArray();
                response.Data = news;
                response.Succeeded = false;
                response.Message = "The CategoryId Don´t exist";
                return response;
            }
            _unitOfWork.NewsModelRepository.Add(news);
            _unitOfWork.SaveChanges();
            intermediate_list.Add("200");
            response.Errors = intermediate_list.ToArray();
            response.Data = news;
            response.Succeeded = true;
            response.Message = "The News was Posted successfully";
            return response;

        }

        public async Task<Response<NewsDto>> Update(int id, NewsUpdateDto newsUpdate)
        {
                var imagesBussines = new ImagesBusiness(_configuration);
                var response = new Response<NewsDto>();
                var errorList = new List<string>();
                string image;
                var news = _unitOfWork.NewsModelRepository.GetById(id);

                if (news == null)
                {
                    errorList.Add("This news not found");
                    response.Data = null;
                    response.Errors = errorList.ToArray();
                    response.Succeeded = false;
                    return response;
                }
                if (newsUpdate.Image != null)
                {
                    image = await imagesBussines.UploadFileAsync(newsUpdate.Image);
                   news.Image = image;
                }
                if (newsUpdate.Content != null)
                {
                     news.Content = newsUpdate.Content;
                }


                if (newsUpdate.Name != null)
                {
                news.Name = newsUpdate.Name;
                }
                _unitOfWork.NewsModelRepository.Update(news);
                await _unitOfWork.SaveChangesAsync();

                var updatedNews = _unitOfWork.NewsModelRepository.GetById(id);
                response.Data = entityMapper.NewsModeltoNewsDto(updatedNews);
                response.Succeeded = true;
                return response;
            }
        }
    }
    
