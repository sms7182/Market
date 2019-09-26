using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Models.Contracts
{
    public class FlatInvoiceInfo
    {

        public virtual string Code { get; set; }
        public string StoreName { get; set; }
        public Guid StoreId { get; set; }

        public virtual DateTime CreationDate { get; set; }      
        public virtual decimal NetPrice { get; set; }
        public virtual decimal TotalPrice { get; set; }
        public Guid Id { get; set; }

    }
    public class InvoiceInfo:FlatInvoiceInfo
    {
        public InvoiceInfo()
        {
            InvoiceInfoLines = new List<InvoiceInfoLine>();
        }
        public virtual string CreatedBy { get; set; }
        public Guid CreatedById { get; set; }
        public List<InvoiceInfoLine> InvoiceInfoLines { get; set; }

    }
    public class InvoiceInfoLine
    {
        public Guid Id { get; set; }
        public virtual string ItemCode { get; set; }
        public string ItemName { get; set; }
        public Guid ItemId { get; set; }

        public virtual decimal Quantity { get; set; }

        public virtual decimal UnitPrice { get; set; }

        public virtual decimal NetPrice { get; set; }

        public virtual decimal TotalPrice { get; set; }
    }

}
