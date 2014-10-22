using System.ServiceModel;
using StructureMap;
using StructureMap.Pipeline;
using CarRental.Business.Bootstrapper.Pipeline;
using CarRental.Business.Bootstrapper.Configuration.DSL.Expressions;

namespace CarRental.Business.Bootstrapper
{
    public class Registry
    {
        private static Registry _registry;

        private Registry()
        {
            ObjectFactory.Configure(
                config => config.For<INumberGenerator>().
                          LifecycleStrategiesAre(
                              new WcfOperationLifecycleStrategy(),
                              new LifecycleStrategy(() => new ThreadLocalStorageLifecycle())).
                          Use<NumberGenerator>());
        }

        public static Registry Current
        {
            get
            {
                if (_registry == null) _registry = new Registry();
                return _registry;
            }
        }

        public T Resolve<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}
