using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;

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
    }
}
