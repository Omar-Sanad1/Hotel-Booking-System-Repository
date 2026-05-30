using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ReservationService : IReservationService
    {
        private readonly HotelBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        public ReservationService(HotelBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationToReturnDTO>> GetAllReservationsAsync()
        {
            var reservations = await _dbContext.Reservations.Include(r => r.Customer).Include(r => r.Room).ToListAsync();
            var reservationsMapping = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationToReturnDTO>>(reservations);
            return reservationsMapping;
        }
        public async Task<ReservationToReturnDTO> GetReservationByIDAsync(int id)
        {
            var specifiedReservation =   await _dbContext.Reservations.
                                         Include(r => r.Customer).Include(r => r.Room).
                                         FirstOrDefaultAsync(r=>r.ID == id);

            if (specifiedReservation is null)
                throw new Exception("This reservation id isn't available");

            var specifiedReservationMapping = _mapper.Map<Reservation, ReservationToReturnDTO>(specifiedReservation);
            return specifiedReservationMapping;
        }
        public async Task<IEnumerable<ReservationToReturnDTO>> GetCustomerReservationsAsync(int customerid)
        {
            var customerReservations = await _dbContext.Reservations.
                                       Include(r => r.Customer).
                                       Include(r => r.Room).
                                       Where(r => r.CustomerID == customerid)
                                       .ToListAsync();
            return _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationToReturnDTO>>(customerReservations);
        }
        public async Task<ReservationToReturnDTO> CreateReservationAsync(CreateReservationDTO createReservation)
        {
            //  اتءكد ان الغرفة موجودة 
            var specifiedRoom = await _dbContext.Rooms.FirstOrDefaultAsync(r => r.ID == createReservation.RoomID);
            if (specifiedRoom is null)
                throw new Exception("This room id isn't available");

            // اتءكد ان الغرفة متاحة يعني مش محجوزة
            if(specifiedRoom.AvailabilityStatus != "Available")
                throw new Exception("This room availability isn't available");

            // حساب السعر اوتوماتيك
            var nightsNumber = (createReservation.CheckOutDate - createReservation.CheckInDate).Days;

            if (nightsNumber <= 0)
                throw new Exception("Error! The nights number must be greater than zero");

            var pricePerNights = nightsNumber * specifiedRoom.PricePerNight;

            var reservation = new Reservation
            {
                CustomerID = createReservation.CustomerID,
                RoomID = createReservation.RoomID,
                CheckInDate = createReservation.CheckInDate,
                CheckOutDate = createReservation.CheckOutDate,
                BookingDate = DateTime.Now,
                TotalPrice = (double)pricePerNights,
                PaymentStatus="Pending",
                ReservationStatus = "Confirmed"
            };

            await _dbContext.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();

            specifiedRoom.AvailabilityStatus = "Reserved";
            await _dbContext.SaveChangesAsync();

            return await GetReservationByIDAsync(reservation.ID);
        }
        public async Task<ReservationToReturnDTO> CancelReservationAsync(int reservationid)
        {
            var cancelledReservation = await 
                                       _dbContext.Reservations.Include(r => r.Room).
                                       FirstOrDefaultAsync(r => r.ID == reservationid);

            if (cancelledReservation is null)
                throw new Exception($"This is no reservation id = {reservationid}");

            if (cancelledReservation.ReservationStatus == "Cancelled")
                throw new Exception("This reservation is already cancelled");

            cancelledReservation.ReservationStatus = "Cancelled";

            cancelledReservation.Room.AvailabilityStatus = "Available";

            await _dbContext.SaveChangesAsync();

            return await GetReservationByIDAsync(reservationid);
        }


    }
}
