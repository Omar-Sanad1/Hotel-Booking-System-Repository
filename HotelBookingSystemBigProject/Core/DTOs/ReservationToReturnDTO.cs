
namespace Core.DTOs
{
    public class ReservationToReturnDTO
    {
        public int ID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime BookingDate { get; set; }
        public double TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
        public string ReservationStatus { get; set; }
        public string RoomNumber { get; set; } // ==> FK
        public string CustomerName { get; set; } // ==> FK
    }
}
