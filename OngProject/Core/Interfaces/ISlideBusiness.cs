using OngProject.Core.Models.DTOs;
using System.Collections.Generic;

namespace OngProject.Core.Interfaces
{
    public interface ISlideBusiness
    {
        SlideDto showDetailSlide(int id);
        IEnumerable<SlideDto> GetSlides();
    }
}
