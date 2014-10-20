using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Data;
using StructureMap;
using Core.Common.Core;
using System.Data.Entity;
using StructureMap.Configuration.DSL;

namespace CarRental.Business.Bootstrapper
{
    public static class StructureMapLoader
    {
        public static void Init() {

            ObjectFactory.Initialize(cfg =>
            {
                cfg.AddRegistry(new CoreRegistry());
                cfg.AddRegistry(new DataRegistry());
                cfg.AddRegistry(new StandardRegistry());
            });

            //AggregateCatalog catalog = new AggregateCatalog();

            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(AccountRepository).Assembly));
            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(CarRentalEngine).Assembly));

            //CompositionContainer container = new CompositionContainer(catalog);

            //return container;
        }
    }
}
