using StructureMap;
using StructureMap.Pipeline;
using System.ServiceModel;

namespace CarRental.Business.Managers.Pipeline
{
    public class WcfOperationLifecycle : ILifecycle
    {
        public static readonly string ITEM_NAME = "STRUCTUREMAP-INSTANCES";

        public void EjectAll(ILifecycleContext context)
        {
            FindCache(context).DisposeAndClear();
        }

        public IObjectCache FindCache(ILifecycleContext context)
        {
            if (!OperationContext.Current.OutgoingMessageProperties.ContainsKey(ITEM_NAME))
                OperationContext.Current.OutgoingMessageProperties.Add(ITEM_NAME, context);
            return (IObjectCache)OperationContext.Current.OutgoingMessageProperties[ITEM_NAME]; 
        }

        public string Description { get { return "WcfOperationLifecycle"; } }

        public static bool HasContext()
        {
            return OperationContext.Current != null;
        }
    }
}


