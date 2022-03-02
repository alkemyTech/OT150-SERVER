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

        public List<CommentDto> showListCommentDto(int id)
        {
            var lista = _unitOfWork.CommentModelRepository.GetAll();
            List<CommentDto> listaFiltrada = new List<CommentDto>();
            foreach (var item in lista)
            {
                if (item.News_Id == id)
                {
                    listaFiltrada.Add(_entityMapper.CommentModelToCommentDto(item));
                }
            }
            return listaFiltrada;
        }
    }
}
