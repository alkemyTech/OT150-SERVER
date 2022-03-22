using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class testimonialsControllerTest
    {
        private ITestimonialsBussines testimonialsBusiness;
        private TestimonialsController testimonialsController;
       
      

        [TestInitialize()]
        public void Init()
        {
        
            ContextHelper.MakeDbContext();
            ContextHelper.MakeContext();
            testimonialsBusiness = new TestimonialsBusiness(ContextHelper.unitOfWork, ContextHelper.configuration);
            testimonialsController = new TestimonialsController(ContextHelper.unitOfWork, testimonialsBusiness,new UriService("https://localhost:44353/"));
                  
        }

        private IFormFile CreateImage()
        {
            var imageCurrentPath = Directory.GetCurrentDirectory();
            var index = imageCurrentPath.IndexOf("Test\\");
            var finalPath = imageCurrentPath.Substring(0, index + 4) + "\\Image\\Captura1.png";
            var imageFile = File.OpenRead(finalPath);

            IFormFile newFile = new FormFile(imageFile, 0, imageFile.Length, "Captura1", "Captura1.png")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
            return newFile;
        }
      
       
        
        [TestMethod]
        public async Task GetAllTestimonialsSuccessfullyTest()
        {
            
            PaginationParams paginationParams = new PaginationParams();
            paginationParams.PageNumber = 1;
            paginationParams.PageSize = 5;
            var response = testimonialsController.GetTestimonials(paginationParams);
            var objectResult = response as ObjectResult;

          
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
           
        }
        

        [TestMethod]
        public async Task Post_ValidEntity_200Code()
        {
            var newtestimonial = new TestimonialsPostDto
            {
                Name = "testimonial name",
                Content = "testimonial content",
                Image = CreateImage()
            };

            var response = testimonialsController.Post(newtestimonial);
            var result = await response as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }
        [TestMethod]
        public async Task Put_ValidEntity_200Code()
        {
            var newtestimonial = new TestimonialsPutDto
            {
                Name = "testimonial name 1",
                Content = "testimonial content 1",
                Image = CreateImage(),
                
            
            };

            var response =  await testimonialsController.Put(1,newtestimonial);
            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task Put_EntityNotFound_404Code()
        {
            
            var newtestimonial = new TestimonialsPutDto
            {
                Name = "testimonial name",
                Content = "testimonial content",
                Image = CreateImage(),
               
            };

            var response = await testimonialsController.Put((-1),newtestimonial);
            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        
        }
        [TestMethod]
        public async Task DeleteTestimonialSuccessfullyTest()
        {
            // Arrange


            var id = 1;
            // Act
            var response = await testimonialsController.Delete(id);
            var result = response as ObjectResult;
            var checkDelete = await testimonialsController.Delete(id);
            var result1 = checkDelete as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(404, result1.StatusCode);


        }
        [TestMethod]
        public async Task DeleteTestimonialUnSuccessfullyTest()
        {
            // Arrange


            var id = -5;

            // Act
            var response = await testimonialsController.Delete(id);
            var result = response as ObjectResult;

            // Assert

            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
