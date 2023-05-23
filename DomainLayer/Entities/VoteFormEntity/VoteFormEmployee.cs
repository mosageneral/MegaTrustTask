using DomainLayer.Entities.BaseEntity;
using DomainLayer.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.VoteFormEntity
{
    public class VoteFormEmployee:BaseDomain
    {
        public Guid VoteFormId { get; set; }
        public VoteForm VoteForm { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
