using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ActivityBusiness : IActivityBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper _entityMapper;
        private readonly IConfiguration _configuration;
       

        public ActivityBusiness(IUnitOfWork unitOfWork, EntityMapper entityMapper,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
            _configuration = configuration;
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
        public async Task<Response<ActivityDto>> Update(int id, ActivityUpdateDto activityUpdateDto)
        {
            var imagesBussines = new ImagesBusiness(_configuration);
            var response = new Response<ActivityDto>();
            var errorList = new List<string>();
            string image;
            var activity = _unitOfWork.ActivityModelRepository.GetById(id);
           
            if (activity == null)
            {
                errorList.Add("This activity not found");
                response.Data = null;
                response.Errors = errorList.ToArray();
                response.Succeeded = false;
                return response;
            }
            if (activityUpdateDto.Image != null)
            {
                image=await imagesBussines.UploadFileAsync(activityUpdateDto.Image);
                activity.Image = image;
            }
            if (activityUpdateDto.Content != null)
            {
                activity.Content = activityUpdateDto.Content;
            }


            if (activityUpdateDto.Name != null)
            {
                activity.Name = activityUpdateDto.Name;
            }
            _unitOfWork.ActivityModelRepository.Update(activity);
            await _unitOfWork.SaveChangesAsync();
            
            var updatedActivity =_unitOfWork.ActivityModelRepository.GetById(id);
            response.Data = _entityMapper.ActivityModelToActivityDto(updatedActivity);
            response.Succeeded = true;
            return response;
        }
    }
}
