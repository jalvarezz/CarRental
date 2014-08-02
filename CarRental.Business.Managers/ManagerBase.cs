using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ServiceModel;

namespace CarRental.Business.Managers {
    public class ManagerBase {
        public ManagerBase() {
            if (ObjectBase.Container != null)
                ObjectBase.Container.SatisfyImportsOnce(this);
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codeToExecute) {
            try {
                return codeToExecute.Invoke();
            }
            catch (FaultException ex) {
                throw ex;
            }
            catch (Exception ex) {
                throw new FaultException(ex.Message);
            }
        }

        protected void ExecuteFaultHandledOperation(Action codeToExecute) {
            try {
                codeToExecute.Invoke();
            }
            catch (FaultException ex) {
                throw ex;
            }
            catch (Exception ex) {
                throw new FaultException(ex.Message);
            }
        }
    }
}
