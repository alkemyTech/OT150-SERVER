using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;


namespace OngProject.DataAccess
{
    public class SeedOrganization : IEntityTypeConfiguration<OrganizationModel>
	{
		public void Configure(EntityTypeBuilder<OrganizationModel> builder)
		{
			builder.HasData(
				new OrganizationModel
				{
					Id = 1,
					Name = "Organization",
					Image = "Image from organization",
					Address = "Address of Organization",
					Phone = 1112345678,
					Email = "Organization@ong.com",
					WelcomeText = "Welcome to Organization",
					AboutUsText = "We are Organization...",
					FacebooK = "OrganizationFaceBook",
					Linkedin = "OrganizationLinkedin",
					Instagram = "OrganizationInstagram",
					LastModified = DateTime.Now,
					SoftDelete = true
				}
			);
		}
	}
}