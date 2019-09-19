using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Models
{
    public class User:BaseClass
    {
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }
    }
}
