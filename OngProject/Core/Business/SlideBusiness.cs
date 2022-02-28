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
            var Slides = _unitOfWork.SlideModelRepository.GetAll();
            var SlidesDto = new List<SlideDto>();
            foreach (var ASlide in Slides)
            {
                SlidesDto.Add(_entityMapper.SlideListDtoSlideModel(ASlide));
            }
            return SlidesDto;
        }
    }
}
