using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class ActivityBusiness : IActivityBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _entityMapper;

        public ActivityBusiness(IUnitOfWork unitOfWork, EntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }

        public Response<ActivityDto> Create(ActivityDto activityDto)
        {

            var response = new Response<ActivityDto>();

            var activity = _entityMapper.ActivityDtoToActivityModel(activityDto);

            _unitOfWork.ActivityModelRepository.Add(activity);
            _unitOfWork.SaveChanges();

            response.Data = activityDto;
            response.Succeeded = true;
            response.Message = "The activity was successfully added";
            return response;

        }
    }
}
