using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;

namespace OngProject.Core.Interfaces
{
    public interface IActivityBusiness
    {
        Response<ActivityDto> Create(ActivityDto activityDto);
    }
}
