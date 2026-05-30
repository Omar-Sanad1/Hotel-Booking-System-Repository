using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Review : BaseEntity
    {
        public double RatingValue { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public int RoomID { get; set; } // ==> FK
        public Room Room { get; set; } // ==> Navigation Property
        public int CustomerID { get; set; } // ==> FK
        public Customer Customer { get; set; } // ==> Navigation Property
    }
}
