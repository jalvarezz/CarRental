using System.ServiceModel;

namespace CarRental.Business.Managers.Pipeline
{
    public class WcfOperationLifecycleStrategy : LifecycleStrategy
    {
        public WcfOperationLifecycleStrategy() : base(
            WcfOperationLifecycle.HasContext, 
            () => new WcfOperationLifecycle(), 
            c => OperationContext.Current.OperationCompleted += ((s, e) => c.DisposeAndClear())) {}
    }
}


