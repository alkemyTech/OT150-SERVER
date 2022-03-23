﻿using Microsoft.AspNetCore.Http;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CommentBusiness : ICommentBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _entityMapper;

        public CommentBusiness(IUnitOfWork unitOfWork, EntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }

        public List<CommentDto> GetComments()
        {
            var comments = _unitOfWork.CommentModelRepository.GetAll();
            var commentsDto = new List<CommentDto>();

            foreach (var comment in comments)
            {
                commentsDto.Add(_entityMapper.CommentModelToCommentDto(comment));
            }

            return commentsDto;
        }

        public List<CommentDto> showListCommentDto(int id)
        {
            List<CommentDto> listaFiltrada = new List<CommentDto>();
            var lista = _unitOfWork.CommentModelRepository.GetAll();
            if (lista != null)
            {
                foreach (var item in lista)
                {
                    if (item.NewsId == id)
                    {
                        listaFiltrada.Add(_entityMapper.CommentModelToCommentDto(item));
                    }
                }
            }
            else listaFiltrada = null;

            return listaFiltrada;
        }

        public async Task<Response<CommentModel>> DeleteComment(int id, string rol, string idUser)
        {
            CommentModel comment = _unitOfWork.CommentModelRepository.GetById(id);
            var response = new Response<CommentModel>();
            List<string> intermediate_list = new List<string>();
            if (comment == null)
            {
                intermediate_list.Add("404");
                response.Data = comment;
                response.Message = "This Comment not Found";
                response.Succeeded = false;
                response.Errors = intermediate_list.ToArray();
                return response;

            }
            if (rol == "Admin" || idUser == comment.UserId.ToString())
            {
                CommentModel entity = await _unitOfWork.CommentModelRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                intermediate_list.Add("200");
                response.Errors = intermediate_list.ToArray();
                response.Data = entity;
                response.Succeeded = true;
                response.Message = "The Comment was Deleted successfully";
                return response;
            }
            intermediate_list.Add("403");
            response.Data = comment;
            response.Succeeded = false;
            response.Errors = intermediate_list.ToArray();
            response.Message = "You don't have permission for modificated this comment";
            return response;
        }
        public async Task<Response<CommentPostDto>> Post(CommentPostDto commentPost,int id)
        {
            var response = new Response<CommentPostDto>();
            var error = new List<string>();
            var user = _unitOfWork.UserModelRepository.GetById(id);
          
            var news = _unitOfWork.NewsModelRepository.GetById(commentPost.NewsId);
            
            if (news != null)
            {
                var mappedComment = _entityMapper.CommentPostDtoToCommentModel(commentPost);
                mappedComment.UserId=user.Id;

                _unitOfWork.CommentModelRepository.Add(mappedComment);
                await _unitOfWork.SaveChangesAsync();
                response.Data = commentPost;
                response.Message = "The comment was added";
                response.Succeeded = true;
                return response;
               
            }
            else
            {
                
                if(news == null)
                {
                    error.Add("The news does not exist");
                }
                response.Data = null;
                response.Message = "The comment was not added";
                response.Errors = error.ToArray();
                response.Succeeded = false;
                return response;
            }
         
        }

        public async Task<Response<CommentDto>> Update(int commentId, CommentPutDto commentDto)
        {
            var response = new Response<CommentDto>();
            var errorList = new List<string>();
            var commentUpdate = _unitOfWork.CommentModelRepository.GetById(commentId);

            if (commentUpdate == null)
            {
                errorList.Add("Comment doesn't exist");
                response.Data = null;
                response.Errors = errorList.ToArray();
                response.Succeeded = false;
                return response;
            }



            commentUpdate.Body = commentDto.Body;
            commentUpdate.UserId = commentDto.User_Id;

            _unitOfWork.CommentModelRepository.Update(commentUpdate);
            await _unitOfWork.SaveChangesAsync();

            response.Data = _entityMapper.CommentModelToCommentDto(commentUpdate);
            response.Succeeded = true;
            return response;
        }
    }
}
