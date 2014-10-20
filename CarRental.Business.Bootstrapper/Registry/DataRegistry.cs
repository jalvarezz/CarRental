using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Configuration.DSL;
using CarRental.Data;
using StructureMap.Graph;
using CarRental.Data.Contracts;
using System.Data.Entity;

namespace CarRental.Business.Bootstrapper
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();

                For<IUnitOfWork>().Use<UnitOfWork>();
                For<DbContext>().Use<CarRentalContext>();
            });
        }
    }
}
