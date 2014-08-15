using CarRental.Client.Contracts;
using Core.Common.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CarRental.Client.Proxies
{
    [Export(typeof(IRentalService))]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class RentalClient : UserClientBase<IRentalService>, IRentalService
    {
        public RentalClient()
        {
            
        }

        #region IRentalService Members

        public Entities.Rental RentCarToCustomer(string loginEmail, int carId, DateTime dateDueBack)
        {
            return Channel.RentCarToCustomer(loginEmail, carId, dateDueBack);
        }

        public Entities.Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
        {
            return Channel.RentCarToCustomer(loginEmail, carId, rentalDate, dateDueBack);
        }

        public void AcceptCarReturn(int carId)
        {
            Channel.AcceptCarReturn(carId);
        }

        public IEnumerable<Entities.Rental> GetRentalHistory(string loginEmail)
        {
            return Channel.GetRentalHistory(loginEmail);
        }

        public Entities.Reservation GetReservation(int reservationId)
        {
            return Channel.GetReservation(reservationId);
        }

        public Entities.Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate)
        {
            return Channel.MakeReservation(loginEmail, carId, rentalDate, returnDate);
        }

        public void ExecuteRentalFromReservation(int reservationId)
        {
            Channel.ExecuteRentalFromReservation(reservationId);
        }

        public void CancelReservation(int reservationId)
        {
            Channel.CancelReservation(reservationId);
        }

        public CustomerReservationData[] GetCurrentReservations()
        {
            return Channel.GetCurrentReservations();
        }

        public CustomerReservationData[] GetCustomerReservations(string loginEmail)
        {
            return Channel.GetCustomerReservations(loginEmail);
        }

        public Entities.Rental GetRental(int rentalId)
        {
            return Channel.GetRental(rentalId);
        }

        public CustomerRentalData[] GetCurrentRentals()
        {
            return Channel.GetCurrentRentals();
        }

        public Entities.Reservation[] GetDeadReservations()
        {
            return Channel.GetDeadReservations();
        }

        public bool IsCarCurrentlyRented(int carId)
        {
            return Channel.IsCarCurrentlyRented(carId);
        }

        public Task<Entities.Rental> RentCarToCustomerAsync(string loginEmail, int carId, DateTime dateDueBack)
        {
            return Channel.RentCarToCustomerAsync(loginEmail, carId,dateDueBack);
        }

        public Task<Entities.Rental> RentCarToCustomerAsync(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack)
        {
            return Channel.RentCarToCustomerAsync(loginEmail, carId, rentalDate, dateDueBack);
        }

        public Task AcceptCarReturnAsync(int carId)
        {
            return Channel.AcceptCarReturnAsync(carId);
        }

        public Task<IEnumerable<Entities.Rental>> GetRentalHistoryAsync(string loginEmail)
        {
            return Channel.GetRentalHistoryAsync(loginEmail);
        }

        public Task<Entities.Reservation> GetReservationAsync(int reservationId)
        {
            return Channel.GetReservationAsync(reservationId);
        }

        public Task<Entities.Reservation> MakeReservationAsync(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate)
        {
            return Channel.MakeReservationAsync(loginEmail, carId, rentalDate, returnDate);
        }

        public Task ExecuteRentalFromReservationAsync(int reservationId)
        {
            return Channel.ExecuteRentalFromReservationAsync(reservationId);
        }

        public Task CancelReservationAsync(int reservationId)
        {
            return Channel.CancelReservationAsync(reservationId);
        }

        public Task<CustomerReservationData[]> GetCurrentReservationsAsync()
        {
            return Channel.GetCurrentReservationsAsync();
        }

        public Task<CustomerReservationData[]> GetCustomerReservationsAsync(string loginEmail)
        {
            return Channel.GetCustomerReservationsAsync(loginEmail);
        }

        public Task<Entities.Rental> GetRentalAsync(int rentalId)
        {
            return Channel.GetRentalAsync(rentalId);
        }

        public Task<CustomerRentalData[]> GetCurrentRentalsAsync()
        {
            return Channel.GetCurrentRentalsAsync();
        }

        public Task<Entities.Reservation[]> GetDeadReservationsAsync()
        {
            return Channel.GetDeadReservationsAsync();
        }

        public Task<bool> IsCarCurrentlyRentedAsync(int carId)
        {
            return Channel.IsCarCurrentlyRentedAsync(carId);
        }

        #endregion
    }
}
