using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Models
{
    public class Store:BaseClass
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        //public Bank Bank { get; set; }
        public virtual double Lat { get; set; }
        public virtual double Lng { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }

    }
}
