using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
    public class SeedSlides : IEntityTypeConfiguration<SlideModel>
	{
		public void Configure(EntityTypeBuilder<SlideModel> builder)
		{
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new SlideModel
					{
						Id = i,
						ImageUrl = "ImageUrl to Slide " + i,
						Text = "Text to Slide " + i,
						Order = i,
						OrganizationId = 1,
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				);
			}
		}
	}
}
