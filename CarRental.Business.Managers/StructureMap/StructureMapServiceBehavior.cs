using CarRental.Business.Bootstrapper.StructureMap;
using CarRental.Data;
using StructureMap;
using StructureMap.Pipeline;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using CarRental.Business.Contracts;
using Core.Common.Core;
using Core.Common.Contracts;

namespace CarRental.Business.Managers.StructureMap
{
    public class StructureMapServiceBehavior : IServiceBehavior
    {
        private IContainer _Container {
            get
            {
                return ObjectBase.SMContainer;
            }
        }

        public StructureMapServiceBehavior()
        {
            if (ObjectBase.SMContainer == null)
            {
                ObjectBase.SMContainer = new Container();

                new ContainerConfigurer().Configure(_Container);
            }
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher cd = cdb as ChannelDispatcher;
                if (cd != null)
                {
                    foreach (EndpointDispatcher ed in cd.Endpoints)
                    {
                        ed.DispatchRuntime.InstanceProvider = new StructureMapInstanceProvider(_Container, serviceDescription.ServiceType);
                        ed.DispatchRuntime.MessageInspectors.Add(new StructureMapServiceInspector(_Container));
                    }
                }
            }
        }

        public void AddBindingParameters(ServiceDescription desc, ServiceHostBase host,
                                         System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(ServiceDescription desc, ServiceHostBase host)
        {
        }
    }

    public class ContainerConfigurer
    {
        public void Configure(IContainer container)
        {
            container.Configure(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new CoreRegistry());
                cfg.AddRegistry(new CarRental.Data.DataRegistry());
                cfg.AddRegistry(new BusinessRegistry());
                cfg.AddRegistry(new ServicesRegistry());

                cfg.For<IInventoryService>().Use<InventoryManager>();
                cfg.For<IRentalService>().Use<RentalManager>();
                cfg.For<IAccountService>().Use<AccountManager>();

                cfg.For<IParameterInspector>().Use<StructureMapServiceInspector>();

                //cfg.For<INumberGenerator>().
                //        LifecycleStrategiesAre(
                //            new WcfOperationLifecycleStrategy(),
                //            new LifecycleStrategy(() => new ThreadLocalStorageLifecycle())).
                //        Use<NumberGenerator>();
            });
        }
    }
}
