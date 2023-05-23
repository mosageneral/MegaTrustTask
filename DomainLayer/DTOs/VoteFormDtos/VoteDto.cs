using DomainLayer.Entities.UserEntity;
using DomainLayer.Entities.VoteFormEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.VoteFormDtos
{
    public class VoteDto
    {
        public Guid ChoiceId { get; set; }
   
        public Guid UserId { get; set; }
        public Guid QuestionId  { get; set; }

    }
}
