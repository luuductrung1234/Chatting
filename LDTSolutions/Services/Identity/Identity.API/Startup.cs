using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

// Identity API
using Identity.API.Compositions;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Identity.API.Services;
using Identity.Infrastructure.Data;

namespace Identity.API
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
            .AddCustomIdentity(Configuration)
            .AddWebAPI(Environment);

         services.AddTransient<ILoginService<ApplicationUser>, EFLoginService>();

         var container = new ContainerBuilder();
         container.Populate(services);

         return new AutofacServiceProvider(container.Build());
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

         app.UseStaticFiles();

         app.UseAuthentication();

         app.UseHttpsRedirection();
         app.UseMvc();
      }
   }
}
