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
    public class RoomService : IRoomService
    {
        private readonly IGenericRepository<Room> _repo;
        private readonly IMapper _mapper;
        private readonly HotelBookingDbContext _dbContext;
        public RoomService(IGenericRepository<Room> repo, IMapper mapper,HotelBookingDbContext dbContext)
        {
            _repo = repo;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<RoomToReturnDTO>> GetAllRoomsAsync()
        {
            var rooms = await _dbContext.Rooms.Include(r => r.Hotel).ToListAsync();
            var mappingRooms = _mapper.Map<IEnumerable<Room> , IEnumerable<RoomToReturnDTO>>(rooms);
            return mappingRooms;
        }

        public async Task<RoomToReturnDTO> GetRoomByIDAsync(int id)
        {
            var room = await _dbContext.Rooms.Include(r => r.Hotel).FirstOrDefaultAsync(r=>r.ID == id);
            if (room is null)
            {
                throw new Exception("This room id isn't available");
            }

            var roomMapping = _mapper.Map<Room , RoomToReturnDTO>(room);
            return roomMapping;
        }
        public async Task<RoomToReturnDTO> UpdateRoomStatusAsync(int id, string status)
        {
            var room = _repo.GetByID(id);
            if (room is null)
                throw new Exception("This room id isn't available");

            var validStatuses = new[] { "Available", "UnAvailable", "Reserved", "Maintence" };
            if (!validStatuses.Contains(status))
                throw new Exception("This is not valid status");

            room.AvailabilityStatus = status;
            _repo.Update(room);

            return _mapper.Map<Room , RoomToReturnDTO>(room);
        }

        public async Task<IEnumerable<RoomToReturnDTO>> SearchRoomsAsync(RoomFilterDTO filter)
        {
            var specifiedRoom = _dbContext.Rooms.Include(r => r.Hotel).AsQueryable();

            if(!string.IsNullOrEmpty(filter.HotelName))
                specifiedRoom = specifiedRoom.Where(r=>r.Hotel.Name == filter.HotelName);

            if (!string.IsNullOrEmpty(filter.RoomType))
                specifiedRoom = specifiedRoom.Where(r => r.Type == filter.RoomType);

            if (!string.IsNullOrEmpty(filter.AvailabilityStatus))
                specifiedRoom = specifiedRoom.Where(r => r.AvailabilityStatus == filter.AvailabilityStatus);

            if(filter.Capacity is not null) 
                specifiedRoom = specifiedRoom.Where(r=>r.Capacity >= filter.Capacity.Value);

            if (filter.MinPrice is not null)
                specifiedRoom = specifiedRoom.Where(r => r.PricePerNight >= filter.MinPrice.Value);

            if (filter.MaxPrice is not null)
                specifiedRoom = specifiedRoom.Where(r => r.PricePerNight <= filter.MaxPrice.Value);

            return _mapper.Map<IEnumerable<Room> , IEnumerable<RoomToReturnDTO>>(specifiedRoom);
        }

    }
}
