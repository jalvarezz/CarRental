using CarRental.Client.Entities;
using Core.Common.Contracts;
using Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Client.Contracts
{
    [ServiceContract]
    public interface IRentalService : IServiceContract
    {
        [OperationContract]
        [FaultContract(typeof(UnableToRentForDateException))]
        [FaultContract(typeof(CarCurrentlyRentedException))]
        [FaultContract(typeof(NotFoundException))]
        Rental RentCarToCustomer(string loginEmail, int carId, DateTime dateDueBack);

        [OperationContract(Name="RentCarToCustomerByRentalDate")] /*This was added because it was giving an Exception (WCF doesnt support overloads)*/
        [FaultContract(typeof(UnableToRentForDateException))]
        [FaultContract(typeof(CarCurrentlyRentedException))]
        [FaultContract(typeof(NotFoundException))]
        Rental RentCarToCustomer(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack);

        [OperationContract]
        [FaultContract(typeof(CarNotRentedException))]
        void AcceptCarReturn(int carId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        IEnumerable<Rental> GetRentalHistory(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Reservation GetReservation(int reservationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Reservation MakeReservation(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        void ExecuteRentalFromReservation(int reservationId);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void CancelReservation(int reservationId);

        [OperationContract]
        CustomerReservationData[] GetCurrentReservations();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        CustomerReservationData[] GetCustomerReservations(string loginEmail);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        Rental GetRental(int rentalId);

        [OperationContract]
        CustomerRentalData[] GetCurrentRentals();

        [OperationContract]
        Reservation[] GetDeadReservations();

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        [FaultContract(typeof(AuthorizationValidationException))]
        bool IsCarCurrentlyRented(int carId);

        [OperationContract]
        Task<Rental> RentCarToCustomerAsync(string loginEmail, int carId, DateTime dateDueBack);

        [OperationContract(Name = "RentCarToCustomerByRentalDate")] /*This was added because it was giving an Exception (WCF doesnt support overloads)*/
        Task<Rental> RentCarToCustomerAsync(string loginEmail, int carId, DateTime rentalDate, DateTime dateDueBack);

        [OperationContract]
        Task AcceptCarReturnAsync(int carId);

        [OperationContract]
        Task<IEnumerable<Rental>> GetRentalHistoryAsync(string loginEmail);

        [OperationContract]
        Task<Reservation> GetReservationAsync(int reservationId);

        [OperationContract]
        Task<Reservation> MakeReservationAsync(string loginEmail, int carId, DateTime rentalDate, DateTime returnDate);

        [OperationContract]
        Task ExecuteRentalFromReservationAsync(int reservationId);

        [OperationContract]
        Task CancelReservationAsync(int reservationId);

        [OperationContract]
        Task<CustomerReservationData[]> GetCurrentReservationsAsync();

        [OperationContract]
        Task<CustomerReservationData[]> GetCustomerReservationsAsync(string loginEmail);

        [OperationContract]
        Task<Rental> GetRentalAsync(int rentalId);

        [OperationContract]
        Task<CustomerRentalData[]> GetCurrentRentalsAsync();

        [OperationContract]
        Task<Reservation[]> GetDeadReservationsAsync();

        [OperationContract]
        Task<bool> IsCarCurrentlyRentedAsync(int carId);
    }
}
