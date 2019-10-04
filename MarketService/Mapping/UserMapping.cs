using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using MarketService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Mapping
{
    public class UserMapping : IAutoMappingOverride<CustomerUserInfo>
    {
        public void Override(AutoMapping<CustomerUserInfo> mapping)
        {
            
            mapping.Schema("Market");

           // mapping.Table("CustomerUserInfo");

            mapping.Id(s => s.Id).GeneratedBy.Assigned();
            mapping.Map(d=>d.UserName).CustomSqlType("text");
             mapping.Map(d => d.Password).CustomSqlType("text");
           
        }
    }
}
