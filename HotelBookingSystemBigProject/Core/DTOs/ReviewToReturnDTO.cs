
namespace Core.DTOs
{
    public class ReviewToReturnDTO
    {
        public int ID { get; set; }
        public double RatingValue { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public int RoomID { get; set; } // ==> FK
        public int CustomerID { get; set; } // ==> FK
    }
}
