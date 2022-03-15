using Microsoft.CodeAnalysis;

namespace OngProject.Core.Models
{
    public class PaginationResponse<T>
    {
        public PaginationResponse(T data)
        {
            Data = data;
        }

        public Metadata Meta { get; set; }
        public T Data { get; set; }

    }
}
