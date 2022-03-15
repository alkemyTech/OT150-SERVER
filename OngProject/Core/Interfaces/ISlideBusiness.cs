using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface ISlideBusiness
    {
        SlideDto showDetailSlide(int id);
        IEnumerable<SlideDto> GetSlides();
        Task<Response<SlideDto>> Update(int id, SlidePutDto slide);
        Task<Response<SlideDtoToDisplay>> Delete(int id);
    }
}
