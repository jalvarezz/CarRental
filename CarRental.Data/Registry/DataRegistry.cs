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
using Core.Common.Contracts;

namespace CarRental.Data
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();

                For(typeof(IRepository<>)).Use(typeof(Repository<>));
                For<IRepositoryFactory>().Use<RepositoryFactory>();
                For<IUnitOfWork>().Use<UnitOfWork>();

                For<DbContext>().Use<CarRentalContext>();
            });
        }
    }
}
