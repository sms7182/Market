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
        bool Save(InvoiceInfo invoice);
        void Delete(Guid id);
        InvoiceInfo Find(Guid id);
        List<FlatInvoiceInfo> GetInvoicesViaUser(Guid userid);
    }
}
