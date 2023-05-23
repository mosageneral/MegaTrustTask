using DomainLayer.Entities.VoteFormEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.VoteFormDtos
{
    public class GetFormsFormployeeResultDto
    {
        public VoteForm VoteForm { get; set; }
        public bool IsVoted { get; set; }
    }
}
