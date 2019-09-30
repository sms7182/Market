using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketService.Interfaces;
using MarketService.Models.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {

        public IStoreRepository StoreRepository { get; set; }
        public StoreController(IStoreRepository _storeRepository)
        {
            StoreRepository = _storeRepository;
        }
         [HttpGet("byid")]
        public ActionResult<string> Get(Guid id)
        {
           var store= StoreRepository.Find(id);
           
            var ser = JsonConvert.SerializeObject(store);
            return ser;
        }        

        [HttpGet("getAll")]
        public ActionResult<string> GetAll()
        {
          var stores=  StoreRepository.GetStores();
          if (stores != null && stores.Any())
           {
              var storesjs =  JsonConvert.SerializeObject(stores);
                return storesjs;
           }
            return string.Empty;
        }


    }
}