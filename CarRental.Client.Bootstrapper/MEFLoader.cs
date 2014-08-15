using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Client;
using CarRental.Client.Proxies;
using System.ComponentModel.Composition.Primitives;

namespace CarRental.Client.Bootstrapper
{
    public static class MEFLoader
    {
        public static CompositionContainer Init()
        {
            return Init(null);
        }

        public static CompositionContainer Init(ICollection<ComposablePartCatalog> catalogParts) {
            AggregateCatalog catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(InventoryClient).Assembly));

            if (catalogParts != null)
            {
                foreach (var part in catalogParts)
                {
                    catalog.Catalogs.Add(part);
                }
            }

            CompositionContainer container = new CompositionContainer(catalog);

            return container;
        }
    }
}
