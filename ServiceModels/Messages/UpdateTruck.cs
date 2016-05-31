using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceModels.Messages
{
    [Route("/trucks/{TruckId}", "POST")]
    public class UpdateTruck
    {
        public int TruckId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
