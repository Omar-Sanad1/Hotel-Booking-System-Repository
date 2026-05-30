using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Payment : BaseEntity
    {
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public double PaymentAmount { get; set; }
        public int ReservationID { get; set; } // ==> FK
        public Reservation Reservation { get; set; } // ==> Navigation Property
    }
}
