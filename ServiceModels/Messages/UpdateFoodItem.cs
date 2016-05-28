using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceModels.Messages
{
    [Route("/fooditems/{FoodItemId}","POST")]
    public class UpdateFoodItem
    {
        public int FoodItemId { get; set; }
        public int TruckId { get; set; }
        public DateTime Date { get; set; }
        public string FoodName { get; set; }
        public string PersonName { get; set; }
        public decimal Price { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
