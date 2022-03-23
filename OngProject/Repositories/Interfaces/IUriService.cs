using OngProject.Core.Models;
using System;

namespace OngProject.Repositories.Interfaces
{
    public interface IUriService
    {
        Uri GetNextPage(PaginationParams paginationParams, string actionUrl);
        Uri GetPreviousPage(PaginationParams paginationParams, string actionUrl);
    }
}
