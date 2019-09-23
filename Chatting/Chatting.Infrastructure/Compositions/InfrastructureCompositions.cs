using System;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Authentication;
using Microsoft.AspNetCore.Hosting;


// Chatting Domain
using Chatting.Domain.Interfaces;

// Chatting Infrastructure
using Chatting.Infrastructure.DatabaseMapping;
using Chatting.Infrastructure.Repositories;
using Chatting.Infrastructure.Common;

namespace Chatting.Infrastructure.Compositions
{
   public static class InfrastructureCompositions
   {
      public static IServiceCollection AddInfrastructure(this IServiceCollection services, IHostingEnvironment env)
      {
         MappingConfigurations.Map();

         services
            .AddSingleton<GenericDbConfigurationBuilder>()
            .AddSingleton<ReadOnlyDbConfigurationBuilder>()
            .AddSingleton<MasterDataDbConfigurationBuilder>()
            .AddDbContext(env)
            .AddRepositories();

         return services;
      }

      public static IServiceCollection AddDbContext(this IServiceCollection services, IHostingEnvironment env)
      {
         services.AddSingleton<IGenericDbContext>(sp =>
         {
            var dbConfiguration = sp.GetRequiredService<GenericDbConfigurationBuilder>();

            MongoClientSettings settings = MongoClientSettings.FromUrl(
               new MongoUrl(dbConfiguration.ConnectionString)
            );
            settings.SslSettings = new SslSettings()
            {
               EnabledSslProtocols = SslProtocols.Tls12
            };

            settings.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;

            if (env.EnvironmentName.ToLower() == "local")
            {
               settings.GuidRepresentation = MongoDB.Bson.GuidRepresentation.CSharpLegacy;
            }

            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(dbConfiguration.DatabaseName);
            return new GenericDbContext(mongoDatabase);
         })
         .AddSingleton<IReadOnlyDbContext>(sp =>
         {
            var dbConfiguration = sp.GetRequiredService<ReadOnlyDbConfigurationBuilder>();

            MongoClientSettings settings = MongoClientSettings.FromUrl(
               new MongoUrl(dbConfiguration.ConnectionString)
            );
            settings.SslSettings = new SslSettings()
            {
               EnabledSslProtocols = SslProtocols.Tls12
            };

            settings.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;

            if (env.EnvironmentName.ToLower() == "local")
            {
               settings.GuidRepresentation = MongoDB.Bson.GuidRepresentation.CSharpLegacy;
            }

            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(dbConfiguration.DatabaseName);
            return new ReadOnlyDbContext(mongoDatabase);
         });


         return services;
      }

      public static IServiceCollection AddRepositories(this IServiceCollection services)
      {
         services.AddScoped<IUserRepository, UserRepository>();
         services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
         services.AddScoped<IRoomRepository, RoomRepository>();

         return services;
      }
   }
}
