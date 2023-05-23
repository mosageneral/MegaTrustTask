using DomainLayer.Entities.VoteFormEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.VoteFormDtos
{
    public class AddQuestionDto
    {
        public Guid VoteFormId { get; set; }
      
        public string Text { get; set; }
       
    }
}
