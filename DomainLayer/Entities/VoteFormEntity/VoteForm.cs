using DomainLayer.Entities.BaseEntity;
using DomainLayer.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.VoteFormEntity
{
    public class VoteForm:BaseDomain
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
       
        public ICollection<Question> Questions { get; set; }
        
    }
}
