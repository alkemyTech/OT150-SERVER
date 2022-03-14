using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IJwtHelper _jwtHelper;
        private readonly IUnitOfWork _unitOfWork;


        public AuthController(IUserBusiness userBusiness, IJwtHelper jwtHelper, IUnitOfWork unitOfWork)
        {
            this._userBusiness = userBusiness;
            _jwtHelper = jwtHelper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm]UserRegisterDto userRegisterDto)
        {

            if (ModelState.IsValid)
            {

                if (_userBusiness.ValidationEmail(userRegisterDto.Email))
                {
                    var userDto = await _userBusiness.Register(userRegisterDto);

                    var users = await _unitOfWork.UserModelRepository.FindAllAsync();
                    var user = users.Where(x => x.Email == userDto.Email).FirstOrDefault();
                    var tokenParameter = new TokenParameter

                    {
                        Email = user.Email,
                        Id = user.Id,
                        Role = userDto.Role
                    };

                    var token = _jwtHelper.GenerateJwtToken(tokenParameter);                

                    return Ok(token);
                }

                else
                {
                    return BadRequest("Error:The email already exists");
                } 

            }
            else
            {
                return BadRequest(ModelState);

            }
            
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] UserLoginDto login)
        {
            if (ModelState.IsValid)
            {

                var user = _userBusiness.Login(login.Email, login.Password);

                if (user == null)
                {
                    return BadRequest("Error: Email or password are incorrect");
                }

                var tokenParameter = new TokenParameter
                {
                    Email = user.Email,
                    Id = user.Id,
                    Role = user.Role
                };

                var token = _jwtHelper.GenerateJwtToken(tokenParameter);

                return Ok(token);
                
            }
            else
            {
                return BadRequest(ModelState);

            }
        }

        [HttpGet("Me")]
        [Authorize]
        public IActionResult Me()
        {
            var claimId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            var id = Int32.Parse(claimId.Value);

            var user = _userBusiness.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
