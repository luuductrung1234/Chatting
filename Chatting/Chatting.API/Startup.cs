using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// Chatting API
using Chatting.API.Compositions;

// Chatting Infrastructure
using Chatting.Infrastructure.Compositions;
using Chatting.API.Hubs;

namespace Chatting.API
{
   public class Startup
   {
      private static readonly string _corsPolicy = "MyCustomCorsPolicy";

      private readonly IHostingEnvironment _env;

      public Startup(IConfiguration configuration, IHostingEnvironment env)
      {
         Configuration = configuration;
         _env = env;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services
            .AddCustomConfiguration(Configuration)
            .AddInfrastructure(_env)
            .AddWebAPI(_corsPolicy);
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

         app.UseSignalR(routes => routes.MapHub<ChattingHub>("/chattinghub"));

         app.UseMvc();
      }
   }
}
