using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Models.Contracts
{
    public class StoreInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        //public Bank Bank { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
