using StructureMap.Pipeline;

namespace Core.Common.ServiceModel.Pipeline
{
    public class HttpContextAndPageLifecycleStrategy : LifecycleStrategy
    {
        public HttpContextAndPageLifecycleStrategy() : base(
            () => HttpContextLifecycle.HasContext() && HttpContext.Current.Handler is Page, 
            () => new HttpContextLifecycle(), 
            c => ((Page)HttpContext.Current.Handler).Unload += ((s,e) => c.DisposeAndClear())) {}
    }
}


