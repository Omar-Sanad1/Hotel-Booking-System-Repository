using Core.DTOs;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IHotelService
    {
        public Task<IEnumerable<HotelToReturnDTO>> GetAllHotelsAsync();
        public Task<HotelToReturnDTO> GetHotelByIDAsync(int hotelid);
        // لاضافة فندق جديد وكمان هو طالبها في المشروع "The system manages multiple hotels"
        public Task<HotelToReturnDTO> CreateHotelAsync(CreateHotelDTO createHotel);
        public Task<HotelToReturnDTO> UpdateHotelInformationByIDAsync(int hotelid,CreateHotelDTO createHotel);
        public Task DeleteHotelAsync(int hotelid);

    }
}
