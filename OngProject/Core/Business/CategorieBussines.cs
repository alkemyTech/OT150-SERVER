using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CategorieBussines : ICategorieBussines
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper entityMapper = new EntityMapper();

        public CategorieBussines(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CategorieDto> GetCategories()
        {
            var categorias = _unitOfWork.CategorieModelRepository.GetAll();
            var categoriasDto = new List<CategorieDto>();
            foreach (var c in categorias)
            {
                categoriasDto.Add(entityMapper.CategorieListDtoCategorieModel(c));
            }

            return categoriasDto;



        }
    }
}
