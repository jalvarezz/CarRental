using AttributeRouting.Web.Http;
using CarRental.Web.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarRental.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class ReservationApiController : ApiControllerBase
    {
        [HttpGet]
        [GET("api/reservation/availableCars")]
        public HttpResponseMessage GetAvailableCars(HttpRequestMessage request, 
            DateTime pickupDate, DateTime returnDate)
        {
            return GetHttpResponse(request, () =>
            {
                return null;
            });
        }
    }
}