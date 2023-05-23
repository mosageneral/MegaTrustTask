using DomainLayer.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.UserEntity
{
    public class User:BaseDomain
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        
    }
}
