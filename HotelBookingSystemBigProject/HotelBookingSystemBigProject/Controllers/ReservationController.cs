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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("getbyid{id}")]
        public async Task<IActionResult> GetReservationByIDAsync(int id)
        {
            var reservation = await _reservationService.GetReservationByIDAsync(id);
            return Ok(reservation);
        }

        [HttpGet("customerreservations")]
        public async Task<IActionResult> GetCustomerReservationsAsync(int customerid)
        {
            var customerReservations = await _reservationService.GetCustomerReservationsAsync(customerid);
            return Ok(customerReservations);
        }

        [HttpPost("createreservation")]
        public async Task<IActionResult> CreateReservationAsync([FromBody]CreateReservationDTO createReservation)
        {
            var createdReservation = await _reservationService.CreateReservationAsync(createReservation);
            return Ok(createdReservation);
        }

        [HttpPost("cancellreservation")]
        public async Task<IActionResult> CancellReservationAsync(int reservationid)
        {
            var cancelledReservation = await _reservationService.CancelReservationAsync(reservationid);
            return Ok(cancelledReservation);
        }


        //[HttpPost("add")]
        //public void Add(Reservation reservation)
        //{
        //    _repo.Add(reservation);
        //}

        //[HttpPut("update")]
        //public void Update(Reservation reservation)
        //{
        //    _repo.Update(reservation);
        //}

        //[HttpDelete("delete")]
        //public void Delete(Reservation reservation)
        //{
        //    _repo.Delete(reservation);
        //}
    }
}
