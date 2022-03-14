using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IEmailBusiness _emailBusiness;
        private readonly IConfiguration _configuration;
        private readonly IEncryptHelper _encryptHelper;
        private readonly EntityMapper entityMapper = new EntityMapper();
        private readonly IJwtHelper _jwtHelper;
        public UserBusiness(IUnitOfWork unitOfWork, IEmailBusiness emailBusiness, IEncryptHelper encryptHelper, IConfiguration configuration, IJwtHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _emailBusiness = emailBusiness;

            _encryptHelper = encryptHelper;
            _configuration = configuration;
            _jwtHelper = jwtHelper;
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

            var roleName = _unitOfWork.RoleModelRepository.GetById(user.RoleId);

            var userDto = entityMapper.UserModelToUserLoginToDisplayDto(user);

            if (roleName != null)
            {
                userDto.Role = roleName.NameRole;
            }
            else
            {
                return null;
            }

            return userDto;
        }

        public async Task<UserRegisterToDisplayDto> Register(UserRegisterDto userRegisterDto)
        {
            var imagesBusiness = new ImagesBusiness(_configuration);

            var roleName = _unitOfWork.RoleModelRepository.GetById(userRegisterDto.RoleId);

            var userDto = entityMapper.UserRegisterDtoToUserRegisterToDisplayDto(userRegisterDto);

            if (roleName != null)
            {
                userDto.Role = roleName.NameRole;
            }
            else
            {
                return null;
            }

            userRegisterDto.Password = _encryptHelper.EncryptPassSha256(userRegisterDto.Password);


           
            var user = entityMapper.UserRegisterDtoToUserModel(userRegisterDto);
            if (userRegisterDto.Photo != null)
            {
                user.Photo = await imagesBusiness.UploadFileAsync(userRegisterDto.Photo);
            }

            _unitOfWork.UserModelRepository.Add(user);
            _unitOfWork.SaveChanges();
            await _emailBusiness.SendEmailWithTemplateAsync(userRegisterDto.Email, $"Bienvenido a esta gran comunidad", $"Gracias por registrarte {user.FirstName}", "Ong Somos Más");
            return userDto;
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

        public UserDto GetById(int id)
        {
            var user = _unitOfWork.UserModelRepository.GetById(id);

            var userDto = entityMapper.UserListDtoUserModel(user);

            return userDto;
        }

        public async Task<Response<UserDto>> DeleteUser(int id)
        {
            var users = _unitOfWork.UserModelRepository.GetAll();
            var response = new Response<UserDto>();
            if (!users.Any(x => x.Id == id))
            {
                var list = new List<string>();
                list.Add("This user not found");
                response.Errors = list.ToArray();
                response.Data = null;

                response.Succeeded = false;

                return response;

            }

            var user = GetById(id);
            await _unitOfWork.UserModelRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            response.Data = user;

            response.Succeeded = true;
            response.Message = "The user was deleted";

            return response;
        }

        public async Task<Response<UserDto>> UpdateUser(int id, UserUpdateDto userUpdate)
        {
            var imagesBussines = new ImagesBusiness(_configuration);
            var response = new Response<UserDto>();
            var errorList = new List<string>();
            string photo;
            var User = await _unitOfWork.UserModelRepository.GetByIdAsync(id);

            if (User == null)
            {
                errorList.Add("404");
                response.Data = null;
                response.Errors = errorList.ToArray();
                response.Succeeded = false;
                return response;
            }
            if (userUpdate.Photo != null)
            {
                photo = await imagesBussines.UploadFileAsync(userUpdate.Photo);
                User.Photo = photo;
            }
            if (userUpdate.FirstName != null)
            {
                User.FirstName = userUpdate.FirstName;
            }
            if (userUpdate.LastName != null)
            {
                User.LastName = userUpdate.LastName;
            }
            if (userUpdate.Password != null)
            {
                var encrypted = _encryptHelper.EncryptPassSha256(userUpdate.Password);
                User.Password = encrypted;
            }
            _unitOfWork.UserModelRepository.Update(User);
            await _unitOfWork.SaveChangesAsync();

            var user = await _unitOfWork.UserModelRepository.GetByIdAsync(id);
            var userDto = entityMapper.UserListDtoUserModel(user);
            response.Data = userDto;
            response.Succeeded = true;
            return response;
        }
    }

}
