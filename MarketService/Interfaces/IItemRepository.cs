using MarketService.Models;
using MarketService.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Interfaces
{
    public interface IItemRepository
    {
        Item GetItemByCode(string code);
        void Save(ItemInfo iteminfo);
    }
}
