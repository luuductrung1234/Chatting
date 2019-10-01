using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

// Common
using SalesHub.Common.WebAPI;
using LDTSolutions.Common.WebApi.Cors;

namespace Chatting.API.Compositions
{
   public static class WebAPICompositions
   {
      public static IServiceCollection AddWebAPI(this IServiceCollection services, IHostingEnvironment environment)
      {
         services
            .AddCustomCors()
            .AddCustomSignalR()
            .AddHttpContextAccessor()
            .AddMvc()
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

         //if(environment.IsLocal())
         //{
         //   // attach RequireHttps attribute (for all request) to Mvc Layer
         //   services.Configure<MvcOptions>(options =>
         //   {
         //      options.Filters.Add(new RequireHttpsAttribute());
         //   });
         //}

         return services;
      }

      public static IServiceCollection AddCustomSignalR(this IServiceCollection services)
      {
         services.AddSignalR(c => c.EnableDetailedErrors = true);
         //.AddJsonProtocol(options =>
         //{
         //   // customize how Json format data
         //   options.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();
         //})
         //.AddMessagePackProtocol(options =>
         //{
         //   // customize how MessagePack format data
         //   options.FormatterResolvers = new List<MessagePack.IFormatterResolver>()
         //   {
         //      MessagePack.Resolvers.StandardResolver.Instance
         //   };
         //});

         return services;
      }
   }
}
