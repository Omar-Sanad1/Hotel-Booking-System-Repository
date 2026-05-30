using AutoMapper;
using Core.Entities;
using Core.DTOs;

namespace HotelBookingSystemBigProject.Helper.imagesURLsResolver
{
    public class RoomImageUrlResolver : IValueResolver<Room, RoomToReturnDTO, string>
    {
        private readonly IConfiguration _configuration;
        public RoomImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Room source, RoomToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImageUrl))
            {
                return $"{_configuration["ApiBaseUrl"]} - {source.ImageUrl}";
            }
            return string.Empty;
        }
    }
}
