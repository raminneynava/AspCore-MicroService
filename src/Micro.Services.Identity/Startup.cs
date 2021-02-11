using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Common.Auth;
using Micro.Common.Commands;
using Micro.Common.Mongo;
using Micro.Common.RabbitMq;
using Micro.Services.Identity.Domain.Repositories;
using Micro.Services.Identity.Domain.Services;
using Micro.Services.Identity.Handlers;
using Micro.Services.Identity.Repositories;
using Micro.Services.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Micro.Services.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMongoDB(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddJwt(Configuration);
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddScoped<IEncrypter, Encrypter>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
         //   app.UseEndpoints(endpoints =>
         //   {
         //       endpoints.MapControllers();
         //   });
         //using(var serviceScope = app.ApplicationServices.CreateScope())
         //   {
         //       var service = serviceScope.ServiceProvider.GetService<IDatabaseInitializer>().InitializerAsync();

         //   }

            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializerAsync();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
