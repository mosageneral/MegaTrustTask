using DomainLayer.Entities.UserEntity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Configration
{
    class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasData
            (
            new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse("8ecbb10d-2733-4d28-b91e-1a0533df18cd"),
                RoleId = Guid.Parse("b615b23a-e499-424a-aa71-78e352c639a4"),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            },
             new UserRole
             {
                 Id = Guid.NewGuid(),
                 UserId = Guid.Parse("2a36a09b-d375-47ad-a092-a4362138ebed"),
                 RoleId = Guid.Parse("c8f0a060-c4ae-4435-8296-5cf0e1784f4b"),
                 CreatedAt = DateTime.Now,
                 UpdatedAt = DateTime.Now

             },
             new UserRole
             {
                 Id = Guid.NewGuid(),
                 UserId = Guid.Parse("4fd11a06-5f58-4f47-89eb-e84c2d2cfc01"),
                 RoleId = Guid.Parse("c8f0a060-c4ae-4435-8296-5cf0e1784f4b"),
                 CreatedAt = DateTime.Now,
                 UpdatedAt = DateTime.Now

             },
             new UserRole
             {
                 Id = Guid.NewGuid(),
                 UserId = Guid.Parse("5ccdf9d5-7205-4642-bae7-2076532d0fba"),
                 RoleId = Guid.Parse("c8f0a060-c4ae-4435-8296-5cf0e1784f4b"),
                 CreatedAt = DateTime.Now,
                 UpdatedAt = DateTime.Now

             });

        }
    }
}
