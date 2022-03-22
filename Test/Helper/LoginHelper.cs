using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helper
{
    internal class LoginHelper
    {
        public static ControllerContext LoginHelperJwt(IJwtHelper jwtHelper)
        {
            var tokenParameter = new TokenParameter

            {
                Email = "Email",
                Id = 1,
                Role = "Admin"
            };

            var token = jwtHelper.GenerateJwtToken(tokenParameter);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Authorization", $"Bearer {token}");

            return new ControllerContext()
            {
                HttpContext = httpContext,
            };
        }
    }
}
