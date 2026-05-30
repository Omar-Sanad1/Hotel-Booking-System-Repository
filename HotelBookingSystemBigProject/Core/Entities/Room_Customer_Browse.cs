using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Room_Customer_Browse
    {
        public int RoomID { get; set; } // ==> FK
        public Room Room { get; set; } // ==> Navigation Property
        public int CustomerID { get; set; } // ==> FK
        public Customer Customer { get; set; } // ==> Navigation Property
    }
}
