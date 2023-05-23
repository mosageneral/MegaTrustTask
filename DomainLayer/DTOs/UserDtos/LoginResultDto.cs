using DomainLayer.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.UserDtos
{
    public class LoginResultDto
    {
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
