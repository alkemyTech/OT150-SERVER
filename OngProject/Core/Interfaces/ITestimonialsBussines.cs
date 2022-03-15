using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsBussines
    {
        Task<Response<TestimonialsPostToDisplayDto>> Post(TestimonialsPostDto testimonialPostDto);
        Task<Response<TestimonialsModel>> Delete(int id, string rol, string UserId);
        Response<TestimonialsModel> PutTestimonials(TestimonialsPutDto testimonialsDto);
    }
}
