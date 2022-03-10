using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IActivityBusiness
    {
        Response<ActivityDto> Create(ActivityDto activityDto);
        Task<Response<ActivityDto>> Update(int id, ActivityUpdateDto activityUpdateDto);
    }
}
