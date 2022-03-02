using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
    public class SeedActivities : IEntityTypeConfiguration<ActivityModel>
    {
		public void Configure(EntityTypeBuilder<ActivityModel> builder)
		{
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new ActivityModel
					{
						Id = i,
						Name = "activity " + i,
						Content = "Contenido de actividad " + i,
						Image = "Image from activity " + i,
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				);
			}
		}
	}
}
