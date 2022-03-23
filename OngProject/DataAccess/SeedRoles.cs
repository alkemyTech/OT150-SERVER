using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.DataAccess
{
	public class SeedRoles : IEntityTypeConfiguration<RoleModel>
	{
		

        public void Configure(EntityTypeBuilder<RoleModel> builder)
        {
			builder.HasData(
						 new RoleModel
						 {
							 Id = 1,
							 NameRole = "Admin",
							 DescriptionRole = "Administrador",

							 LastModified = DateTime.Now,
							 SoftDelete = true
						 }
					 );

			builder.HasData(
				new RoleModel
				{
					Id = 2,
					NameRole = "User",
					DescriptionRole = "Usuario",

					LastModified = DateTime.Now,
					SoftDelete = true
				}
			);

		}
	}
}

