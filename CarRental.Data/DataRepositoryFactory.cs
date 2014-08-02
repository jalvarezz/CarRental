using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data {
    [Export(typeof(IDataRepositoryFactory))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataRepositoryFactory : IDataRepositoryFactory {

        #region IDataRepositoryFactory Members

        public T GetDataRepository<T>() where T : IDataRepository {
            return ObjectBase.Container.GetExportedValue<T>();
        }

        #endregion
    }
}
