using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Test.Helper;

namespace Test
{
    [TestClass]
    public class UserControllerTest
    {
        private IUserBusiness userBusiness;
        private UserController userController;

        [TestInitialize]
        public void Init()
        {
            ContextHelper.MakeDbContext();
            ContextHelper.MakeContext();
            userBusiness = new UserBusiness(ContextHelper.unitOfWork, ContextHelper.emailBusiness, ContextHelper.encryptHelper, ContextHelper.configuration);
            userController = new UserController(userBusiness, ContextHelper.configuration);
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
        public async Task GetAllUsersSuccessfully()
        {

            var response = userController.Lista();
            var objectResult = await response as ObjectResult;


            Assert.IsNotNull(objectResult);
            Assert.AreEqual(200, objectResult.StatusCode);
        }

        [TestMethod]
        public async Task PutUserSuccessfully()
        {
            var userUpdateDto = new UserUpdateDto
            {
                FirstName = "Name User",
                LastName = "Last Name User",
                Password = "Password",
                Photo = CreateImage()
            };

            var response = await userController.Update(1, userUpdateDto);
            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task PutUserFailedInvalidEntity()
        {
            var userUpdateDto = new UserUpdateDto
            {
                FirstName = "Name User",
                LastName = "Last Name User",
                Password = "Password",
                Photo = CreateImage()
            };

            var response = await userController.Update(-5, userUpdateDto);
            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [TestMethod]
        public async Task UserDeletedSuccessfully()
        {
            var id = 1;

            var response = await userController.Delete(id);
            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

        }

        [TestMethod]
        public async Task UserDeleteFailed()
        {
            var id = -5;

            var response = await userController.Delete(id);
            var result = response as ObjectResult;

            Assert.AreEqual(404, result.StatusCode);
        }
    }
}
