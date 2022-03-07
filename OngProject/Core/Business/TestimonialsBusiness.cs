using Microsoft.Extensions.Configuration;
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
    public class TestimonialsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

       
        private readonly IConfiguration _configuration;
      
        private readonly EntityMapper entityMapper = new EntityMapper();
        public TestimonialsBusiness(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<Response<TestimonialsModel>> Delete(int id, string UserId)

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
            if (UserId == testimonials.Id.ToString())
            {
                TestimonialsModel entity = await _unitOfWork.TestimonialsModelRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                intermediate_list.Add("200");
                response.Errors = intermediate_list.ToArray();
                response.Data = entity;
                response.Succeeded = true;
                response.Message = "The Testimonial was Deleted successfully";
                return response;
            }
            intermediate_list.Add("403");
            response.Data = testimonials;
            response.Succeeded = false;
            response.Errors = intermediate_list.ToArray();
            response.Message = "You don't have permission for modificated this Testimonial";
            return response;
        }


    }
    }

