using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceModels.Messages
{
    [Route("/trucks/{TruckId}", "DELETE")]
    public class DeleteTruck
    {
        public int TruckId { get; set; }
    }
}
