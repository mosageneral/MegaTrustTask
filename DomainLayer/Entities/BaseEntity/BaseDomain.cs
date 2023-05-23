using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.BaseEntity
{
    public class BaseDomain
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } =DateTime.Now;
    }
}
