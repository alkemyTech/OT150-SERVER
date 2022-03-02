using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class SlideBusiness : ISlideBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _entityMapper;
        public SlideBusiness(IUnitOfWork unitOfWork, EntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }


        public SlideDto showDetailSlide(int id)
        {
            SlideDto detalleDto;
            var detalle = _unitOfWork.SlideModelRepository.GetById(id);
            if (detalle == null)
                detalleDto = null;
            else detalleDto = _entityMapper.SlideModelToSlideDto(detalle);
            return detalleDto;
        }
    }
}
