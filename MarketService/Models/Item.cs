using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Models
{
    public class Item:BaseClass
    {

        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual decimal UnitPrice { get; set; }
    }
    public enum Unit
    {
        Kilo=1,
        Number=2
    }
    
}
