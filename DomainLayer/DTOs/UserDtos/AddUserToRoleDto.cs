using DomainLayer.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.UserDtos
{
    public class AddUserToRoleDto
    {
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
     
    }
}
