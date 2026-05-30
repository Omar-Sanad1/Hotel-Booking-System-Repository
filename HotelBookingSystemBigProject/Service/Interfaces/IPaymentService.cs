using Core.DTOs;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPaymentService
    {
        public Task<PaymentToReturnDTO> GetPaymentByReservationIDAsync(int reservationId);
        public Task<PaymentToReturnDTO> CreatePaymentAsync(CreatePaymentDTO createpayment);
        public Task<PaymentToReturnDTO> UpdatePaymentStatusAsync(int paymentId, string status);

    }
}
