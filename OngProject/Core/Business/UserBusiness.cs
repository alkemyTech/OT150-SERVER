
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
       
        private readonly IEmailBusiness _emailBusiness;
        private readonly IConfiguration _configuration;
        private readonly IEncryptHelper _encryptHelper;
        private readonly EntityMapper entityMapper=new EntityMapper();
        public UserBusiness(IUnitOfWork unitOfWork, IEmailBusiness emailBusiness, IEncryptHelper encryptHelper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _emailBusiness = emailBusiness;

            _encryptHelper = encryptHelper;
            _configuration = configuration;
        }

        public UserLoginToDisplayDto Login(string email, string password)
        {
            var encrypted = _encryptHelper.EncryptPassSha256(password);

            var users = _unitOfWork.UserModelRepository.GetAll();

            var user = users.Where(user => user.Email.Equals(email) && user.Password.Equals(encrypted)).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var roleName = _unitOfWork.RoleModelRepository.GetById(user.RoleId).NameRole;

            var userDto = entityMapper.UserModelToUserLoginToDisplayDto(user);

            userDto.Role = roleName;

            return userDto;
        }

        public async Task<UserRegisterToDisplayDto> Register(UserRegisterDto userRegisterDto)
        {
            var imagesBusiness = new ImagesBusiness(_configuration);

            userRegisterDto.Password = _encryptHelper.EncryptPassSha256(userRegisterDto.Password);

            if(userRegisterDto.Role!=1 && userRegisterDto.Role != 2)
            {
                userRegisterDto.Role = 2;
            }
            var user = entityMapper.UserRegisterDtoToUserModel(userRegisterDto);
            if (userRegisterDto.Photo != null)
            {
                user.Photo = await imagesBusiness.UploadFileAsync(userRegisterDto.Photo);
            }

            _unitOfWork.UserModelRepository.Add(user);
            _unitOfWork.SaveChanges();
            await _emailBusiness.SendEmailWithTemplateAsync(userRegisterDto.Email,$"Bienvenido a esta gran comunidad",$"Gracias por registrarte {user.FirstName}","Ong Somos Más");
            return entityMapper.UserRegisterDtoToUserRegisterToDisplayDto(userRegisterDto);
        }

        public bool ValidationEmail(string emailAddress)
        {
            var users = _unitOfWork.UserModelRepository.GetAll();
            if (users.Any(x => x.Email == emailAddress))
            {
                return false;
            }

            return true;
        }

        public List<UserDto> GetUsuarios()
        {
            var usuarios = _unitOfWork.UserModelRepository.GetAll();
            var usuariosDto = new List<UserDto>();
            foreach (var user in usuarios )
            {
                usuariosDto.Add(entityMapper.UserListDtoUserModel(user));
            }

            return usuariosDto;          
        }

        public UserDto GetById(int id)
        {
            var user = _unitOfWork.UserModelRepository.GetById(id);

            var userDto = entityMapper.UserListDtoUserModel(user);

            return userDto;
        }

    }

}
