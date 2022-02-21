using Microsoft.AspNetCore.Identity;
using OngProject.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities.Jwt
{
    public class IdentityUserEntity : IdentityUser
    {
        public bool IsActive { get; set; }
        
        public int RoleId { get; set; }
       
    }
}
