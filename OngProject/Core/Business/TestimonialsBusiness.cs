using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class TestimonialsBusiness:ITestimonialsBussines
    {
        private readonly IUnitOfWork _unitOfWork;


        private readonly IConfiguration _configuration;

        private readonly EntityMapper entityMapper = new EntityMapper();
        
        public TestimonialsBusiness(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<Response<TestimonialsPostToDisplayDto>> Post(TestimonialsPostDto testimonialPostDto)
        {
            var TestimonialsModel = entityMapper.TestimonialsPostDtoToTestimonialsModel(testimonialPostDto);
           
            var response = new Response<TestimonialsPostToDisplayDto>();
            var imagesBusiness = new ImagesBusiness(_configuration);
            string image;
            var TestimonialDisplay = entityMapper.TestimonialsPostDtoToTestimonialsPostToDisplayDto(testimonialPostDto);
            if (testimonialPostDto.Image != null)
            {
                image = await imagesBusiness.UploadFileAsync(testimonialPostDto.Image);
                TestimonialsModel.Image = image;
                TestimonialDisplay.Image = image;
            }
         
            _unitOfWork.TestimonialsModelRepository.Add(TestimonialsModel);
            await _unitOfWork.SaveChangesAsync();
            response.Succeeded = true;
            response.Message = "The testimonial was added";
          
            response.Data = TestimonialDisplay;
            
            return response;
        }

        
    }
}