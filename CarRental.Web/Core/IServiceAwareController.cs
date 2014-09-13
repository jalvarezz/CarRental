using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Web.Core
{
    public interface IServiceAwareController
    {
        List<IServiceContract> DisposableServices { get; }

        void RegisterDisposableServices(List<IServiceContract> disposableServices);
    }
}
