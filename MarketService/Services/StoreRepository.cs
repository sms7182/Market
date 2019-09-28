using MarketService.Interfaces;
using MarketService.Models;
using MarketService.Models.Contracts;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Services
{
    public class StoreRepository : IStoreRepository
    {
        ISession session;
        public StoreRepository(ISession _session)
        {
            session = _session;
        }


        public StoreInfo Find(Guid id)
        {
            var store = session.Get<Store>(id);
            if (store != null)
            {
                var storeInfo = new StoreInfo();
                storeInfo.Id = store.Id;
                storeInfo.Name = store.Name;
                storeInfo.Address = store.Address;
                storeInfo.Lat = store.Lat;
                storeInfo.Lng = store.Lng;
                storeInfo.City = store.City;
                storeInfo.State = store.State;

                return storeInfo;
            }

            return new StoreInfo();
        }

        public List<StoreInfo> GetStores()
        {
            var stores = session.Query<Store>().ToList();
            List<StoreInfo> storeInfos = new List<StoreInfo>();
            for (var i = 0; i < stores.Count; i++)
            {
                var store = stores[i];

                var storeInfo = new StoreInfo();
                storeInfo.Id = store.Id;
                storeInfo.Name = store.Name;
                storeInfo.Address = store.Address;
                storeInfo.Lat = store.Lat;
                storeInfo.Lng = store.Lng;
                storeInfo.City = store.City;
                storeInfo.State = store.State;

                storeInfos.Add(storeInfo);
            }

            return storeInfos;
        }

    }
}
