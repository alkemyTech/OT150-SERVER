using OngProject.Core.Models.DTOs;
using System.Collections.Generic;

namespace OngProject.Core.Interfaces
{
    public interface ICommentBusiness
    {
        List<CommentDto> showListCommentDto(int id);
    }
}
