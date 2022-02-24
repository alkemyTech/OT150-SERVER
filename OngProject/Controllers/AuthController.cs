using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using System;
using System.Net;
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


    }
}
