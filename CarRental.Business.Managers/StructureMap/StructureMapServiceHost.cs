using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Managers.StructureMap
{
    public class StructureMapServiceHost : ServiceHost
    {
        public StructureMapServiceHost()
        {
        }

        public StructureMapServiceHost(Container container, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (container == null) throw new ArgumentNullException("container");

            var contracts = ImplementedContracts.Values;

            foreach (var c in contracts)
            {
                var instanceProvider = new StructureMapInstanceProvider(container, serviceType);

                c.Behaviors.Add(instanceProvider);
            }
        }

        protected override void OnOpening()
        {
            //Description.Behaviors.Add(new StructureMapServiceBehavior());
            base.OnOpening();
        }
    }
}
