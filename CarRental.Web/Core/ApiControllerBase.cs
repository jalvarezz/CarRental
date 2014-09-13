using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.ServiceModel;
using System.Web.Http;

namespace CarRental.Web.Core
{
    public class ApiControllerBase : ApiController, IServiceAwareController
    {
        protected HttpResponseMessage GetHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> codeToExecute)
        {
            HttpResponseMessage response = null;

            try
            {
                response = codeToExecute.Invoke();
            }
            catch (SecurityException ex)
            {
                response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (FaultException<AuthorizationValidationException> ex)
            {
                response = request.CreateResponse(HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        protected virtual void RegisterServices(List<IServiceContract> disposableServices)
        {
            return;
        }

        private List<IServiceContract> _DisposableServices;
        List<IServiceContract> IServiceAwareController.DisposableServices
        {
            get
            {
                if (_DisposableServices == null)
                    _DisposableServices = new List<IServiceContract>();

                return _DisposableServices;
            }
        }

        public void RegisterDisposableServices(List<IServiceContract> disposableServices)
        {
            RegisterServices(disposableServices);
        }

        protected void ValidateAuthorizedUser(string userRequested)
        {
            string userLoggedIn = User.Identity.Name;
            if (userLoggedIn != userRequested)
                throw new SecurityException("Attempting to access data for another user.");
        }
    }
}