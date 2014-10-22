using CarRental.Data;
using StructureMap;
using System.Data.Entity;
using CarRental.Business.Bootstrapper.StructureMap;
using Core.Common.Contracts;
using CarRental.Data.Contracts;

namespace CarRental.Business.Bootstrapper
{
    public static class StructureMapLoader
    {
        public static void Init() {

            ObjectFactory.Initialize(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new CoreRegistry());
                cfg.AddRegistry(new CarRental.Data.DataRegistry());
                cfg.AddRegistry(new BusinessRegistry());
                cfg.AddRegistry(new ServicesRegistry());

                //cfg.For<INumberGenerator>().
                //        LifecycleStrategiesAre(
                //            new WcfOperationLifecycleStrategy(),
                //            new LifecycleStrategy(() => new ThreadLocalStorageLifecycle())).
                //        Use<NumberGenerator>();
            });
        }
    }
}
