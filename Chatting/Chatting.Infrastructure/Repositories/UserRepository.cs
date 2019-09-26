using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Common
using SalesHub.Common.GenericMongoDbRepository;

// Chatting Domain
using Chatting.Domain;
using Chatting.Domain.Interfaces;

// Chatting Infrastructure
using Chatting.Infrastructure.Common;

namespace Chatting.Infrastructure.Repositories
{
   public class UserRepository : BaseRepository<User>, IUserRepository
   {
      public UserRepository(IGenericDbContext dbContext)
         : base(dbContext, partitionKey: "userCode")
      {

      }
      public Task AddUserAsync(User user)
      {
         throw new NotImplementedException();
      }

      public Task<User> GetUserAsync(Guid id)
      {
         throw new NotImplementedException();
      }

      public Task<User> GetUserAsync(string userCode)
      {
         throw new NotImplementedException();
      }

      public Task<IEnumerable<User>> GetUsersAsync()
      {
         throw new NotImplementedException();
      }
   }
}
