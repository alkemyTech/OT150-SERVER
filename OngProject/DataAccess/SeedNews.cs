using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.DataAccess
{
    public class SeedNews : IEntityTypeConfiguration<NewsModel>
    {
		public void Configure(EntityTypeBuilder<NewsModel> builder)
		{
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new NewsModel
					{
						Id = i,
						Name = "News " + i,
						Content = "Content from news " + i,
						Image = "Image from news " + i,
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				);
			}
		}
	}
}
