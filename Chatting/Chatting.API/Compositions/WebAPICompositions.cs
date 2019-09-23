using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Mvc;

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
                  builder.AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials()
                     .WithOrigins("https://localhost:44376",
                        "http://localhost:59553");
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
            .AddSignalR(c => c.EnableDetailedErrors = true);

         return services;
      }
   }
}
