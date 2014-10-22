using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.TypeRules;

namespace CarRental.Business {
    public class BusinessEngineFactory : IBusinessEngineFactory {

        private readonly IContainer _Container;

        public BusinessEngineFactory(IContainer Container)
        {
            _Container = Container;
        }

        #region IBusinessEngineFactory Members

        public T GetBusinessEngine<T>() where T : IBusinessEngine
        {
            var candidate = _Container.TryGetInstance<T>();

            if (candidate == null)
                throw new NullReferenceException("The requested BusinessEngine Type was not registered with the container.");

            return candidate;
        }

        #endregion
    }
}
