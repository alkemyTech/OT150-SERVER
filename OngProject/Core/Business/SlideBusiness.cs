using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;

namespace OngProject.Core.Business
{
    public class SlideBusiness : ISlide
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _entityMapper;

        public SlideBusiness(IUnitOfWork UnitOfWork, EntityMapper EntityMapper)
        {
            _unitOfWork = UnitOfWork;
            _entityMapper = EntityMapper;
        }
        public IEnumerable<SlideDto> GetSlides()
        {
            var slides = _unitOfWork.SlideModelRepository.GetAll();
            var slidesDto = new List<SlideDto>();
            foreach (var slide in slides)
            {
                slidesDto.Add(_entityMapper.SlideListDtoSlideModel(slide));
            }
            return slidesDto;
        }
    }
}
