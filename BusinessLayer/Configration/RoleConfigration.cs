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
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasData
            (
            new Role
            {
                Id = Guid.Parse("b615b23a-e499-424a-aa71-78e352c639a4"),
                RoleName="Admin",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            },
             new Role
             {
                 Id =Guid.Parse("c8f0a060-c4ae-4435-8296-5cf0e1784f4b"),
                 RoleName = "Employee",
                 CreatedAt = DateTime.Now,
                 UpdatedAt = DateTime.Now

             }) ;

        }
    }
}
