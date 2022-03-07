using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICommentBusiness
    {
        List<CommentDto> showListCommentDto(int id);
        List<CommentDto> GetComments();
        Task<Response<CommentModel>> DeleteComment(int id, string rol, string idUser);
        Task<Response<CommentPostDto>> Post(CommentPostDto commentPost);
    }
}
