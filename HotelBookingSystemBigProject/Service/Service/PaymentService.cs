using AutoMapper;
using Core.DTOs;
using Core.Entities;
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
    public class PaymentService : IPaymentService
    {
        private readonly HotelBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        public PaymentService(HotelBookingDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<PaymentToReturnDTO> GetPaymentByReservationIDAsync(int reservationId)
        {
            var paymentForSpecifiedReservation = await _dbContext.Payments.
                                                 FirstOrDefaultAsync(p=>p.ReservationID == reservationId);
                                                
            if (paymentForSpecifiedReservation is null)
                throw new Exception("Payment not found");

            return _mapper.Map<Payment, PaymentToReturnDTO>(paymentForSpecifiedReservation);
        }
        public async Task<PaymentToReturnDTO> CreatePaymentAsync(CreatePaymentDTO createpayment)
        {
            var specifiedReservation = await _dbContext.Reservations.FindAsync(createpayment.ReservationID);
            if (specifiedReservation is null)
                throw new Exception("Reservation not found");

            var existingPayment = _dbContext.Payments.FirstOrDefaultAsync(p => p.ReservationID == createpayment.ReservationID);
            if (existingPayment is not null)
                throw new Exception("This reservation is paid before");

            if(specifiedReservation.ReservationStatus == "Cancelled")
                throw new Exception("This reservation is cancelled");

            var payment = new Payment
            {
                ReservationID = createpayment.ReservationID,
                PaymentDate = DateTime.Now,
                PaymentMethod = createpayment.PaymentMethod,
                PaymentStatus = "Completed",
                PaymentAmount = specifiedReservation.TotalPrice
            };

            await _dbContext.Payments.AddAsync(payment);

            specifiedReservation.ReservationStatus = "Paid";

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Payment, PaymentToReturnDTO>(payment);
        }
        public async Task<PaymentToReturnDTO> UpdatePaymentStatusAsync(int paymentId, string status)
        {
            var specifiedPayment = await _dbContext.Payments.FirstOrDefaultAsync(p=>p.ID ==  paymentId);
            if (specifiedPayment is null)
                throw new Exception($"There is no payment with id = {specifiedPayment}");

            var validStatuses = new[] { "Completed" , "Failed" , "Pending" , "Refunded"};
            if (!validStatuses.Contains(status))
                throw new Exception("This non valid status");

            specifiedPayment.PaymentStatus = status;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Payment, PaymentToReturnDTO>(specifiedPayment);

        }
    }
}
