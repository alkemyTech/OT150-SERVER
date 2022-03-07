using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IUserBusiness
    {
        Task<UserRegisterToDisplayDto> Register(UserRegisterDto userRegisterDto);
        bool ValidationEmail(string emailAddress);
        List<UserDto> GetUsuarios();

        UserLoginToDisplayDto Login(string email, string password);
        UserDto GetById(int id);
    }
}
