using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MarketService.Models;
using MarketService.Models.Contracts;

namespace MarketService.Interfaces
{
   public interface IInvoiceRepository
    {
        void Save(InvoiceInfo invoice);
        void Delete(Guid id);
        Invoice Find(Guid id);
        List<Invoice> GetInvoicesViaUser(Guid userid);
    }
}
