using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;

namespace HotelBookingSystemBigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IGenericRepository<Payment> _repo;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        public PaymentController(IGenericRepository<Payment> repo, IMapper mapper, IPaymentService paymentService)
        {
            _repo = repo;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var payments = _repo.GetAll();
            var paymentsMapping = _mapper.Map<IEnumerable<Payment>, IEnumerable<PaymentToReturnDTO>>(payments);
            return Ok(paymentsMapping);
        }

        [HttpGet("getpaymentbyreservationid")]
        public async Task<IActionResult> GetPaymentByReservationID(int reservationid)
        {
            var payment = await _paymentService.GetPaymentByReservationIDAsync(reservationid);
            return Ok(payment);
        }

        [HttpPost("addpayment")]
        public async Task<IActionResult> AddPayment([FromBody]CreatePaymentDTO createPayment)
        {
            var createdPayment = await _paymentService.CreatePaymentAsync(createPayment);
            return Ok(createdPayment);
        }

        [HttpPut("updatestatus")]
        public async Task<IActionResult> UpdateStatus(int paymentid , string status)
        {
            var updatedStatus = await _paymentService.UpdatePaymentStatusAsync(paymentid, status);
            return Ok(updatedStatus);
        }

        [HttpDelete("delete")]
        public void Delete(Payment payment)
        {
            _repo.Delete(payment);
        }
    }
}
