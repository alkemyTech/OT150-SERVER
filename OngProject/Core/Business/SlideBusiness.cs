using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class SlideBusiness : ISlide
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _entityMapper;

        public SlideBusiness(IUnitOfWork unitOfWork, EntityMapper EntityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = EntityMapper;
        }

        public SlideDto GetById(int id)
        {
            var slide = _unitOfWork.SlideModelRepository.GetById(id);
            var slideDto= _entityMapper.SlideModelToSlideDtoDetail(slide);
            return slideDto; 
        }
    }
}
