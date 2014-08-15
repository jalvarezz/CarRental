using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Proxies
{
    [Export(typeof(IServiceFactory))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class ServiceFactory : IServiceFactory
    {
        #region IServiceFactory Members

        public T CreateClient<T>() where T : IServiceContract
        {
            return ObjectBase.Container.GetExportedValue<T>();
        }

        #endregion
    }
}
