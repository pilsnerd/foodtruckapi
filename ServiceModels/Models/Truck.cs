using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModels.Models
{
    public class Truck
    {
        public int TruckId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal AverageRating { get; set; }
    }
}
