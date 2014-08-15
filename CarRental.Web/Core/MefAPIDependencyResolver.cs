using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using Core.Common.Extensions;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace CarRental.Web.Core
{
    public class MefAPIDependencyResolver : IDependencyResolver
    {
        public MefAPIDependencyResolver(CompositionContainer container)
        {
            _Container = container;
        }

        CompositionContainer _Container;

        public object GetService(Type serviceType)
        {
            return _Container.GetExportedValueByType(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _Container.GetExportedValuesByType(serviceType);
        }
    }
}