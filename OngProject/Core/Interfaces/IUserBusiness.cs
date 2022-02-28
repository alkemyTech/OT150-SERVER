using OngProject.Core.Models.DTOs;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IUserBusiness
    {
        Task<UserRegisterToDisplayDto> Register(UserRegisterDto userRegisterDto);
        bool ValidationEmail(string emailAddress);

        UserLoginToDisplayDto Login(string email, string password);
    }
}
