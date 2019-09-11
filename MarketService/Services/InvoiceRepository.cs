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
    public class InvoiceRepository : IInvoiceRepository
    {
        ISession session;
        public InvoiceRepository(ISession _session)
        {
            session = _session;
        }
        public void Delete(Guid id)
        {
           var invoice= session.Get<Invoice>(id);
            if (invoice != null)
            {
                session.Delete(invoice);
                
            }
        }

        public Invoice Find(Guid id)
        {
            var invoice=session.Get<Invoice>(id);
            return invoice;
        }

        public List<Invoice> GetInvoicesViaUser(Guid userid)
        {
           var invoices= session.Query<Invoice>().Where(d => d.CreatedById == userid).ToList();
            return invoices;
        }

        public void Save(InvoiceInfo invoiceinfo)
        {
            var invoice = session.Get<Invoice>(invoiceinfo.Id);
            if (invoice != null)
            {
                invoice.NetPrice = invoiceinfo.NetPrice;
                invoice.TotalPrice = invoiceinfo.TotalPrice;
                session.SaveOrUpdate(invoice);
            }
            else
            {
                invoice = new Invoice();
                invoice.TotalPrice = invoiceinfo.TotalPrice;
                invoice.NetPrice = invoiceinfo.NetPrice;
                invoice.Code = invoiceinfo.Code;
                invoice.CreatedBy = invoice.CreatedBy;
                invoice.CreationDate = DateTime.UtcNow;
                session.Save(invoice);
            }
            
        }
    }
}
