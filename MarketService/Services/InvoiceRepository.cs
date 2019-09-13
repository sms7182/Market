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
            session.BeginTransaction();
            var invoice = session.Get<Invoice>(invoiceinfo.Id);
            if (invoice != null)
            {
                invoice.NetPrice = invoiceinfo.NetPrice;
                invoice.TotalPrice = invoiceinfo.TotalPrice;


            var resrictions= Restrictions.On<Item>(val => val.Code).IsIn(invoiceinfo.InvoiceInfoLines.Select(d=>d.Item).ToList());
                var loadedItems=session.QueryOver<Item>().Where(resrictions).List();
                for (var i = 0; i < invoiceinfo.InvoiceInfoLines.Count; i++)
                {
                   var tempinfo= invoiceinfo.InvoiceInfoLines[i];
                    if (invoice.InvoiceLines.Any(d => d.Item.Code == tempinfo.Item))
                    {
                        var lineFounded = invoice.InvoiceLines.FirstOrDefault(d => d.Item.Code == tempinfo.Item);
                        lineFounded.Quantity = tempinfo.Quantity;
                        lineFounded.NetPrice = tempinfo.NetPrice;
                        lineFounded.UnitPrice = tempinfo.UnitPrice;
                        lineFounded.TotalPrice = tempinfo.TotalPrice;

                    }
                    else
                    {
                       var invoiceLine= new InvoiceLine();
                       var item= loadedItems.FirstOrDefault(d => d.Code == tempinfo.Item);
                        invoiceLine.Item = item;
                        invoiceLine.Quantity = tempinfo.Quantity;
                        invoiceLine.NetPrice = tempinfo.Quantity * tempinfo.UnitPrice;
                        invoiceLine.UnitPrice = tempinfo.UnitPrice;
                        invoiceLine.TotalPrice = tempinfo.TotalPrice;
                        invoice.InvoiceLines.Add(invoiceLine);
                    }
                }

                session.SaveOrUpdate(invoice);
            }
            else
            {
                var user=session.Query<User>().Where(d => d.UserName == invoiceinfo.CreatedBy).FirstOrDefault();
                invoice = new Invoice();
                invoice.TotalPrice = invoiceinfo.TotalPrice;
                invoice.NetPrice = invoiceinfo.NetPrice;
                invoice.Code = invoiceinfo.Code;
                invoice.CreatedById = user.Id;
                invoice.CreationDate = DateTime.UtcNow;
                var resrictions = Restrictions.On<Item>(val => val.Code).IsIn(invoiceinfo.InvoiceInfoLines.Select(d => d.Item).ToList());
                var loadedItems = session.QueryOver<Item>().Where(resrictions).List();
                for (var i = 0; i < invoiceinfo.InvoiceInfoLines.Count; i++)
                {
                        var tempinfo = invoiceinfo.InvoiceInfoLines[i];
                        var invoiceLine = new InvoiceLine();
                        var item = loadedItems.FirstOrDefault(d => d.Code == tempinfo.Item);
                        invoiceLine.Item = item;
                        invoiceLine.Quantity = tempinfo.Quantity;
                        invoiceLine.NetPrice = tempinfo.Quantity * tempinfo.UnitPrice;
                        invoiceLine.UnitPrice = tempinfo.UnitPrice;
                        invoiceLine.TotalPrice = tempinfo.TotalPrice;
                       
                        invoice.InvoiceLines.Add(invoiceLine);
                    
                }
                session.Save(invoice);
                session.Transaction.Commit();
            }
            
        }
    }
}
