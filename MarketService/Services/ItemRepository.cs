using MarketService.Interfaces;
using MarketService.Models;
using MarketService.Models.Contracts;
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

        public void Save(ItemInfo iteminfo)
        {
            session.BeginTransaction();
           var item= session.Query<Item>().Where(d => d.Code == iteminfo.Code).FirstOrDefault();
            if (item != null)
            {
                item.Name = iteminfo.Name;
                item.Unit = int.Parse(iteminfo.Unit);
                item.UnitPrice = iteminfo.UnitPrice;
                session.SaveOrUpdate(item);
            }
            else
            {
                item = new Item();
                item.Name = iteminfo.Name;
                item.Unit = int.Parse(iteminfo.Unit);
                item.UnitPrice = iteminfo.UnitPrice;
                item.Code = iteminfo.Code;
                session.Save(item);
            }
            session.Transaction.Commit();

        }
    }
}
