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

        public InvoiceInfo Find(Guid id)
        {
            var invoice=session.Get<Invoice>(id);
            if (invoice != null) {
                return MapInvoiceToInvoiceInfo(invoice);
            }
            
            return new InvoiceInfo();
        }
        private InvoiceInfo MapInvoiceToInvoiceInfo(Invoice invoice)
        {
            var invoiceforreturn = new InvoiceInfo();
            invoiceforreturn.Id = invoice.Id;
            invoiceforreturn.StoreName = invoice.Store.Name;
            invoiceforreturn.NetPrice = invoice.NetPrice;
            invoiceforreturn.TotalPrice = invoice.TotalPrice;
            invoiceforreturn.Code = invoice.Code;
            //invoiceforreturn.CreatedBy = invoice.CreatedBy.UserName;
            //invoiceforreturn.CreatedById = invoice.CreatedBy.Id;
            invoiceforreturn.CreationDate = invoice.CreationDate;
            for (var i = 0; i < invoice.InvoiceLines.Count; i++)
            {
                var temp = invoice.InvoiceLines.ToList()[i];
                var invoiceInfoLineTemp = new InvoiceInfoLine();
                invoiceInfoLineTemp.Id = temp.Id;
                invoiceInfoLineTemp.ItemCode = temp.Item.Code;
                invoiceInfoLineTemp.ItemName = temp.Item.Name;
                invoiceInfoLineTemp.ItemId = temp.Item.Id;
                invoiceInfoLineTemp.NetPrice = temp.NetPrice;
                invoiceInfoLineTemp.UnitPrice = temp.UnitPrice;
                invoiceInfoLineTemp.Quantity = temp.Quantity;
                invoiceInfoLineTemp.TotalPrice = temp.TotalPrice;
                invoiceforreturn.InvoiceInfoLines.Add(invoiceInfoLineTemp);
            }
            return invoiceforreturn;

        }
        public List<InvoiceInfo> GetInvoicesViaUser(Guid userid)
        {
           var invoices= session.Query<Invoice>().Where(d => d.CreatedById == userid).ToList();
            List<InvoiceInfo> invoiceInfos = new List<InvoiceInfo>();
            for(var i = 0; i < invoices.Count; i++)
            {
               var tempInfo= MapInvoiceToInvoiceInfo(invoices[i]);
                invoiceInfos.Add(tempInfo);
            }
            return invoiceInfos;
        }

        public bool Save(InvoiceInfo invoiceinfo)
        {
            var success = false;
            try
            {
                session.BeginTransaction();
                var invoice = session.Get<Invoice>(invoiceinfo.Id);
                if (invoice != null)
                {
                    invoice.NetPrice = invoiceinfo.NetPrice;
                    invoice.TotalPrice = invoiceinfo.TotalPrice;

                    var resrictions = Restrictions.On<Item>(val => val.Code).IsIn(invoiceinfo.InvoiceInfoLines.Select(d => d.ItemCode).ToList());
                    var loadedItems = session.QueryOver<Item>().Where(resrictions).List();
                    for (var i = 0; i < invoiceinfo.InvoiceInfoLines.Count; i++)
                    {
                        var tempinfo = invoiceinfo.InvoiceInfoLines[i];
                        if (invoice.InvoiceLines.Any(d => d.Item.Code == tempinfo.ItemCode))
                        {
                            var lineFounded = invoice.InvoiceLines.FirstOrDefault(d => d.Item.Code == tempinfo.ItemCode);
                            lineFounded.Quantity = tempinfo.Quantity;
                            lineFounded.NetPrice = tempinfo.NetPrice;
                            lineFounded.UnitPrice = tempinfo.UnitPrice;
                            lineFounded.TotalPrice = tempinfo.TotalPrice;

                        }
                        else
                        {
                            var invoiceLine = new InvoiceLine();
                            var item = loadedItems.FirstOrDefault(d => d.Code == tempinfo.ItemCode);
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
                    //var user = session.Query<User>().Where(d => d.UserName == invoiceinfo.CreatedBy).FirstOrDefault();
                    invoice = new Invoice();
                    invoice.TotalPrice = invoiceinfo.TotalPrice;
                    invoice.NetPrice = invoiceinfo.NetPrice;
                    invoice.Code = invoiceinfo.Code;
                    invoice.CreatedById = invoiceinfo.CreatedById;
                    invoice.CreationDate = invoiceinfo.CreationDate;
                    //invoice.Store = session.Get<Store>(invoiceinfo.StoreId);
                    invoice.StoreId = invoiceinfo.StoreId;

                    var resrictions = Restrictions.On<Item>(val => val.Code).IsIn(invoiceinfo.InvoiceInfoLines.Select(d => d.ItemCode).ToList());
                    var loadedItems = session.QueryOver<Item>().Where(resrictions).List();
                    for (var i = 0; i < invoiceinfo.InvoiceInfoLines.Count; i++)
                    {
                        var tempinfo = invoiceinfo.InvoiceInfoLines[i];
                        var invoiceLine = new InvoiceLine();
                        var item = loadedItems.FirstOrDefault(d => d.Code == tempinfo.ItemCode);
                        invoiceLine.Item = item;
                        invoiceLine.Quantity = tempinfo.Quantity;
                        invoiceLine.NetPrice = tempinfo.Quantity * tempinfo.UnitPrice;
                        invoiceLine.UnitPrice = tempinfo.UnitPrice;
                        invoiceLine.TotalPrice = tempinfo.TotalPrice;

                        invoice.InvoiceLines.Add(invoiceLine);

                    }
                    session.Save(invoice);
                }

                session.Transaction.Commit();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
