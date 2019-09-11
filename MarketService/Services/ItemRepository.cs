using MarketService.Interfaces;
using MarketService.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Services
{
    public class ItemRepository : IItemRepository
    {
        ISession session;
        public ItemRepository(ISession _session)
        {
            session = _session;
        }
        public Item GetItemByCode(string code)
        {
          var item=  session.Query<Item>().Where(d => d.Code == code).FirstOrDefault<Item>();
            return item;
        }
    }
}
