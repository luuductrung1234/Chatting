using System;
using System.Collections.Generic;
using System.Text;

namespace Chatting.Infrastructure.Common
{
   public interface IDbConfigurationBuilder
   {
      string ConnectionString { get; }
      string DatabaseName { get; }
   }
}
