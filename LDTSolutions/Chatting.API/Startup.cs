using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;

// Common
using SalesHub.Common.WebAPI;
using LDTSolutions.Common.WebApi.Cors;

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
      public Startup(IConfiguration configuration, IHostingEnvironment environment)
      {
         Configuration = configuration;
         Environment = environment;
      }

      public IHostingEnvironment Environment { get; }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public IServiceProvider ConfigureServices(IServiceCollection services)
      {
         services
            .AddCustomConfiguration(Configuration)
            .AddInfrastructure(Environment)
            .AddMediator()
            .AddWebAPI(Environment);

         var container = new ContainerBuilder();
         container.Populate(services);

         return new AutofacServiceProvider(container.Build());
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsLocal())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseCustomCors();

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
