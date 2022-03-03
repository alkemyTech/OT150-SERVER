using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;

namespace OngProject.DataAccess
{
	public class SeedContacts : IEntityTypeConfiguration<ContactsModel>
	{
		public void Configure(EntityTypeBuilder<ContactsModel> builder)
		{
			for (int i = 1; i < 11; i++)
			{
				builder.HasData(
					new ContactsModel
					{
						Id = i,
						Name = "Contact " + i,
						Email = "Email of contact " + i,
						Phone = new Random().Next(1100000000, 1200000000).ToString(),
						Message = "Message of contact " + i,
						LastModified = DateTime.Now,
						SoftDelete = true
					}
				);
			}
		}
	}
}
