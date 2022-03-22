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
    public class ActivitiesControllerTest
    {
        private IActivityBusiness activitiesBusiness;
        private ActivityController activitiesController;

        [TestInitialize]
        public void Init()
        {
            ContextHelper.MakeDbContext();
            ContextHelper.MakeContext();
            activitiesBusiness = new ActivityBusiness(ContextHelper.unitOfWork, ContextHelper.entityMapper, ContextHelper.configuration);
            activitiesController = new ActivityController(activitiesBusiness);
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
        public async Task Post_ValidEntity_200Code()
        {
            var newActivity = new ActivityDto
            {
                Name = "Activity name",
                Content = "Activity content",
                Image = "Activity Image"
            };
            
            var response = activitiesController.Activities(newActivity);
            var result = response as ObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task Put_ValidEntity_200Code()
        {
            var newActivity = new ActivityUpdateDto
            {
                Name = "Activity name",
                Content = "Activity content",
                Image = CreateImage(),
            };

            var response = await activitiesController.Update(1, newActivity);
            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task Put_EntityNotFound_404Code()
        {
            var newActivity = new ActivityUpdateDto
            {
                Name = "Activity name",
                Content = "Activity content",
                Image = CreateImage(),
            };

            var response = await activitiesController.Update((-1), newActivity);
            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
