using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ServiceModel;
using CarRental.Business.Entities;
using Core.Common.Contracts;
using System.Threading;
using CarRental.Common;
using Core.Common.Exceptions;
using StructureMap;
using StructureMap.TypeRules;

namespace CarRental.Business.Managers {
    public class ManagerBase {
        public ManagerBase()
        {
            OperationContext context = OperationContext.Current;

            if(context != null)
            {
                try
                {
                    _LoginName = context.IncomingMessageHeaders.GetHeader<string>("String", "System");
                }
                catch {
                    _LoginName = string.Empty;
                }

                if (_LoginName.IndexOf(@"\") > 1) _LoginName = string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(_LoginName))
            {
                _AuthorizationAccount = LoadAuthorizationValidationAccount(_LoginName);
            }
        }

        protected virtual Account LoadAuthorizationValidationAccount(string loginName)
        {
            return null;
        }

        protected Account _AuthorizationAccount = null;
        protected string _LoginName;

        protected void ValidateAuthorization(IAccountOwnedEntity entity)
        {
            if (!Thread.CurrentPrincipal.IsInRole(Security.CarRentalAdminRole))
            {
                if (_AuthorizationAccount != null)
                {
                    if (_LoginName != string.Empty && entity.AccountId != _AuthorizationAccount.AccountId)
                    {
                        AuthorizationValidationException ex = new AuthorizationValidationException("Attempt to access a secure record for another user.");
                        throw new FaultException<AuthorizationValidationException>(ex, ex.Message);
                    }
                }
            }
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
