using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OngProject.Entities.Jwt;

namespace OngProject.Data
{
    public class UserContext : IdentityDbContext<IdentityUserEntity> 
    {
        

       

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:Challenge"]);
        }
    }
}
