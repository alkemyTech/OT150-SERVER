using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
	public class SeedCategories : IEntityTypeConfiguration<CategorieModel>
	{
		public void Configure(EntityTypeBuilder<CategorieModel> builder)
		{
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new CategorieModel
					{
						Id = i,
						NameCategorie = "category" + i,
						DescriptionCategorie = "Description from category " + i,
						Image = "Image from category " + i,
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				);
			}
		}
	}
}