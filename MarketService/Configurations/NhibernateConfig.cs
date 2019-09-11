using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MarketService.Mapping;
using MarketService.Models;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace MarketService.Configurations
{

    public class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "MarketService.Models";
        }
    }
    public class NhibernateConfig
    {
        IConfiguration ConfigurationManager;
        public NhibernateConfig(IConfiguration configuration)
        {
            ConfigurationManager = configuration;
        }
        private ISession session;
        public ISession Session
        {
            get
            {
                if (session != null)
                {
                    return session;
                }

                session = GetSessionFactory().OpenSession();

                return session;
            }
        }

        public ISessionFactory GetSessionFactory()
        {
            var cfg = new StoreConfiguration();

            var connectionString=ConfigurationManager.GetConnectionString("DefaultConnection");
            var mappings= Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
               .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<InvoiceMapping>().UseOverridesFromAssemblyOf<InvoiceMapping>().Where(d=>d.Namespace== "MarketService.Models"
               &&d.BaseType==typeof(BaseClass))));


             var buildSessionFactory=  mappings.ExposeConfiguration(d => new SchemaExport(d)
                .Create(true,true))
               .BuildSessionFactory();
            return buildSessionFactory;
        }
    }
}
