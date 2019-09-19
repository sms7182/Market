using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using MarketService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Mapping
{
    public class ItemMapping :IAutoMappingOverride<Item>
    {
        public void Override(AutoMapping<Item> mapping)
        {
            mapping.Id(d => d.Id).Not.GeneratedBy.Assigned();
            mapping.Schema("Market");
            mapping.Map(s => s.Unit).CustomType<Unit>();
           
        }
    }
}
