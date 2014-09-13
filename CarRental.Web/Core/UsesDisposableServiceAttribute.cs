using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CarRental.Web.Core
{
    public class UsesDisposableService : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            // pre-processing

            IServiceAwareController controller = actionContext.ControllerContext.Controller as IServiceAwareController;

            if(controller != null)
            {
                controller.RegisterDisposableServices(controller.DisposableServices);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // pre-processing

            IServiceAwareController controller = actionExecutedContext.ActionContext.ControllerContext.Controller as IServiceAwareController;

            if(controller != null)
            {
                foreach (var service in controller.DisposableServices)
                {
                    if (service != null && service is IDisposable)
                        (service as IDisposable).Dispose();
                }
            }
        }
    }
}