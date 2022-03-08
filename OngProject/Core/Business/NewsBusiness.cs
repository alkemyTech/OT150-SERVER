using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper entityMapper = new EntityMapper();

        public NewsBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}