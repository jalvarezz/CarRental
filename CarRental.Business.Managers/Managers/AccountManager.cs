﻿using CarRental.Business.Contracts;
using CarRental.Business.Entities;
using CarRental.Common;
using CarRental.Data.Contracts;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business.Managers
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,
                     ConcurrencyMode=ConcurrencyMode.Multiple,
                     ReleaseServiceInstanceOnTransactionComplete=false)]
    public class AccountManager : ManagerBase, IAccountService
    {
        public AccountManager()
        {

        }

        public AccountManager(IRepositoryFactory repositoryFactory)
        {
            _RepositoryFactory = repositoryFactory;
        }

        IRepositoryFactory _RepositoryFactory;

        protected override Account LoadAuthorizationValidationAccount(string loginName)
        {
            IAccountRepository accountRepository = _RepositoryFactory.BuildCustomRepository<IAccountRepository>();
            Account authAcct = accountRepository.GetByLogin(loginName);

            if (authAcct == null)
            {
                NotFoundException ex = new NotFoundException(string.Format("Cannot find account for login name {0} to use for security trimming.", loginName));
                throw new FaultException<NotFoundException>(ex, ex.Message);
            }

            return authAcct;
        }

        #region IAccountService

        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public Account GetCustomerAccountInfo(string loginEmail)
        {
            return ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _RepositoryFactory.BuildCustomRepository<IAccountRepository>();

                Account accountEntity = accountRepository.GetByLogin(loginEmail);
                if(accountEntity == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("Account with login {0} is not in the database", loginEmail));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                ValidateAuthorization(accountEntity);

                return accountEntity;
            });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        [PrincipalPermission(SecurityAction.Demand, Role = Security.CarRentalAdminRole)]
        [PrincipalPermission(SecurityAction.Demand, Name = Security.CarRentalUser)]
        public void UpdateCustomerAccountInfo(Account account)
        {
            ExecuteFaultHandledOperation(() =>
            {
                IAccountRepository accountRepository = _RepositoryFactory.BuildCustomRepository<IAccountRepository>();

                ValidateAuthorization(account);

                Account updatedAccount = accountRepository.Update(account);
            });
        }

        #endregion
    }
}
