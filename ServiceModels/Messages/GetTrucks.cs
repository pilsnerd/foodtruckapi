using ServiceModels.Models;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModels.Messages
{
    [Route("/trucks", "GET")]
    public class GetTrucks : IReturn<List<Truck>>
    {
    }
}
