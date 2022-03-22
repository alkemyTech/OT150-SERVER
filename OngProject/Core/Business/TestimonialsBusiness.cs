using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper;
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

        public PagedList<TestimonialsDto> GetAllTestimonials(PaginationParams paginationParams)
        {
            var testimonials = _unitOfWork.TestimonialsModelRepository.GetAll();
            var testimonialsDto = new List<TestimonialsDto>();
            foreach (var c in testimonials)
            {
                testimonialsDto.Add(entityMapper.TestimonialsModelToTestimonialsDto(c));
            }
            
            var pagedNews = PagedList<TestimonialsDto>.Create(testimonialsDto, paginationParams.PageNumber, paginationParams.PageSize);
          
            return pagedNews;
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



        public async Task<Response<TestimonialsModel>> Delete(int id)

        {
            TestimonialsModel testimonials = _unitOfWork.TestimonialsModelRepository.GetById(id);
            var response = new Response<TestimonialsModel>();
            List<string> intermediate_list = new List<string>();
            if (testimonials == null)
            {
                intermediate_list.Add("404");
                response.Data = testimonials;
                response.Message = "This Testimonial not Found";
                response.Succeeded = false;
                response.Errors = intermediate_list.ToArray();
                return response;

            }
          
                TestimonialsModel entity = await _unitOfWork.TestimonialsModelRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                response.Errors = null;
                response.Data = entity;
                response.Succeeded = true;
                response.Message = "The Testimonial was Deleted successfully";
                return response;
            
          
        
        }

        public async Task<Response<TestimonialsModel>> PutTestimonials(int id,TestimonialsPutDto testimonialsDto)
        {
            ImagesBusiness images=new ImagesBusiness(_configuration);
            Response<TestimonialsModel> response = new Response<TestimonialsModel>();
            List<string> intermediate_list = new List<string>();
            var testimonials = _unitOfWork.TestimonialsModelRepository.GetById(id);
            if (testimonials == null)
            {
                intermediate_list.Add("404");
                response.Data = testimonials;
                response.Message = "This testimonial not Found";
                response.Succeeded = false;
                response.Errors = intermediate_list.ToArray();
                return response;
            }
            if (testimonialsDto.Name != null)
                testimonials.Name = testimonialsDto.Name;
            if (testimonialsDto.Image != null)
                testimonials.Image = await images.UploadFileAsync(testimonialsDto.Image);
            if (testimonialsDto.Content != null)
                testimonials.Content = testimonialsDto.Content;
           
            testimonials.LastModified = DateTime.Now;
            _unitOfWork.TestimonialsModelRepository.Update(testimonials);
            
            response.Data = testimonials;
            response.Message = "The Testimonials was successfully Updated";
            response.Succeeded = true;
            response.Errors = null;
            return response;
        }

    }
}
