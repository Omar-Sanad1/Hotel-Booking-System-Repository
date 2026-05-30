using AutoMapper;
using Core.Entities;
using Core.DTOs;

namespace HotelBookingSystemBigProject.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerToReturnDTO>();
            /////////////////////////////////////////////////
            
            CreateMap<Hotel, HotelToReturnDTO>();
            /////////////////////////////////////////////////
            
            CreateMap<Payment, PaymentToReturnDTO>();
            /////////////////////////////////////////////////
            
            CreateMap<Reservation, ReservationToReturnDTO>()
                .ForMember(r => r.CustomerName, r => r.MapFrom(c => c.Customer.FullName))
                .ForMember(r => r.RoomNumber, r => r.MapFrom(r => r.Room.Number));
            /////////////////////////////////////////////////
            
            CreateMap<Review, ReviewToReturnDTO>();
            /////////////////////////////////////////////////
            
            CreateMap<Room, RoomToReturnDTO>()
                .ForMember(r => r.HotelName, r => r.MapFrom(h => h.Hotel.Name));
        }
    }
}
