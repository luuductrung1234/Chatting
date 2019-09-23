using Chatting.Domain;
using Chatting.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Chatting.Infrastructure.Repositories
{
   public class UserRepository : IUserRepository
   {
      public void CreateUser(User user)
      {
         throw new NotImplementedException();
      }

      public User GetUser(Guid id)
      {
         throw new NotImplementedException();
      }

      public User GetUser(string userCode)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<User> GetUsers()
      {
         throw new NotImplementedException();
      }
   }
}
