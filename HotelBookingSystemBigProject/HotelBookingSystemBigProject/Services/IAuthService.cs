using Core.DTOs;
using HotelBookingSystemBigProject.Models;

namespace HotelBookingSystemBigProject.Services
{
    public interface IAuthService
    {
        public Task<CustomerToReturnDTO> RegisterAsync(RegisterModel model);
        //Login
        public Task<CustomerToReturnDTO> GetTokenAsync(TokenRequestModel model);
    }
}
