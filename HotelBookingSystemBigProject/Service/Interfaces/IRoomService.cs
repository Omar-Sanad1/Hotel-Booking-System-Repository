using Core.Entities;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;

namespace Service.Interfaces
{
    public interface IRoomService
    {
        // 1. Get All Rooms
        // 2. Get Room By ID
        // 3. Update Room Status
        // 4. Search For Room

        public Task<IEnumerable<RoomToReturnDTO>> GetAllRoomsAsync();
        public Task<RoomToReturnDTO> GetRoomByIDAsync(int id);
        public Task<RoomToReturnDTO> UpdateRoomStatusAsync(int id, string status);

        // ال Customer هيستخدمها لو عايز يبحث عن غرفة بمعايير معينة زي السعر / السعة / النوع / الفندق / ........
        public Task<IEnumerable<RoomToReturnDTO>> SearchRoomsAsync(RoomFilterDTO filter);
    }
}
