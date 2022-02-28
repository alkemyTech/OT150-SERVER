using OngProject.Core.Models.DTOs;

namespace OngProject.Core.Interfaces
{
    public interface ISlide
    {
        SlideDto GetById(int id);
    }
}
