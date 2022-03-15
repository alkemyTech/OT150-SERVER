using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace OngProject.Core.Business
{
    public class SlideBusiness : ISlideBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _entityMapper;
        private readonly IConfiguration _configuration;
        private readonly ImagesBusiness _imagesBusiness;
        

        public SlideBusiness(IUnitOfWork unitOfWork, EntityMapper entityMapper, IConfiguration configuration, ImagesBusiness imagesBusiness)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
            _configuration = configuration;
            _imagesBusiness = imagesBusiness;
           
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
        public async Task<Response<SlideDtoToDisplay>>Post(SlidePostDto slidePostDto)
        {
            var slide = new SlideModel();
            var response = new Response<SlideDtoToDisplay>();
        
            var errores = new List<string>();
            var organizations = _unitOfWork.OrganizationModelRepository.GetAll();

            if (!organizations.Any(x => x.Id == slidePostDto.OrganizationId))
            {
                errores.Add("The organization not found");
                response.Errors = errores.ToArray();
                response.Succeeded = false;
                response.Message = "The slide was not created";
                response.Data = null;
                return response;

            }
            slide = _entityMapper.SlidePostDtoToSlideModel(slidePostDto);
            if (slidePostDto.Order == 0 || slidePostDto.Order==null)
            {
                var last=_unitOfWork.SlideModelRepository.GetAll().Last();
              
                slide.Order = last.Order + 1;
            }

        
            
           
            
            try
            {
                if (slidePostDto.Image != null)
                {
                    slide.ImageUrl = await UploadBase64ImageToBucket(slidePostDto.Image);
                }
            }
            catch (Exception ex)
            {
                errores.Add(ex.ToString());
                response.Data = null;
                response.Errors = errores.ToArray();
                response.Succeeded = false;
                response.Message = "The slide was not created.";

                return response;
            }
            _unitOfWork.SlideModelRepository.Add(slide);
           await _unitOfWork.SaveChangesAsync();
            response.Data = _entityMapper.SlideModelToSlideDtoToDisplay(slide);
            response.Succeeded = true;
            response.Message = "The slide was created";
            response.Errors = null;
            return response;

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
        private async Task<string> UploadBase64ImageToBucket(string base64)
        {
            string newName = $"{Guid.NewGuid()}_user";

            int indexOfSemiColon = base64.IndexOf(";", StringComparison.OrdinalIgnoreCase);
            string dataLabel = base64.Substring(0, indexOfSemiColon);
            string contentType = dataLabel.Split(':').Last();
            var startIndex = base64.IndexOf("base64,", StringComparison.OrdinalIgnoreCase) + 7;
            var fileContents = base64.Substring(startIndex);

            var formFileModel = new FormFileModel()
            {
                FileName = newName,
                ContentType = contentType,
                Name = newName
            };
            byte[] imageBinaryFile = Convert.FromBase64String(fileContents);
    
            MemoryStream stream = new MemoryStream(imageBinaryFile);
            
            IFormFile file = new FormFile(stream, 0, imageBinaryFile.Length, formFileModel.Name, formFileModel.FileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = formFileModel.ContentType
            };
           
            return await _imagesBusiness.UploadFileAsync(file);
        }
    }
}

