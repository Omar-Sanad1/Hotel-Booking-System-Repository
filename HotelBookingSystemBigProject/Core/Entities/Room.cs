using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Room : BaseEntity
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public int FloorNumber { get; set; }
        public string AvailabilityStatus { get; set; }
        public string ImageUrl { get; set; }
        public int HotelID { get; set; } // ==> FK
        public Hotel Hotel { get; set; } // ==> Navigation Property
        public List<Review> Reviews { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();

    }
}
