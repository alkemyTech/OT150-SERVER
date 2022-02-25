using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IEmailBusiness _emailBusiness;


        public AuthController(IUserBusiness userBusiness, IEmailBusiness emailBusiness)
        {
            this._userBusiness = userBusiness;
            _emailBusiness = emailBusiness;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto userRegisterDto)
        {
            

                if (ModelState.IsValid)
                {

                    if (_userBusiness.ValidationEmail(userRegisterDto.Email))
                    {
                        var userToDisplay= _userBusiness.Register(userRegisterDto); 
                        await _emailBusiness.SendEmail(userRegisterDto.Email);
                        return Ok(userToDisplay);
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
        public IActionResult Login([FromBody] UserLoginDto login)
        {
            if (ModelState.IsValid)
            {

                var user = _userBusiness.Login(login.Email, login.Password);

                if (user == null)
                {
                    return BadRequest("Error: Email or password are incorrect");
                }
                return Ok(user);
                
            }
            else
            {
                return BadRequest(ModelState);

            }
        }
    }
}
