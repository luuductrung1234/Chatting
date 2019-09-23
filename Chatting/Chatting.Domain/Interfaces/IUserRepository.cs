using System;
using System.Collections.Generic;

namespace Chatting.Domain.Interfaces
{
   public interface IUserRepository
   {
      void CreateUser(User user);

      IEnumerable<User> GetUsers();

      User GetUser(Guid id);

      User GetUser(string userCode);
   }
}
