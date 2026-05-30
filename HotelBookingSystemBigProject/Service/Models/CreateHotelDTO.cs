using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class CreateHotelDTO
    {
        public int ID { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ContactInformation { get; set; }
    }
}
