using Core.Common.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace CarRental.Web.Core
{
    public class CustomControllerFactory : IControllerFactory
    {
        private readonly DefaultControllerFactory _defaultControllerFactory;
        private readonly CompositionContainer _container;

        public CustomControllerFactory(CompositionContainer container)
        {
            _defaultControllerFactory = new DefaultControllerFactory();
            _container = container;
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = _container.GetExportedValue<IController>(controllerName);

            if (controller == null)
                throw new Exception("Controller not found!");

            return controller;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;

            if (disposableController != null)
            {
                disposableController.Dispose();
            }
        }
    }
}