using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

// Compositions
using Identity.DummyAPI.Compositions;

// Infrastructure
using Identity.Infrastructure.Data;

namespace Identity.DummyAPI
{
   public class Startup
   {
      public IConfiguration Configuration { get; }

      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      // This method gets called by the runtime. Use this method to add services to the container.
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
      public void ConfigureServices(IServiceCollection services)
      {
         services
            .AddCustomIdentity(configuration: Configuration)
            .AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }

         app.Run(async (context) =>
         {
            await context.Response.WriteAsync($"Dummy API as a startup-project for {typeof(AppIdentityDbContext).Name} migrations!");
         });

         app.UseStaticFiles();

         app.UseAuthentication();

         app.UseMvc();
      }
   }
}
