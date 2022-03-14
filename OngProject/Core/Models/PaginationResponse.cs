
using Microsoft.CodeAnalysis;

namespace OngProject.Core.Models
{
    public class PaginationResponse<T>
    {
        public PaginationResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public Metadata Meta { get; set; }
    }
}
