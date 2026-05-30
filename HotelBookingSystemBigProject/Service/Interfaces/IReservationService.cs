using Core.DTOs;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IReservationService
    {
        public Task<IEnumerable<ReservationToReturnDTO>> GetAllReservationsAsync();
        public Task<ReservationToReturnDTO> GetReservationByIDAsync(int id);
        public Task<IEnumerable<ReservationToReturnDTO>> GetCustomerReservationsAsync(int customerid);
        public Task<ReservationToReturnDTO> CreateReservationAsync(CreateReservationDTO createReservation);
        public Task<ReservationToReturnDTO> CancelReservationAsync(int reservationid);

    }
}
