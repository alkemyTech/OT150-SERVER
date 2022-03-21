using Microsoft.EntityFrameworkCore;
using OngProject.DataAccess;
using OngProject.Entities;
using System;

namespace Test.Helper
{
    public class OngContextInMemory
    {

        public static OngContext MakeDbContext()
        {
            var options = new DbContextOptionsBuilder<OngContext>().UseInMemoryDatabase(databaseName: "Ong").Options;

            var dbcontext = new OngContext(options);

            SeedActivitiesMemory(dbcontext);
            return dbcontext;
        }

        private static void SeedActivitiesMemory(OngContext context)
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

            context.Add(activity);
            context.SaveChanges();
        }
    }
}
