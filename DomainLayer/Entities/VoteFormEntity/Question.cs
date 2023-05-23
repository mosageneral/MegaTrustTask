using DomainLayer.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.VoteFormEntity
{
    public class Question:BaseDomain
    {
        public Guid VoteFormId { get; set; }
        public VoteForm VoteForm { get; set; }
        public string Text { get; set; }
        public ICollection<Choice> Choices { get; set; }
    }
}
