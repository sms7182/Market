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
    public class ItemController : ControllerBase
    {
        public IItemRepository ItemRepository { get; set; }
        public ItemController(IItemRepository itemRepository)
        {
            ItemRepository = itemRepository;
        }
        [HttpGet("byId")]
        public ActionResult<string> GetItem(string itemcode)
        {

           var d= ItemRepository.GetItemByCode(itemcode);
           var ser= JsonConvert.SerializeObject(d);
            return ser;
        }

        public void Post([FromBody]ItemInfo iteminfo)
        {
            if (iteminfo != null)
            {
                ItemRepository.Save(iteminfo);
            }
        }



    }
}