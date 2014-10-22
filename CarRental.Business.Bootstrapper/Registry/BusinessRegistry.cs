using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Configuration.DSL;
using CarRental.Data;
using StructureMap.Graph;
using StructureMap.Configuration;
using Core.Common.Contracts;

namespace CarRental.Business.Bootstrapper.StructureMap
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();

                For<IBusinessEngineFactory>().Use<BusinessEngineFactory>();
            });
        }
    }
}
