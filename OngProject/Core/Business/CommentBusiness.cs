using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;

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
    }
}
