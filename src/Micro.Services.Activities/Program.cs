using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Common.Commands;
using Micro.Common.Events;
using Micro.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Micro.Services.Activities
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args, "http://*:5052")
            .UseRabbitMq()
            .SubscribeToCommand<CreateActivity>()
            .Build()
            .Run();

        }

}}
