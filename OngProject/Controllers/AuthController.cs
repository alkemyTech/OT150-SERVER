using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IEmailBusiness _emailBusiness;
        private readonly IJwtHelper _jwtHelper;


        public AuthController(IUserBusiness userBusiness, IEmailBusiness emailBusiness, IJwtHelper jwtHelper)
        {
            this._userBusiness = userBusiness;
            _emailBusiness = emailBusiness;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto userRegisterDto)
        {
            

                if (ModelState.IsValid)
                {

                    if (_userBusiness.ValidationEmail(userRegisterDto.Email))
                    { 
                        
                        return Ok(
                        await _userBusiness.Register(userRegisterDto));
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
