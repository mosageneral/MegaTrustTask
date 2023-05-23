using DomainLayer.Entities.VoteFormEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.VoteFormDtos
{
    public class VoteFormQuestionStatusDto
    {
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public decimal Percentage { get; set; }
        public int VoteCount { get; set; }
    }
}
