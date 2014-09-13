using AttributeRouting.Web.Http;
using CarRental.Client.Contracts;
using CarRental.Client.Entities;
using CarRental.Web.Core;
using CarRental.Web.Models;
using Core.Common.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace CarRental.Web.Controllers.API
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    [UsesDisposableService]
    public class ReservationApiController : ApiControllerBase
    {
        [ImportingConstructor]
        public ReservationApiController(IInventoryService inventoryService, IRentalService rentalService)
        {
            _InventoryService = inventoryService;
            _RentalService = rentalService;
        }

        IInventoryService _InventoryService;
        IRentalService _RentalService;

        protected override void RegisterServices(List<IServiceContract> disposableServices)
        {
            disposableServices.Add(_InventoryService);
            disposableServices.Add(_RentalService);
        }

        [HttpGet]
        [GET("api/reservation/availableCars")]
        [AllowAnonymous]
        public HttpResponseMessage GetAvailableCars(HttpRequestMessage request, DateTime pickupDate, DateTime returnDate)
        {
            return GetHttpResponse(request, () =>
            {
                Car[] cars = _InventoryService.GetAvailableCars(pickupDate, returnDate);

                return request.CreateResponse<Car[]>(HttpStatusCode.OK, cars);
            });
        }

        [HttpPost]
        [POST("api/reservation/reservecar")]
        public HttpResponseMessage ReserveCar(HttpRequestMessage request, [FromBody]ReservationModel model)
        {
            return GetHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                string user = User.Identity.Name;

                Reservation reservation = _RentalService.MakeReservation(user, model.Car, model.PickupDate, model.ReturnDate);

                response = request.CreateResponse<Reservation>(HttpStatusCode.OK, reservation);

                return response;
            });
        }
    }
}