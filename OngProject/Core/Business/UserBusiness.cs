
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using SendGrid.Helpers.Mail;
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
        private readonly IEmailBusiness _emailBusiness;



        private readonly IEncryptHelper _encryptHelper;
        public UserBusiness(IUnitOfWork unitOfWork, IEmailBusiness emailBusiness, IEncryptHelper encryptHelper)
        {
            _unitOfWork = unitOfWork;
            _emailBusiness = emailBusiness;

            _encryptHelper = encryptHelper;
        }


        public async Task<UserRegisterToDisplayDto> Register(UserRegisterDto userRegisterDto)
        {

            var user = entityMapper.UserRegisterDtoToUserModel(userRegisterDto);
           


            user.Password = _encryptHelper.EncryptPassSha256(user.Password);

            _unitOfWork.UserModelRepository.Add(user);
            _unitOfWork.SaveChanges();

            await _emailBusiness.SendEmailWithTemplateAsync(userRegisterDto.Email,$"Bienvenido a esta gran comunidad",$"Gracias por registrarte {user.FirstName}","Ong Somos Más");
            return entityMapper.UserRegisterDtoToUserRegisterToDisplayDto(userRegisterDto);
        }
       
        public bool ValidationEmail(string emailAddress)
        {
            var users = _unitOfWork.UserModelRepository.GetAll();
            if (users.Any(x => x.Email == emailAddress))
            {
                return false;
            }

            return true;
        }
    }

}
