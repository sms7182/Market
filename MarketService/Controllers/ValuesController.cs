using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketService.Interfaces;
using MarketService.Models;
using MarketService.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace MarketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController(IInvoiceRepository invoiceRepository)
        {

        }
        // GET api/values
      //  [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
     //   [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpGet]
        public string BusinessVal()
        {
            var iteminfo=new ItemInfo();
            iteminfo.Code = "7575";
            iteminfo.Name = "Titap";
            iteminfo.Unit = "Number";
            iteminfo.UnitPrice = 2000;
           
           var serialize= JsonConvert.SerializeObject(iteminfo);

            return serialize;
        }
    }
    
}
