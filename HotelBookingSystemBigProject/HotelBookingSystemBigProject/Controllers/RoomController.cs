using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Service.Models;

namespace HotelBookingSystemBigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IGenericRepository<Room> _repo;
        //private readonly IMapper _mapper;
        private readonly IRoomService _roomService;
        public RoomController(IGenericRepository<Room> repo,IRoomService roomService)
        {
            _repo = repo;
            //_mapper = mapper;
            _roomService = roomService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("getbyid{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            var room = await _roomService.GetRoomByIDAsync(id);
            return Ok(room);
        }

        [HttpPut("updateroomstatus")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoomStatusAsync(int id , string status)
        {
            var result = await _roomService.UpdateRoomStatusAsync(id, status);
            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchForRoomsAsync([FromBody]RoomFilterDTO filter)
        {
            var specifiedRoom = await _roomService.SearchRoomsAsync(filter);
            return Ok(specifiedRoom);
        }

        [HttpPost("add")]
        public void Add(Room room)
        {
            _repo.Add(room);
        }


        [HttpDelete("delete")]
        public void Delete(Room room)
        {
            _repo.Delete(room);
        }


    }
}
