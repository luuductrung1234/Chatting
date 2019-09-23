using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatting.Infrastructure.Common
{
   public class GenericDbConfigurationBuilder : IDbConfigurationBuilder
   {
      private string _connectionString;
      private string _databaseName;

      public string ConnectionString => _connectionString;

      public string DatabaseName => _databaseName;

      public GenericDbConfigurationBuilder(IOptions<DatabaseSetting> options)
      {
         _connectionString = options.Value.ConnectionString;
         _databaseName = options.Value.DatabaseName;
      }
   }
}
