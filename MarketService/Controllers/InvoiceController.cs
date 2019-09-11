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
    public class InvoiceController : ControllerBase
    {

        public IInvoiceRepository InvoiceRepository { get; set; }
        public InvoiceController(IInvoiceRepository _invoiceRepository)
        {
            InvoiceRepository = _invoiceRepository;
        }
         [HttpGet("byid")]
        public ActionResult<string> Get(Guid id)
        {
           var invoice= InvoiceRepository.Find(id);
            var ser = JsonConvert.SerializeObject(invoice);
            return ser;
        }

        // POST api/values
        [HttpPost("byinvoice")]
        public void Post(string invoicejs)
        {
           var invoiceinfo= JsonConvert.DeserializeObject<InvoiceInfo>(invoicejs);
            if (invoiceinfo != null)
            {
                InvoiceRepository.Save(invoiceinfo);
            }
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

        [HttpGet("byid")]
        public ActionResult<string> InvoiceHistoryPerUser(Guid userid)
        {
          var invoices=  InvoiceRepository.GetInvoicesViaUser(userid);
          if (invoices != null && invoices.Any())
           {
              var invoicesjs=  JsonConvert.SerializeObject(invoices);
                return invoicesjs;
           }
            return string.Empty;
        }


    }
}