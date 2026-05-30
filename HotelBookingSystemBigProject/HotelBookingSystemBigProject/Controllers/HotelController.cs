using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;

namespace HotelBookingSystemBigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("getallhotels")]
        public async Task<IActionResult> GetAllHotelsAsync()
        {
            var hotels = await _hotelService.GetAllHotelsAsync();
            return Ok(hotels);
        }

        [HttpGet("gethotelbyid{id}")]
        public async Task<IActionResult> GetHotelByIDAsync(int hotelid)
        {
            var hotel = await _hotelService.GetHotelByIDAsync(hotelid);
            return Ok(hotel);
        }

        [HttpPost("createhotel")]
        public async Task<IActionResult> CreateHotelAsync(CreateHotelDTO createHotel)
        {
            var createdHotel = await _hotelService.CreateHotelAsync(createHotel);
            return Ok(createdHotel);
        }

        [HttpPut("updatehotelinformation")]
        public async Task<IActionResult> UpdateHotelInformationByIDAsync(int hotelid, CreateHotelDTO createHotel)
        {
            var updatedHotelInfo = await _hotelService.UpdateHotelInformationByIDAsync(hotelid, createHotel);
            return Ok(updatedHotelInfo);
        }

        [HttpDelete("deletehotelbyid{id}")]
        public async Task DeleteHotelAsync(int hotelid)
        {
            await _hotelService.DeleteHotelAsync(hotelid);
        }

    }
}
