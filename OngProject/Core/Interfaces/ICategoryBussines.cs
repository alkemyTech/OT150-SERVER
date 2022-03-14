using OngProject.Core.Helper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ICategoryBussines
    {
        public PagedList<CategorieDto> GetCategories(PaginationParams paginationParams);
        CategoryGetDto GetCategory(int id);
        Response<CategorieModel> PostCategory(CategoryPostDto categoryPostDto);
        Task<Response<CategorieModel>> DeleteCategory(int id);
        Task<Response<CategorieModel>> UpdateCategory(int id, CategoryUpdateDto categoryUpdate);
    }
}
