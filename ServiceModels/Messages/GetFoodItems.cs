using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModels.Messages
{
    [Route("/trucks/{TruckId}/fooditems", "GET")]
    public class GetFoodItems
    {
        public int TruckId { get; set; }
    }
}
