using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Common.Commands;
using Micro.Common.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Micro.Services.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args,"http://*:5051")
           .UseRabbitMq()
           .SubscribeToCommand<CreateUser>()
           .Build()
           .Run();

        }
    }
}