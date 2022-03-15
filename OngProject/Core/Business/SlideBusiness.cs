using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class SlideBusiness : ISlideBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _entityMapper;
        private readonly IConfiguration _configuration;
        public SlideBusiness(IUnitOfWork unitOfWork, EntityMapper entityMapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
            _configuration = configuration;
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
        public IEnumerable<SlideDto> GetSlides()
        {
            var slides = _unitOfWork.SlideModelRepository.GetAll();
            var slidesDto = new List<SlideDto>();
            foreach (var slide in slides)
            {
                slidesDto.Add(_entityMapper.SlideListDtoSlideModelImageOrder(slide));
            }
            return slidesDto;
        }

        public async Task<Response<SlideDto>> Update(int id, SlidePutDto slideDto)
        {
            var imagesBussines = new ImagesBusiness(_configuration);
            var response = new Response<SlideDto>();
            var errorList = new List<string>();
            string image;
            var slideUpdate = _unitOfWork.SlideModelRepository.GetById(id);

            if (slideUpdate == null)
            {
                errorList.Add("Slide not found");
                response.Data = null;
                response.Errors = errorList.ToArray();
                response.Succeeded = false;
                return response;
            }
            if (slideDto.ImageUrl != null)
            {
                image = await imagesBussines.UploadFileAsync(slideDto.ImageUrl);
                slideUpdate.ImageUrl = image;
            }

            slideUpdate.Order = slideDto.Order;
            slideUpdate.OrganizationId = slideDto.OrganizationId;
            slideUpdate.Text = slideDto.Text;

            _unitOfWork.SlideModelRepository.Update(slideUpdate);
            await _unitOfWork.SaveChangesAsync();
            
            response.Data = _entityMapper.SlideModelToSlideDto(slideUpdate);
            response.Succeeded = true;
            return response;
        }
        public async Task<Response<SlideDtoToDisplay>> Delete(int id)
        {
            var slides = _unitOfWork.SlideModelRepository.GetAll();

            Response<SlideDtoToDisplay> response = new Response<SlideDtoToDisplay>();

            var slide = slides.FirstOrDefault(x => x.Id == id);
            var error = new List<string>();
            if (slide == null || slide.SoftDelete == false)
            {

                error.Add("This slide not found");
                response.Data = null;
                response.Succeeded = false;
                response.Message = "The deletion was not successfully";
                response.Errors = error.ToArray();

            }
            else
            {
                var slideDeleted = await _unitOfWork.SlideModelRepository.Delete(id);
                _unitOfWork.SaveChanges();
                var slideAux = _entityMapper.SlideModelToSlideDto(slideDeleted);
                response.Data = _entityMapper.SlideDtoToSlideDtoToDisplay(slideAux);
                response.Succeeded = true;
                response.Message = "The slide has been successfully deleted";

            }
            return response;
        }
    }
}

