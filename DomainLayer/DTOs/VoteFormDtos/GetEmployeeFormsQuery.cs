using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.VoteFormDtos
{
    public class GetEmployeeFormsQuery
    {
        public Guid EmployeeId { get; set; }
        public int page { get; set; }
        public int PageCount { get; set; }
        public bool IsVoted { get; set; } = false;
        public string FormTitle { get; set; }
    }
}
