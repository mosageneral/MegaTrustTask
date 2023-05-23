using DomainLayer.Entities.BaseEntity;
using DomainLayer.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.VoteFormEntity
{
    public class Vote:BaseDomain
    {
        public Guid ChoiceId { get; set; }
        public Choice Choice { get; set; }
        public Guid UserId { get; set; }
        public User  User { get; set; }
        public Guid QuestionId  { get; set; }
    }
}
