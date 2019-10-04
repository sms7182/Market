using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Mapping;
using MarketService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Mapping
{
    public class InvoiceMapping : IAutoMappingOverride<Invoice>
    {
       
        public void Override(AutoMapping<Invoice> mapping)
        {
            mapping.Id(d => d.Id).GeneratedBy.Assigned();
            mapping.Schema("Market");
            mapping.References(d => d.CreatedBy).Column("CreatedById").ReadOnly().Not.LazyLoad();
            mapping.References(d => d.Store).Column("StoreId").ReadOnly().Not.LazyLoad();

            mapping.Map(d => d.CreatedById).Column("CreatedById");
            mapping.Map(d => d.StoreId).Column("StoreId");
            mapping.Map(d=>d.CreationDate);
            mapping.HasMany(d => d.InvoiceLines).Cascade.All().KeyColumn("InvoiceId");
        }
    }

    public class InvoiceLineMapping :  IAutoMappingOverride<InvoiceLine>
    {
      
        public void Override(AutoMapping<InvoiceLine> mapping)
        {
            mapping.Id(d => d.Id).GeneratedBy.Assigned();
            mapping.Schema("Market");
            mapping.References(d => d.Item).Column("ItemId");
       

        }
    }
}
