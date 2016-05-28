using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceModels.Messages
{
    [Route("/fooditems/{FoodItemId}", "DELETE")]
    public class DeleteFoodItem
    {
        public int FoodItemId { get; set; }
    }
}
