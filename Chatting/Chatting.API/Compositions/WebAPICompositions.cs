using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Chatting.API.Compositions
{
   public static class WebAPICompositions
   {
      public static IServiceCollection AddWebAPI(this IServiceCollection services, string corsPolicy)
      {
         services
            .AddCors(options =>
            {
               options.AddPolicy(corsPolicy, builder =>
               {
                  builder
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials()
                     .AllowAnyOrigin()
                     .WithOrigins("http://localhost:8080");
               });
            })
            .AddMvc()
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

         services.AddSignalRConfigration();

         return services;
      }

      public static IServiceCollection AddSignalRConfigration(this IServiceCollection services)
      {
         services.AddHttpContextAccessor()
            .AddSignalR(c => c.EnableDetailedErrors = true)
            .AddJsonProtocol(options =>
            {
               // customize how Json format data
               options.PayloadSerializerSettings.ContractResolver = new DefaultContractResolver();
            })
            .AddMessagePackProtocol(options =>
            {
               // customize how MessagePack format data
               options.FormatterResolvers = new List<MessagePack.IFormatterResolver>()
               {
                  MessagePack.Resolvers.StandardResolver.Instance
               };
            });

         return services;
      }
   }
}
