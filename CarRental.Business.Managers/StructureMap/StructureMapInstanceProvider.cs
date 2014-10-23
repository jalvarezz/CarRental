using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Managers.StructureMap
{
    public class StructureMapInstanceProvider : IInstanceProvider, IContractBehavior
    {
        private readonly IContainer _Container;
        private readonly Type _ServiceType;

        public StructureMapInstanceProvider(IContainer container, Type serviceType)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (serviceType == null) throw new ArgumentNullException("serviceType");

            _Container = container;
            _ServiceType = serviceType;
        }

        public object GetInstance(System.ServiceModel.InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            return _Container.GetInstance(_ServiceType);
        }

        public object GetInstance(System.ServiceModel.InstanceContext instanceContext)
        {
            return this.GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(System.ServiceModel.InstanceContext instanceContext, object instance)
        {
            //No cleanup required
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            //throw new NotImplementedException();
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = this;
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            //throw new NotImplementedException();
        }
    }
}
