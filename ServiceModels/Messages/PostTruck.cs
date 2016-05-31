using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModels.Messages
{
    [Route("/trucks", "POST")]
    public class PostTruck
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
