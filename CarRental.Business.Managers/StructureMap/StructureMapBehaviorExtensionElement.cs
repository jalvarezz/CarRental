using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Managers.StructureMap
{
    public class StructureMapBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new StructureMapServiceBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(StructureMapServiceBehavior);
            }
        }
    }
}
