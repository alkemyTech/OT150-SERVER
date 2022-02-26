﻿
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
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
        private readonly EntityMapper entityMapper = new EntityMapper();



        private readonly IEncryptHelper _encryptHelper;
        public UserBusiness(IUnitOfWork unitOfWork, IEmailBusiness emailBusiness, IEncryptHelper encryptHelper)
        {
            _unitOfWork = unitOfWork;


            _encryptHelper = encryptHelper;
        }


        public UserRegisterToDisplayDto Register(UserRegisterDto userRegisterDto)
        {

            var user = entityMapper.UserRegisterDtoToUserModel(userRegisterDto);
           


            user.Password = _encryptHelper.EncryptPassSha256(user.Password);

            _unitOfWork.UserModelRepository.Add(user);
            _unitOfWork.SaveChanges();


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
            foreach (var user in usuarios)
            {
                usuariosDto.Add(entityMapper.UserListDtoUserModel(user));
            }

            return usuariosDto;



        }

    }

}
