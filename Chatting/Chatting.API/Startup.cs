using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// Chatting API
using Chatting.API.Compositions;
using Chatting.API.Hubs;

// Chatting Infrastructure
using Chatting.Infrastructure.Compositions;

// Chatting Application
using Chatting.Application.Compositions;

namespace Chatting.API
{
   public class Startup
   {
      private static readonly string _corsPolicy = "MyCustomCorsPolicy";


      public Startup(IConfiguration configuration, IHostingEnvironment environment)
      {
         Configuration = configuration;
         Environment = environment;
      }

      public IHostingEnvironment Environment { get; }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services
            .AddCustomConfiguration(Configuration)
            .AddInfrastructure(Environment)
            .AddMediator()
            .AddWebAPI(Environment, _corsPolicy);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseCors(_corsPolicy);

         app.UseHttpsRedirection();

         app.UseSignalR((configure) =>
         {
            // signalR server side desired specific transports
            var desiredTransports =
               HttpTransportType.WebSockets |
               HttpTransportType.ServerSentEvents;

            configure.MapHub<ChattingHub>("/chattinghub", (options) =>
            {
               options.Transports = desiredTransports;

               // the maximum number of bytes from the client that the server buffers
               options.TransportMaxBufferSize = 32;

               // the maximum number of bytes the server can send
               options.ApplicationMaxBufferSize = 32;
            });
         });

         app.UseMvc();
      }
   }
}
