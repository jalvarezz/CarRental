using AttributeRouting.Web.Mvc;
using CarRental.Web.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Web.Controllers.MVC
{
    [Export("Customer", typeof(IController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class CustomerController : ViewControllerBase
    {
        [HttpGet]
        [GET("customer/reserve")]
        public ActionResult ReserveCar()
        {
            return View();
        }


        //
        // GET: /Customer/
        public ActionResult Index()
        {
            return View();
        }
	}
}