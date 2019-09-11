using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Models
{
    public abstract class BaseClass
    {
        public BaseClass()
        {
            Id = Guid.NewGuid();
        }
        public virtual Guid Id { get; set; }
    }
}
