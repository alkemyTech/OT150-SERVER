﻿using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CategoryBussines : ICategoryBussines
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper entityMapper = new EntityMapper();

        public CategoryBussines(IUnitOfWork unitOfWork)
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

        public CategoryGetDto GetCategory(int id)
        {
            var category = _unitOfWork.CategorieModelRepository.GetById(id);
            return entityMapper.CategorieModelToCategorieGetDto(category);
        }

        public Response<CategorieModel> PostCategory(CategoryPostDto categoryPostDto)
        {
            Response<CategorieModel> response = new Response<CategorieModel>();
            List<string> intermediate_list = new List<string>();
            var category = entityMapper.CategoryPostDtoToCategoryModel(categoryPostDto);
            _unitOfWork.CategorieModelRepository.Add(category);
            _unitOfWork.SaveChanges();
            intermediate_list.Add("200");
            response.Errors = intermediate_list.ToArray();
            response.Data = category;
            response.Succeeded = true;
            response.Message = "The Category was Posted successfully";
            return response;
        }
    }
}