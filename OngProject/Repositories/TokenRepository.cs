using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OngProject.Entities.Jwt;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OngProject.Repositories
{
    public class TokenRepository : ITokenRepository
    {
      

        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUserEntity> _userManager;
        public TokenRepository(IConfiguration configuration)
        {
            
            _configuration = configuration;
        }
      

        public async Task<RequestToken> GetToken(IdentityUserEntity identityUser)
        {
            var userRoles = await _userManager.GetRolesAsync(identityUser);

            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,identityUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                
                new Claim(ClaimTypes.Email,identityUser.Email)
             

            };
            AuthClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var AuthSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:JwtKey")));
            var token = new JwtSecurityToken(
               issuer: _configuration.GetValue<string>("Jwt:issuer"),
      audience: _configuration.GetValue<string>("Jwt:audience"),
      expires: _configuration.GetValue<DateTime>("Jwt:expires"),
      claims: AuthClaims,
      signingCredentials: new SigningCredentials(AuthSignInKey, SecurityAlgorithms.HmacSha256));
            return new RequestToken
            {

                TokenCode = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }

    } 
}

