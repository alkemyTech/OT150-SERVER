using Microsoft.Extensions.Configuration;
using OngProject.Core.Business;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {



        public UserModel UserRegisterDtoToUserModel(UserRegisterDto userRegisterDTO)
        {




            return new UserModel()
            {
                FirstName = userRegisterDTO.Name,
                LastName = userRegisterDTO.LastName,
                Email = userRegisterDTO.Email,
                Password = userRegisterDTO.Password,
                LastModified = DateTime.Now,
                SoftDelete = true,
                RoleId=userRegisterDTO.Role
             
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
        public ContactDto ConctactListDtoContactModel(ContactsModel contactDto)
        {
            return new ContactDto()
            {
                Name = contactDto.Name,
                Email = contactDto.Email,
                Phone = contactDto.Phone,
                Message = contactDto.Message
            };
        }
        public MemberDto MemberListDtoMemberModel(MemberModel memberDto)
        {
            return new MemberDto()
            {
                Name = memberDto.Name,
                Image = memberDto.Image,
                InstagramUrl = memberDto.InstagramUrl,
                LinkedinUrl = memberDto.LinkedinUrl,
                FacebookUrl = memberDto.FacebookUrl,
                Description = memberDto.Description

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
                Id = user.Id,
                Name = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId
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
        public SlideDto SlideListDtoSlideModelImageOrder(SlideModel slideDto)
        {
            return new SlideDto()
            {
                ImageUrl = slideDto.ImageUrl,
                Order = slideDto.Order
            };

        }
        public CategoryGetDto CategorieModelToCategorieGetDto(CategorieModel categorieModel)
        {
            return new CategoryGetDto()
            {
                NameCategorie = categorieModel.NameCategorie,
                DescriptionCategorie = categorieModel.DescriptionCategorie,
                Image = categorieModel.Image
            };
        }
        public NewsDto NewsModeltoNewsDto(NewsModel newsModel)
        {
            return new NewsDto()
            {
                Name = newsModel.Name,
                Content = newsModel.Content,
                Image = newsModel.Image
            };
        }
    }

}



