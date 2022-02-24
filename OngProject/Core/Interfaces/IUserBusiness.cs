using OngProject.Core.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IUserBusiness
    {
        bool Register(UserRegisterDto userRegisterDto);

    }
}
