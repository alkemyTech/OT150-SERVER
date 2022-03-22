using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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




        /// Register: User
        /// <summary>
        /// Register User
        /// </summary>
        /// <remarks>
        /// Create user. If email already exists, returns 400. 
        /// </remarks>

        /// <param name="userRegisterDto"></param>
        /// <response code="200">OK. The user was created.</response>        
        /// <response code="400">Bad Request.The email already exists.</response>     
        ///<returns></returns>

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult),StatusCodes.Status400BadRequest)]
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
        /// Log: User
        /// <summary>
        /// Log user
        /// </summary>
        /// <remarks>
        /// Log user. Validate email and password, if there is error, returns 400.
        /// </remarks>

        /// <param name="login"></param>
        /// <response code="200">OK. The user was logged.</response>        
        /// <response code="400">Bad Request. Email or password are incorrect.</response>     
        ///<returns></returns>

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult),StatusCodes.Status400BadRequest)]
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
        /// Check: User
        /// <summary>
        /// Check user
        /// </summary>
        /// <remarks>
        /// Check user. 
        /// </remarks>


        /// <response code="200">OK. The user exists.</response>        
        /// <response code="404">NotFound. The user does not exist.</response> 
        /// <response code="401">Unauthorized. Invalid Token or it wasn't provided.</response>  
        ///<returns></returns>

        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(EmptyResult),StatusCodes.Status404NotFound)]

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
