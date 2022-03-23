using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OngProject.Core.Helper;
using OngProject.Entities;
using System;
using System.Security.Cryptography;
using System.Text;

namespace OngProject.DataAccess
{
    public class SeedUsers : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            EncryptHelper encryptHelper = new EncryptHelper();

            for (int i = 1; i < 11; i++)
            {
                builder.HasData(
                    new UserModel
                    {
                        Id = i,
                        FirstName = "Name User " + i,
                        LastName = "Last Name User" + i,
                        Email = "User" + i + "@ong.com",
                        Password = ComputeSha256Hash("Password" + i),
                       
                        SoftDelete = false,
                        RoleId = 1,
                        LastModified = DateTime.Now
                    }
                );
            }

            for (int i = 11; i < 21; i++)
            {
                builder.HasData(
                    new UserModel
                    {
                        Id = i,
                        FirstName = "Name User " + i,
                        LastName = "Last Name User" + i,
                        Email = "User" + i + "@ong.com",
                        Password = ComputeSha256Hash("Password" + i),
                     
                        SoftDelete = false,
                        RoleId = 2,
                        LastModified = DateTime.Now
                    }
                );
            }
        }

        static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
