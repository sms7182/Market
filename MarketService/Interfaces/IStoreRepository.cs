using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MarketService.Models;
using MarketService.Models.Contracts;

namespace MarketService.Interfaces
{
   public interface IStoreRepository
    {       
        StoreInfo Find(Guid id);
        List<StoreInfo> GetStores();
    }
}
