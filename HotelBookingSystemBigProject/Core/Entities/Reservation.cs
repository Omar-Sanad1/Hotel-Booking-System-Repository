using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime BookingDate { get; set; }
        public double TotalPrice { get; set; }
        public string PaymentStatus { get; set; }
        public string ReservationStatus { get; set; }
        public int RoomID { get; set; } // ==> FK
        public Room Room { get; set; } // ==> Navigation Property
        public int CustomerID { get; set; } // ==> FK
        public Customer Customer { get; set; } // ==> Navigation Property
        public Payment Payment { get; set; } // ==> Navigation Property
    }
}
