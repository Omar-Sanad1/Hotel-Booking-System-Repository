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
    public class ReviewService : IReviewService
    {
        private readonly HotelBookingDbContext _dbContext;
        private readonly IMapper _mapper;
        public ReviewService(HotelBookingDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ReviewToReturnDTO> GetReviewByIdAsync(int reviewid)
        {
            var specifiedReview = await _dbContext.Reviews.
                                  Include(r => r.Customer).
                                  Include(r => r.Room).
                                  FirstOrDefaultAsync(r => r.ID == reviewid);

            if (specifiedReview is null)
                throw new Exception("This no review id like that");

            return _mapper.Map<Review, ReviewToReturnDTO>(specifiedReview);
        }
        public async Task<IEnumerable<ReviewToReturnDTO>> GetReviewsByCustomerIdAsync(int customerId)
        {
            var customerReviews = await _dbContext.Reviews.
                                  Include(r => r.Customer).
                                  Include(r => r.Room).
                                  Where(r => r.CustomerID == customerId).
                                  ToListAsync();

            return _mapper.Map<IEnumerable<Review>,IEnumerable<ReviewToReturnDTO>>(customerReviews);
        }
        public async Task<IEnumerable<ReviewToReturnDTO>> GetReviewsByRoomIdAsync(int roomid)
        {
            var roomReviews = await _dbContext.Reviews.
                              Include(r => r.Customer).
                              Include(r => r.Room).
                              Where(r => r.RoomID == roomid).ToListAsync();
            return _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewToReturnDTO>>(roomReviews);
        }
        public async Task<ReviewToReturnDTO> CreateReviewAsync(CreateReviewDTO createReview)
        {
            var specifiedCustomer = await _dbContext.Customers.FindAsync(createReview.CustomerID);
            if (specifiedCustomer is null)
                throw new Exception("This no customer id like that");

            var specifiedRoom = await _dbContext.Rooms.FindAsync(createReview.RoomID);
            if (specifiedRoom is null)
                throw new Exception("This no room id like that");

            // اتءكد ان الCustomer حجز الغرفة دي قبل كدا
            var hasReservation = await _dbContext.Reservations.AnyAsync(r =>
                                 r.CustomerID == createReview.CustomerID &&
                                 r.RoomID == createReview.RoomID &&
                                 r.ReservationStatus == "Confirmed"
                                                                 );

            if (!hasReservation)
                throw new Exception("Customer must have a confirmed reservation for this room to review it");

            if (createReview.RatingValue < 1 || createReview.RatingValue > 5)
                throw new Exception("The rating value must be between 1 and 5");

            var review = new Review
            {
                CustomerID = createReview.CustomerID,
                RoomID = createReview.RoomID,
                RatingValue = createReview.RatingValue,
                Comment = createReview.Comment,
                ReviewDate = DateTime.Now
            };

            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();

            return await GetReviewByIdAsync(review.ID);
        }

       
    }
}
