using BusinessLayer.Helpers;
using DomainLayer.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLayer.Configration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasData
            (
            new User
            {
                Id =Guid.Parse("8ecbb10d-2733-4d28-b91e-1a0533df18cd"),
                UserName = "Admin",
                Password = EncryptANDDecrypt.EncryptText("123456"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            }, new User
            {
                Id = Guid.Parse("2a36a09b-d375-47ad-a092-a4362138ebed"),
                UserName = "Employee1",
                Password = EncryptANDDecrypt.EncryptText("123456"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            }, new User
            {
                Id = Guid.Parse("4fd11a06-5f58-4f47-89eb-e84c2d2cfc01"),
                UserName = "Employee2",
                Password = EncryptANDDecrypt.EncryptText("123456"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            }, new User
            {
                Id = Guid.Parse("5ccdf9d5-7205-4642-bae7-2076532d0fba"),
                UserName = "Employee4",
                Password = EncryptANDDecrypt.EncryptText("123456"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            });
           
        }
    }
}
