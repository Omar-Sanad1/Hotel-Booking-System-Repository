using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models;

namespace HotelBookingSystemBigProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IGenericRepository<Review> _repo;
        private readonly IMapper _mapper;
        private readonly IReviewService _reviewService;
        public ReviewController(IGenericRepository<Review> repo,IMapper mapper,IReviewService reviewService)
        {
            _repo = repo;
            _mapper = mapper;
            _reviewService = reviewService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var reviews = _repo.GetAll();
            var reviewsMapping = _mapper.Map<IEnumerable<Review>, IEnumerable<ReviewToReturnDTO>>(reviews);
            return Ok(reviewsMapping);
        }

        [HttpGet("getreviewbyid")]
        public async Task<IActionResult> GetReviewByIdAsync(int reviewid)
        {
            var specifiedReview = await _reviewService.GetReviewByIdAsync(reviewid);
            return Ok(specifiedReview);
        }

        [HttpGet("getreviewsbyroomid")]
        public async Task<IActionResult> GetReviewsByRoomID(int roomid)
        {
            var roomReviews = await _reviewService.GetReviewsByRoomIdAsync(roomid);
            return Ok(roomReviews);
        }

        [HttpGet("getreviewsbycustomerid")]
        public async Task<IActionResult> GetReviewsByCustomerID(int customerid)
        {
            var customerReviews = await _reviewService.GetReviewsByCustomerIdAsync(customerid);
            return Ok(customerReviews);
        }

        [HttpPost("createreview")]
        public async Task<IActionResult> CreateReviewAsync(CreateReviewDTO createReview)
        {
            var createdReview = await _reviewService.CreateReviewAsync(createReview);
            return Ok(createdReview);
        }

        [HttpPut("update")]
        public void Update(Review review)
        {
            _repo.Update(review);
        }

        [HttpDelete("delete")]
        public void Delete(Review review)
        {
            _repo.Delete(review);
        }
    }
}
