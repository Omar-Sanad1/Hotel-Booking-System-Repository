using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    // For (SearchRoomsAsync Method) ==> للسيرش
    public class RoomFilterDTO
    {
        public string? HotelName { get; set; }
        public string? RoomType { get; set; }
        public int? Capacity { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? AvailabilityStatus { get; set; }
    }
}
