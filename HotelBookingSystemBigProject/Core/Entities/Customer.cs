using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string AccountStatus { get; set; }
        public List<Review> Reviews { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();

    }
}
