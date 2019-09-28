using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using MarketService.Models;

namespace MarketService.Mapping
{
    public class StoreMapping : IAutoMappingOverride<Store>
    {
        public void Override(AutoMapping<Store> mapping)
        {
            mapping.Id(s => s.Id).Not.GeneratedBy.Assigned();
            mapping.Schema("Market");
        }
    }
}
