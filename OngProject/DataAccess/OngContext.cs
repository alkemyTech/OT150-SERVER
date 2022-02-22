using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OngProject.Entities;

namespace OngProject.DataAccess
{
    public class OngContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public OngContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:Challenge"]);
        }

        public DbSet<ContactsModel> ContactsModel { get; set; }
        public DbSet<TestimonialsModel> TestimonialsModel { get; set; }
        public DbSet<SlideModel> SlidesModels { get; set; }

    }
}
