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

                RoleId = userRegisterDTO.RoleId,


            };
        }

        public UserRegisterToDisplayDto UserRegisterDtoToUserRegisterToDisplayDto(UserRegisterDto userRegisterDto)
        {
            return new UserRegisterToDisplayDto()
            {

                Name = userRegisterDto.Name,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,


            };

        }

        public ContactDto ConctactListDtoContactModel(ContactsModel contactDto)
        {
            return new ContactDto()
            {
                Name = contactDto.Name,
                Email = contactDto.Email,
                Phone = contactDto.Phone,
                Message = contactDto.Message,


            };
        }
        public ContactsModel ContactPostDtoToContactsModel(ContactPostDto contactPostDto)
        {
            return new ContactsModel()
            {
                Name = contactPostDto.Name,
                Email = contactPostDto.Email,
                Phone = contactPostDto.Phone,
                Message = contactPostDto.Message,
                LastModified = DateTime.Now,
                SoftDelete = true
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

        public MemberModel MemberDtoToMemberModel(MemberDto memberDto)
        {
            return new MemberModel()
            {
                Name = memberDto.Name,
                FacebookUrl = memberDto.FacebookUrl,
                InstagramUrl = memberDto.InstagramUrl,
                LinkedinUrl = memberDto.LinkedinUrl,
                Description = memberDto.Description
            };
        }

        public MemberDeleteDto MemberModelToMemberDeleteDto(MemberModel memberDto)
        {
            return new MemberDeleteDto()
            {
                Name = memberDto.Name,
                Description = memberDto.Description,
                Image = memberDto.Image
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
                FacebooK = organizationModel.FacebooK,
                Linkedin = organizationModel.Linkedin,
                Instagram = organizationModel.Instagram
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
                User_Id = comment.UserId

            };
        }


        public TestimonialsDto TestimonialsModelToTestimonialsDto(TestimonialsModel testimonials)
        {
            return new TestimonialsDto()
            {

                Id = testimonials.Id

            };
        }


        public CommentModel CommentPostDtoToCommentModel(CommentPostDto commentPost)
        {
            return new CommentModel()
            {
                NewsId = commentPost.NewsId,


                Body = commentPost.Body,
                LastModified = DateTime.Now,
                SoftDelete = true


            };
        }

        public SlideDto SlideModelToSlideDto(SlideModel mono)
        {
            return new SlideDto()
            {
                ImageUrl = mono.ImageUrl,
                Text = mono.Text,
                Order = mono.Order,
                OrganizationId = mono.OrganizationId
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
        public SlideModel SlidePostDtoToSlideModel(SlidePostDto slidePostDto)
        {
            return new SlideModel()
            {
                Text=slidePostDto.Text,
           
                LastModified= DateTime.Now,
                OrganizationId=slidePostDto.OrganizationId,
                SoftDelete=true
            };

        }
        public SlideDtoToDisplay SlideModelToSlideDtoToDisplay(SlideModel slideModel)
        {
            return new SlideDtoToDisplay()
            {
                Image=slideModel.ImageUrl,
                Order=slideModel.Order
            };

        }
        public SlideDtoToDisplay SlideDtoToSlideDtoToDisplay(SlideDto slideDto)
        {
            return new SlideDtoToDisplay()
            {
                Image = slideDto.ImageUrl,
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


        public ActivityModel ActivityDtoToActivityModel(ActivityDto activityDto)
        {
            return new ActivityModel()
            {
                Name = activityDto.Name,
                Content = activityDto.Content,
                Image = activityDto.Image
            };
        }


        public TestimonialsModel TestimonialsPostDtoToTestimonialsModel(TestimonialsPostDto testimonialsPostDto)
        {
            return new TestimonialsModel()
            {
                Name = testimonialsPostDto.Name,
                Content = testimonialsPostDto.Content,

            };
        }

        public TestimonialsPostToDisplayDto TestimonialsPostDtoToTestimonialsPostToDisplayDto(TestimonialsPostDto testimonialsPostDto)
        {
            return new TestimonialsPostToDisplayDto()
            {
                Name = testimonialsPostDto.Name,

                Content = testimonialsPostDto.Content
            };
        }

        public TestimonialsModel TestimonialsPutToDisplayDto(TestimonialsPutDto testimonialsPutDto)
        {
            return new TestimonialsModel()
            {
                Name = testimonialsPutDto.Name,
                Image = testimonialsPutDto.Image,
                Content = testimonialsPutDto.Content


            };
        }



        public CategorieModel CategoryPostDtoToCategoryModel(CategoryPostDto categoryPostDto)
        {
            return new CategorieModel()
            {
                NameCategorie = categoryPostDto.NameCategory,
                DescriptionCategorie = categoryPostDto.DescriptionCategory,
                Image = categoryPostDto.Image,
                SoftDelete = categoryPostDto.SoftDelete,
                LastModified = categoryPostDto.LastModified,

            };
        }

        public NewsModel NewsPostDtoToNewsModel(NewsPostDto newsPostDto)
        {
            return new NewsModel()
            {
                Name = newsPostDto.Name,
                Content = newsPostDto.Content,
                Image = newsPostDto.Image,
                CategorieId = newsPostDto.CategorieId,
                SoftDelete = newsPostDto.SoftDelete,
                LastModified = newsPostDto.LastModified
            };
        }

        public ActivityDto ActivityModelToActivityDto(ActivityModel activityModel)
        {
            return new ActivityDto()
            { Name = activityModel.Name,
            Content=activityModel.Content,
            Image=activityModel.Image
            
              
            };
        }
        public ActivityModel ActivityUpdateDtoToActivityModel(ActivityUpdateDto activityUpdateDto)
        {
            return new ActivityModel()
            {
                Name = activityUpdateDto.Name,
                Content = activityUpdateDto.Content

            };
        }
    }
}    
