using DomainLayer.Entities.VoteFormEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.VoteFormDtos
{
    public class VoteFormStatusDto
    {
       
        public VoteForm VoteForm { get; set; }
        public int FormVotesCount { get; set; }

        public List<VoteFormQuestionStatusDto> VoteFormQuestionStatus { get; set; }
    }
}
