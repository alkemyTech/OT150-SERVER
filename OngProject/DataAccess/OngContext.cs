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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SeedTestimonials());
            modelBuilder.ApplyConfiguration(new SeedActivities());
            modelBuilder.ApplyConfiguration(new SeedCategories());
            modelBuilder.ApplyConfiguration(new SeedMembers());
            modelBuilder.ApplyConfiguration(new SeedNews());
            modelBuilder.ApplyConfiguration(new SeedOrganization());
            modelBuilder.ApplyConfiguration(new SeedContacts());
            modelBuilder.ApplyConfiguration(new SeedSlides());
            modelBuilder.ApplyConfiguration(new SeedRoles());
            modelBuilder.ApplyConfiguration(new SeedUsers());
            modelBuilder.ApplyConfiguration(new SeedComments());//
        }

        public DbSet<ContactsModel> ContactsModel { get; set; }
        public DbSet<TestimonialsModel> TestimonialsModel { get; set; }
        public DbSet<SlideModel> SlidesModels { get; set; }
        public DbSet<RoleModel> RoleModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<CategoryModel> CategoriesModels { get; set; }
        public DbSet<NewsModel> NewsModels { get; set; }
        public DbSet<OrganizationModel> OrganizationModels { get; set; }
        public DbSet<MemberModel> MemberModels { get; set; }
        public DbSet<ActivityModel> ActivityModels { get; set; }
        public DbSet<CommentModel> CommentModels { get; set; }

    }
}
