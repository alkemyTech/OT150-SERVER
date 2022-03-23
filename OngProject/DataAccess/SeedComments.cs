using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
    public class SeedComments : IEntityTypeConfiguration<CommentModel>
	{
		public void Configure(EntityTypeBuilder<CommentModel> builder)
		{
			builder.HasData(
					new CommentModel
					{
						Id = 1,
						Body = "Comment Nro " + 1,
						NewsId = 1,
						UserId = 1,
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				); //Comment of id 1 for User_Id=1 of type Admin

			builder.HasData(
					new CommentModel
					{
						Id = 2,
						Body = "Comment Nro " + 2,
						NewsId = 2,
						UserId = 11,
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				); //Comment of Id 2 for User_Id=11 of type User

			for (int i = 3; i < 31; i++)
			{
				builder.HasData(
					new CommentModel
					{
						Id = i,
						Body = "Comment Nro " + i,
						NewsId = new Random().Next(1,11),
						UserId = new Random().Next(1,21),
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				); //Randoms Comments

			}
		}
	}
}