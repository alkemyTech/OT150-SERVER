
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EntityMapper entityMapper = new EntityMapper();



        private readonly IEncryptHelper _encryptHelper;
        public UserBusiness(IUnitOfWork unitOfWork,IEmailBusiness emailBusiness,IEncryptHelper encryptHelper)
        {
            _unitOfWork = unitOfWork;
        
          
            _encryptHelper = encryptHelper;
        }
      

        public  bool Register(UserRegisterDto userRegisterDto)
        {
      
            var user = entityMapper.UserRegisterDtoToUserModel(userRegisterDto);
            var users = _unitOfWork.UserModelRepository.GetAll();
        
            //Verificamos que Email no exista 
            if ((users.Any(x => x.Email == user.Email)))
            {
                return false;
            }
            user.Password = _encryptHelper.EncryptPassSha256(user.Password);
         
            _unitOfWork.UserModelRepository.Add(user);
            _unitOfWork.SaveChanges();
            

            return true;
        }
        
      
    }
}
