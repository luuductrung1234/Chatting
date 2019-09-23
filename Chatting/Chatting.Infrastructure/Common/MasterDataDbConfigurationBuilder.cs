using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatting.Infrastructure.Common
{
   public class MasterDataDbConfigurationBuilder : IDbConfigurationBuilder
   {
      private string _connectionString;
      private string _databaseName;

      public string ConnectionString => _connectionString;

      public string DatabaseName => _databaseName;

      public MasterDataDbConfigurationBuilder(IOptions<MasterDatabaseSetting> options)
      {
         _connectionString = options.Value.ConnectionString;
         _databaseName = string.Empty;
      }
   }
}
