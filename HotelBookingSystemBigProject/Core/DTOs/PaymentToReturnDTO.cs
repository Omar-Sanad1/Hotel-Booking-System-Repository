namespace Core.DTOs
{
    public class PaymentToReturnDTO
    {
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public double PaymentAmount { get; set; }
        public int ReservationID { get; set; } // ==> FK
    }
}
