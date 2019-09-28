using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chatting.API.Compositions
{
   public static class IdentityCompositions
   {
      public static IServiceCollection AddIdentitySupport(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<IdentityDbContext>(options =>
            options.UseSqlServer(
               configuration["IdentityDb:ConnectionString"]));

         services.AddIdentity<IdentityUser, IdentityRole>();

         return services;
      }
   }
}
