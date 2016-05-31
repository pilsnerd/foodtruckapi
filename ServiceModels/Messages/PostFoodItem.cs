using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModels.Messages
{
    [Route("/trucks/{TruckId}/fooditems", "POST")]
    public class PostFoodItem
    {
        public int TruckId { get; set; }
        public DateTime Date { get; set; }
        public string FoodName { get; set; }
        public string PersonName { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
