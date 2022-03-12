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
        List<CategorieDto> GetCategories();
        CategoryGetDto GetCategory(int id);
        Response<CategorieModel> PostCategory(CategoryPostDto categoryPostDto);
        Task<Response<CategorieModel>> DeleteCategory(int id);
    }
}
