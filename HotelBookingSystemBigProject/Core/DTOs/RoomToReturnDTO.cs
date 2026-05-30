
namespace Core.DTOs
{
    public class RoomToReturnDTO
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public double PricePerNight { get; set; }
        public int FloorNumber { get; set; }
        public string AvailabilityStatus { get; set; }
        public string ImageUrl { get; set; }
        public string HotelName { get; set; } // ==> FK
    }
}
