using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
	public class SeedTestimonials : IEntityTypeConfiguration<TestimonialsModel>
	{
        public void Configure(EntityTypeBuilder<TestimonialsModel> builder)
        {
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new TestimonialsModel
					{
						Id = i,
						Name = "Testimony " + i,
						Content = "Content from testimony " + i,
						Image = "Image from testimony " + i,
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				);
			}
		}
    }
}
