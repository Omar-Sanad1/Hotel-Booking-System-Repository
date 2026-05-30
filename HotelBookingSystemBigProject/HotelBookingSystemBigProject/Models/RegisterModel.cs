using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystemBigProject.Models
{
    public class RegisterModel
    {
       
        [Required, StringLength(150)]
        public string FullName { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string Password { get; set; }
        [Required, StringLength(150)]
        public string Address { get; set; }
        [Required, StringLength(50)]
        public string PhoneNumber { get; set; }
    }
}
