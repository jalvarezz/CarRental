using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Graph;
using StructureMap.Configuration;

namespace CarRental.Business.Bootstrapper.StructureMap
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            //// Not shown: wiring up all other WCF extensions
            //For<IParameterInspector>().Add<OperationResultParameterInspector>();
            //For<IRentalService>().Use<RentalManager>();
        }
    }
}
