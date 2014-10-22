using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using CarRental.Business.Managers.Pipeline;
using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Configuration;
using CarRental.Business.Managers.Configuration.DSL.Expressions;
using CarRental.Data;
using CarRental.Business.Bootstrapper.StructureMap;

namespace CarRental.Business.Managers.StructureMap
{
    //NOTE: This doesnt work with self hoster environment (Thats why its commented)
    public class StructureMapServiceHostFactory : ServiceHostFactory
    {
        private readonly Container _Container;

        public StructureMapServiceHostFactory()
        {
            _Container = new Container();

            //new ContainerConfigurer().Configure(_Container);
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new StructureMapServiceHost(_Container, serviceType, baseAddresses);
        }
    }

    //public class ContainerConfigurer
    //{
    //    public void Configure(Container container)
    //    {
    //        container.Configure(cfg =>
    //        {
    //            cfg.AddRegistry(new StandardRegistry());
    //            cfg.AddRegistry(new CoreRegistry());
    //            cfg.AddRegistry(new DataRegistry());
    //            cfg.AddRegistry(new BusinessRegistry());
    //            cfg.AddRegistry(new ServicesRegistry());

    //            cfg.For<INumberGenerator>().
    //                    LifecycleStrategiesAre(
    //                        new WcfOperationLifecycleStrategy(),
    //                        new LifecycleStrategy(() => new ThreadLocalStorageLifecycle())).
    //                    Use<NumberGenerator>();
    //        });
    //    }
    //}
}
