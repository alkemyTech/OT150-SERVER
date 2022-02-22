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
        public DbSet<RoleModel> RoleModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<CategorieModel> CategoriesModels { get; set; }
        public DbSet<NewsModel> NewsModels { get; set; }
        public DbSet<OrganizationModels> OrganizationModels { get; set; }
        public DbSet<MemberModel> MemberModels { get; set; }
        public DbSet<ActivityModel> ActivityModels { get; set; }
        public DbSet<CommentModel> CommentModels { get; set; }

    }
}
