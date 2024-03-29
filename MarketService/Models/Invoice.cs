﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Models
{
    public class Invoice:BaseClass
    {
        public Invoice()
        {
            InvoiceLines = new List<InvoiceLine>();
        }
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
        public virtual string Code { get; set; }
        public virtual Store Store { get; set; }
       // public virtual Guid StoreId { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual CustomerUserInfo CreatedBy { get; set; }
        //public virtual Guid CreatedById { get; set; }
        public virtual double NetPrice { get; set; }
        public virtual double TotalPrice { get; set; }
    }
    public class InvoiceLine : BaseClass
    {
        public virtual Item Item { get; set; }

        public virtual double Quantity { get; set; }

        public virtual double  UnitPrice { get; set; }

        public virtual double NetPrice { get; set; }

        public virtual double TotalPrice { get; set; }
     
    }
}
