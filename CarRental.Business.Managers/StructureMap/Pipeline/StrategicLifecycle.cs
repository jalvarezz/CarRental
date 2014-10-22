using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap.Pipeline;
using StructureMap;

namespace CarRental.Business.Managers.Pipeline
{
    public class StrategicLifecycle : ILifecycle 
    {
        private List<ILifecycleStrategy> _strategies;

        public StrategicLifecycle(params ILifecycleStrategy[] strategies)
        { _strategies = strategies.ToList(); }

        public void Add(ILifecycleStrategy lifecycleStrategy)
        { _strategies.Add(lifecycleStrategy); }

        public void EjectAll(ILifecycleContext context)
        { ResolveLifecycle().EjectAll(context); }

        public IObjectCache FindCache(ILifecycleContext context)
        { return ResolveLifecycle().FindCache(context); }

        public string Description
        { get { return "StrategicLifecycle"; } }

        private ILifecycle ResolveLifecycle()
        {
            var lifecycle = (from strategy in _strategies
                             where strategy.IsValid()
                             select strategy.Lifecycle).FirstOrDefault();
            if (lifecycle == null) throw 
                new Exception("Unable to find a suitable lifecycle Strategy.");
            return lifecycle;            
        }
    }
}


