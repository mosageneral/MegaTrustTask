using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.BaseEntity
{
    public class BaseResult<T>
    {
        public string StatusCode { get; set; }
        public T? Result { get; set; }
        public string Erorr { get; set; }

    }
}
