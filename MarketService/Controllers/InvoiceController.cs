using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketService.Interfaces;
using MarketService.Models.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
namespace MarketService.Controllers
{

    public class ROCR
    {
        public int requestid { get; set; }
        public string content { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        
        ILogger<InvoiceController> logger;
        public IInvoiceRepository InvoiceRepository { get; set; }
        public InvoiceController(IInvoiceRepository _invoiceRepository,ILogger<InvoiceController> _logger)
        {
            InvoiceRepository = _invoiceRepository;
            logger = _logger;
        }
         [HttpGet()]
        public ActionResult<string> Get(Guid id)
        {

            if (id == Guid.Empty)
            {
                return "Ok";
            }
           var invoice= InvoiceRepository.Find(id);
           
            var ser = JsonConvert.SerializeObject(invoice);
            return ser;
        }

        // POST api/values
       //  [HttpPost()]
        public ActionResult<string> Post([FromBody]InvoiceInfo invoiceInfo)
        {

            
            if (logger != null)
            {
                logger.Log(LogLevel.Information, "Founded Post method");
              
            }

            var result = false;
            if (invoiceInfo != null)
            { 
                logger.Log(LogLevel.Information,"start saving");
                result = InvoiceRepository.Save(invoiceInfo);
            }

            var resultJson = JsonConvert.SerializeObject(result);
            return resultJson;
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

        [HttpGet("byUserId")]
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