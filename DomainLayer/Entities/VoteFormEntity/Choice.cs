using DomainLayer.Entities.BaseEntity;
using DomainLayer.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.VoteFormEntity
{
    public class Choice:BaseDomain
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public int Votes { get; set; }
      

    }
}
