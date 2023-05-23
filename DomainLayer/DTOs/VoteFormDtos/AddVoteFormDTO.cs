using DomainLayer.Entities.UserEntity;
using DomainLayer.Entities.VoteFormEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.VoteFormDtos
{
    public class AddVoteFormDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Guid> EmployesIds { get; set; }



    }
}
