using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketService.Interfaces;
using MarketService.Models;
using MarketService.Models.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MarketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IInvoiceRepository InvoiceRepository { get; set; }
        public LoginController(IInvoiceRepository invoiceRepository)
        {
            InvoiceRepository = invoiceRepository;
        }
        [HttpGet]
        public string GetUser()
        {
            var iteminfo = new ItemInfo();
            iteminfo.Code = "7575";
            iteminfo.Name = "Titap";
            iteminfo.Unit = "Number";
            iteminfo.UnitPrice = 2000;

            var serialize = JsonConvert.SerializeObject(iteminfo);

            return serialize;
        }
    }
}