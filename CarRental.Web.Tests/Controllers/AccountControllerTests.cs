using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CarRental.Web.Core;
using CarRental.Web.Controllers;
using CarRental.Web.Models;
using System.Web.Mvc;

namespace CarRental.Web.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Login()
        {
            Mock<ISecurityAdapter> mockSecurityAdapter = new Mock<ISecurityAdapter>();
            mockSecurityAdapter.Setup(obj => obj.Initialize());

            string returnUrl = "/testreturnurl";

            AccountController controller = new AccountController(mockSecurityAdapter.Object);

            ActionResult result = controller.Login(returnUrl);

            Assert.IsTrue(result is ViewResult);

            ViewResult viewResult = result as ViewResult;

            Assert.IsTrue(viewResult.Model is AccountLoginModel);

            AccountLoginModel model = viewResult.Model as AccountLoginModel;

            Assert.IsTrue(model.ReturnUrl == returnUrl);
        }
    }
}
