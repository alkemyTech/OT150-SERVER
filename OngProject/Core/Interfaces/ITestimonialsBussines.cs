using OngProject.Core.Helper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsBussines
    {
        PagedList<TestimonialsDto> GetAllTestimonials(PaginationParams paginationParams);
        Task<Response<TestimonialsPostToDisplayDto>> Post(TestimonialsPostDto testimonialPostDto);
        Task<Response<TestimonialsModel>> Delete(int id);
        Task<Response<TestimonialsModel>> PutTestimonials(int id,TestimonialsPutDto testimonialsDto);
    }
}
