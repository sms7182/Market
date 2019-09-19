using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Models.Contracts
{
    public class ItemInfo
    {
        public ItemInfo()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public decimal UnitPrice { get; set; }
        public string Unit { get; set; }
    }
}
