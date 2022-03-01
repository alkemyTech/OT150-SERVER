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
               SoftDelete=false
            };
        }

        public UserRegisterToDisplayDto UserRegisterDtoToUserRegisterToDisplayDto(UserRegisterDto userRegisterDto)
        {
            return new UserRegisterToDisplayDto()
            {
               Name = userRegisterDto.Name,
               LastName = userRegisterDto.LastName,
               Email = userRegisterDto.Email
            };
        }

        public UserDto UserListDtoUserModel(UserModel userDto)
        { 
            return new UserDto()
            {
                FirstName = userDto.FirstName,
                Email = userDto.Email,
                LastName = userDto.LastName
            };
        }

        public OrganizationGetDto OrganizationModeltoOrganizationGetDto(OrganizationModel organizationModel)
        {
            return new OrganizationGetDto()
            {
                Name = organizationModel.Name,
                Address = organizationModel.Address,
                Phone = organizationModel.Phone,
                Image = organizationModel.Image,
            };
        }

        public UserLoginToDisplayDto UserModelToUserLoginToDisplayDto(UserModel user)
        {
            return new UserLoginToDisplayDto()
            {
                Name = user.FirstName,
                LastName = user.LastName,
                Email = user.Email

            };
        }

        public CategorieDto CategorieListDtoCategorieModel(CategorieModel categorieDto)

        {


            return new CategorieDto()
            {

                NameCategorie = categorieDto.NameCategorie,

            };

        }

        public CommentDto CommentModelToCommentDto(CommentModel comment)
        {
            return new CommentDto()
            {
                Body = comment.Body,
                User_Id = comment.User_Id
            };
        }

        public SlideDto SlideModelToSlideDto(SlideModel mono)
        {
            return new SlideDto()
            {
                ImageUrl = mono.ImageUrl,
                Text = mono.Text,
                Order = mono.Order,
                Organization_Id = mono.Organization_Id
            };
        }
    }
      
}
        


