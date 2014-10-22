using CarRental.Business.Bootstrapper.StructureMap;
using CarRental.Business.Managers.Pipeline;
using CarRental.Data;
using StructureMap;
using StructureMap.Pipeline;
using System.ServiceModel;
using CarRental.Business.Managers.Configuration.DSL.Expressions;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace CarRental.Business.Managers.StructureMap
{
    public class StructureMapServiceBehavior : IServiceBehavior
    {
        private readonly Container _Container;

        public StructureMapServiceBehavior()
        {
            _Container = new Container();

            new ContainerConfigurer().Configure(_Container);
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
        public void Configure(Container container)
        {
            container.Configure(cfg =>
            {
                cfg.AddRegistry(new StandardRegistry());
                cfg.AddRegistry(new CoreRegistry());
                cfg.AddRegistry(new CarRental.Data.DataRegistry());
                cfg.AddRegistry(new BusinessRegistry());
                cfg.AddRegistry(new ServicesRegistry());

                cfg.For<INumberGenerator>().
                        LifecycleStrategiesAre(
                            new WcfOperationLifecycleStrategy(),
                            new LifecycleStrategy(() => new ThreadLocalStorageLifecycle())).
                        Use<NumberGenerator>();
            });
        }
    }
}
