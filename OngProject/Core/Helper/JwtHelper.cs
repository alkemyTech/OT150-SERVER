using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OngProject.Core.Helper
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IConfiguration _configuration;

       

        public JwtHelper(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public string GenerateJwtToken(ITokenParameter tokenParameter)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, tokenParameter.Id.ToString()),
                    new Claim(ClaimTypes.Email, tokenParameter.Email),

                    new Claim(ClaimTypes.Role, tokenParameter.Role)

                };
            var authSigningKey = new SymmetricSecurityKey(key);
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return tokenHandler.WriteToken(token);
        }
    }
}
