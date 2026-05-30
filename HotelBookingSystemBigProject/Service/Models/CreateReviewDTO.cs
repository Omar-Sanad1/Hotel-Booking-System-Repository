using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class CreateReviewDTO
    {
        public int ID { get; set; }
        public int RoomID { get; set; }
        public int CustomerID { get; set; }
        public string Comment { get; set; }
        public int RatingValue { get; set; }
    }
}
