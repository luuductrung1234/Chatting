using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.Extensions.Configuration;

// SalesHub Common
using SalesHub.Common.BaseService;
using SalesHub.Common.BaseServiceProvider.Helpers;

namespace Chatting.API
{
   /// <summary>
   /// The FabricRuntime creates an instance of this class for each service type instance. 
   /// </summary>
   internal sealed class Manager : BaseStatelessService
   {
      public Manager(StatelessServiceContext context)
          : base(context)
      { }

      /// <summary>
      /// Optional override to create listeners (like tcp, http) for this service instance.
      /// </summary>
      /// <returns>The collection of listeners.</returns>
      protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
      {
         return new ServiceInstanceListener[]
         {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        return new WebHostBuilder()
                                    .UseKestrel()
                                    .ConfigureServices(
                                        (services) => DoConfigureServices(services, serviceContext))
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .ConfigureAppConfiguration((context, builder) => AddAppConfiguration(builder, this.BaseConfiguration))
                                    .UseStartup<Startup>()
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url)
                                    .Build();
                    }))
         };
      }

      private void DoConfigureServices(IServiceCollection services, StatelessServiceContext serviceContext)
      {
         //Add BaseServiceCollection into web app service collection
         services.AddMany(BaseServiceCollection);

         services.AddSingleton<StatelessServiceContext>(serviceContext);
      }

      private void AddAppConfiguration(IConfigurationBuilder configurationBuilder, IConfiguration baseConfiguration)
      {
         // Add base configuration settings.
         configurationBuilder.AddConfiguration(baseConfiguration);
      }
   }
}
