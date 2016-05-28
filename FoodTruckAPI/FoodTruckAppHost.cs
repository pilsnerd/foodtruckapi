using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Configuration;
using ServiceStack.Host;
using ServiceStack.Host.Handlers;
using ServiceStack.Html;
using ServiceStack.IO;
using ServiceStack.Web;
using System.Net;
using Funq;
using ServiceDefinitions;
using ServiceStack.Validation;

namespace FoodTruckAPI
{
    public class FoodTruckAppHost : AppHostBase
    {
        public FoodTruckAppHost() : base("FoodTruckAPI", typeof(ServiceDefinitionInfo).Assembly) { }

        public override void Configure(Container container)
        {
            InitializePlugins();
        }

        private void InitializePlugins()
        {
            base.Plugins.Add(new ValidationFeature());
        }
    }
}