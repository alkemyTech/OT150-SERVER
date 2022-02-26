using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {



        private readonly IUserBusiness _userBusiness;


        public UserController(IUserBusiness userBusiness)
        {

            _userBusiness = userBusiness;

        }

        [HttpGet("Lista")]
        public async Task<IActionResult> Lista()
        {


            return Ok(_userBusiness.GetUsuarios());





        }
    }

}
