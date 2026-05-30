using Core.DTOs;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IReviewService
    {
        public Task<ReviewToReturnDTO> GetReviewByIdAsync(int reviewid);
        public Task<IEnumerable<ReviewToReturnDTO>> GetReviewsByRoomIdAsync(int roomid);
        public Task<IEnumerable<ReviewToReturnDTO>> GetReviewsByCustomerIdAsync(int customerId);
        public Task<ReviewToReturnDTO> CreateReviewAsync(CreateReviewDTO createReview);
    }
}
