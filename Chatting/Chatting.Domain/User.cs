using System;

// SalesHub Common
using SalesHub.Common.DDDCore;

namespace Chatting.Domain
{
   public class User : Entity, IAggregateRoot
   {
      public string UserCode { get; private set; }

      public string UserName { get; private set; }

      public string Password { get; private set; }

      public DateTime? Birth { get; private set; }

      public User(string userCode,
         string userName,
         string password,
         DateTime? birth)
      {
         UserCode = userCode;
         UserName = userName;
         Password = password;
         Birth = birth;
      }
   }
}
