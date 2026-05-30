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
    public class HotelService : IHotelService
    {
        private readonly HotelBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        public HotelService(HotelBookingDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<HotelToReturnDTO>> GetAllHotelsAsync()
        {
            var allHotels = await _dbContext.Hotels.ToListAsync();
            return _mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelToReturnDTO>>(allHotels);
        }

        public async Task<HotelToReturnDTO> GetHotelByIDAsync(int hotelid)
        {
            var specifiedHotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.ID == hotelid);

            if (specifiedHotel is null)
                throw new Exception("This is no hotel with this id");

            return _mapper.Map<Hotel,HotelToReturnDTO>(specifiedHotel);
        }
        public async Task<HotelToReturnDTO> CreateHotelAsync(CreateHotelDTO createHotel)
        {
            var isExsisting = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Name == createHotel.HotelName);

            if (isExsisting is not null)
                throw new Exception("This is hotel with this name is already exists");

            var hotel = new Hotel
            {
                ID  = createHotel.ID,
                Name = createHotel.HotelName,
                Description = createHotel.Description,
                Location = createHotel.Location,
                ContactInformation = createHotel.ContactInformation,
                Rating = 0
            };

            _dbContext.Hotels.Add(hotel);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Hotel, HotelToReturnDTO>(hotel);
        }

        public async Task<HotelToReturnDTO> UpdateHotelInformationByIDAsync(int hotelid,CreateHotelDTO createHotel)
        {
            var specifiedHotel = _dbContext.Hotels.FirstOrDefault(h=>h.ID ==  hotelid);
            if (specifiedHotel is null)
                throw new Exception("This is no hotel with this id");

            specifiedHotel.Name = createHotel.HotelName;
            specifiedHotel.Location = createHotel.Location;
            specifiedHotel.ContactInformation = createHotel.ContactInformation;
            specifiedHotel.Description = createHotel.Description;

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Hotel, HotelToReturnDTO>(specifiedHotel);
        }
        

        public async Task DeleteHotelAsync(int hotelid)
        {
            var specifiedHotel = await _dbContext.Hotels.FindAsync(hotelid);

            if (specifiedHotel is null)
                throw new Exception("This is no hotel with this id");

            _dbContext.Hotels.Remove(specifiedHotel);
            await _dbContext.SaveChangesAsync();

        }

    }
}
