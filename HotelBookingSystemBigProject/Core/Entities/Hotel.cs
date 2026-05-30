using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Hotel : BaseEntity
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ContactInformation { get; set; }
        public List<Room> Rooms { get; set; } = new();
    }
}
