using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {

        public UserModel UserRegisterDtoToUserModel(UserRegisterDto userRegisterDTO)
        {
            return new UserModel()
            {
               FirstName=userRegisterDTO.Name,
               LastName=userRegisterDTO.LastName,
               Email=userRegisterDTO.Email,
               Password=userRegisterDTO.Password,
               LastModified=DateTime.Today,
               SoftDelete=false,
             

            };
            
        }

        public UserRegisterToDisplayDto UserRegisterDtoToUserRegisterToDisplayDto(UserRegisterDto userRegisterDto)
        {
            return new UserRegisterToDisplayDto(){
                Name = userRegisterDto.Name,
               LastName = userRegisterDto.LastName,
               Email = userRegisterDto.Email
                    };
        
    }
    }
}
