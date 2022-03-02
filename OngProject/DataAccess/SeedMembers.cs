using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
    public class SeedMembers : IEntityTypeConfiguration<MemberModel>
	{
		public void Configure(EntityTypeBuilder<MemberModel> builder)
		{
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new MemberModel
					{
						Id = i,
						Name = "Member " + i,
						Description = "Description from member " + i,
						Image = "Image from member " + i,
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				);
			}
		}
	}
}
