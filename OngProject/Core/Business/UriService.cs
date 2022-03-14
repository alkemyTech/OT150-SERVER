using OngProject.Core.Models;
using OngProject.Repositories.Interfaces;
using System;

namespace OngProject.Core.Business
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetNextPage(PaginationParams paginationParams, string actionUrl)
        {
            string baseUrl = $"{_baseUri}/{actionUrl}?PageNumber={paginationParams.PageNumber + 1}&PageSize={paginationParams.PageSize}";
            return new Uri(baseUrl);
        }

        public Uri GetPreviousPage(PaginationParams paginationParams, string actionUrl)
        {
            string baseUrl = $"{_baseUri}/{actionUrl}?PageNumber={paginationParams.PageNumber - 1}&PageSize={paginationParams.PageSize}";
            return new Uri(baseUrl);
        }
    }
}
