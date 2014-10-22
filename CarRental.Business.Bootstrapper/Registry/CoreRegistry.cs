using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Configuration.DSL;
using CarRental.Data;
using StructureMap.Graph;
using StructureMap;

namespace CarRental.Business.Bootstrapper.StructureMap
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}
