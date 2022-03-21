using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using System;

namespace Test.Helper
{
    public class OngContextInMemory
    {

        private static OngContext _context;
        public static OngContext MakeDbContext()
        {
            var options = new DbContextOptionsBuilder<OngContext>().UseInMemoryDatabase(databaseName: "Ong").Options;
            _context = new OngContext(options);
            _context.Database.EnsureDeleted();
            
            
               
                SeedActivitiesMemory();
                SeedTestimonialsMemory();
                _context.SaveChanges();
            
         

            return _context;
        }

        private static void SeedActivitiesMemory()
        {
            var activity = new ActivityModel
            {
                Id = 1,
                Name = "Activity",
                Content = "Content from activity",
                Image = "ActivityImage.jpg",
                SoftDelete = true,
                LastModified = DateTime.Now
            };

            _context.Add(activity);
           
        }
        private static void SeedTestimonialsMemory()
        {
            var testimonial = new TestimonialsModel
            {
                Id = 1,
                Name = "Testimonial",
                Content = "Content from testimonial",
                Image = "TestimonialImage.jpg",
                SoftDelete = true,
                LastModified = DateTime.Now
            };

            _context.Add(testimonial);
            

        }
    }
}