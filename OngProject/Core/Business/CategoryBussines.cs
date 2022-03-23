using Microsoft.Extensions.Configuration;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
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
        private readonly IConfiguration _configuration;

        public CategoryBussines(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public PagedList<CategorieDto> GetCategories(PaginationParams paginationParams)
        {
            var categorias = _unitOfWork.CategorieModelRepository.GetAll();
            var categoriasDto = new List<CategorieDto>();
            foreach (var c in categorias)
            {
                categoriasDto.Add(entityMapper.CategorieListDtoCategorieModel(c));
            }

            var pagedCategories = PagedList<CategorieDto>.Create(categoriasDto, paginationParams.PageNumber, paginationParams.PageSize);


            return pagedCategories;
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

        public async Task<Response<CategorieModel>> DeleteCategory(int id)
        {
            var response = new Response<CategorieModel>();
            List<string> intermediate_list = new List<string>();
            CategorieModel existCategory = await _unitOfWork.CategorieModelRepository.GetByIdAsync(id);
            if (existCategory == null)
            {
                intermediate_list.Add("404");
                response.Errors = intermediate_list.ToArray();
                response.Data = existCategory;
                response.Succeeded = false;
                response.Message = "This Category not Found";
                return response;
            }
            CategorieModel entity = await _unitOfWork.CategorieModelRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            intermediate_list.Add("200");
            response.Errors = intermediate_list.ToArray();
            response.Data = entity;
            response.Succeeded = true;
            response.Message = "The Comment was Deleted successfully";
            return response;
        }

        public async Task<Response<CategorieModel>> UpdateCategory(int id, CategoryUpdateDto categoryUpdate)
        {
            var imagesBussines = new ImagesBusiness(_configuration);
            var response = new Response<CategorieModel>();
            var errorList = new List<string>();
            string image;
            var category = await _unitOfWork.CategorieModelRepository.GetByIdAsync(id);

            if (category == null)
            {
                errorList.Add("404");
                response.Data = null;
                response.Errors = errorList.ToArray();
                response.Succeeded = false;
                return response;
            }
            if (categoryUpdate.Image != null)
            {
                image = await imagesBussines.UploadFileAsync(categoryUpdate.Image);
                category.Image = image;
            }
            if (categoryUpdate.NameCategorie != null)
            {
                category.NameCategorie = categoryUpdate.NameCategorie;
            }

            if (categoryUpdate.DescriptionCategorie != null)
            {
                category.DescriptionCategorie = categoryUpdate.DescriptionCategorie;
            }
            _unitOfWork.CategorieModelRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();

            var updatedCategory = await _unitOfWork.CategorieModelRepository.GetByIdAsync(id);
            response.Data = updatedCategory;
            response.Succeeded = true;
            return response;
        }
    }
}
