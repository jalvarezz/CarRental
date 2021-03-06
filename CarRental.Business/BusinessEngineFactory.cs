﻿using Core.Common.Contracts;
using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business {
    [Export(typeof(IBusinessEngineFactory))]
    public class BusinessEngineFactory : IBusinessEngineFactory {

        #region IBusinessEngineFactory Members

        public T GetBusinessEngine<T>() where T : IBusinessEngine {
            return ObjectBase.Container.GetExportedValue<T>();
        }

        #endregion
    }
}
