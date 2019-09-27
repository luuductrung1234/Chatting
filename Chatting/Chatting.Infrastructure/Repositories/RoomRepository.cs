using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;

// Common
using SalesHub.Common.GenericMongoDbRepository;
using SalesHub.Common.Core.LINQ;

// Chatting Domain
using Chatting.Domain;
using Chatting.Domain.Interfaces;

// Chatting Infrastructure
using Chatting.Infrastructure.Common;

namespace Chatting.Infrastructure.Repositories
{
   public class RoomRepository : BaseRepository<Room>, IRoomRepository
   {
      public RoomRepository(IGenericDbContext dbContext)
         : base(dbContext, partitionKey: "roomCode")
      {

      }

      public async Task AddRoomAsync(Room room)
      {
         await this.AddOneAsync(room);
      }

      public async Task<bool> CheckRoomExistedAsync(string roomCode, IList<string> userCodes)
      {
         if(userCodes.Count == 0)
         {
            return false;
         }

         var predicate = PredicateBuilder.Create<Room>(room => room.RoomCode == roomCode);

         predicate = predicate.And(room => room.UserCodes
               .Contains(userCodes[0]));
         for (int i = 1; i < userCodes.Count(); i++)
         {
            var index = i;
            predicate = predicate.Or(room => room.UserCodes.Contains(userCodes[index]));
         }

         return await this.AnyAsync(predicate);
      }

      public Task<Room> GetRoomAsync(Guid id)
      {
         throw new NotImplementedException();
      }

      public Task<Room> GetRoomAsync(string roomCode)
      {
         throw new NotImplementedException();
      }

      public Task<IEnumerable<Room>> GetRoomsAsync()
      {
         throw new NotImplementedException();
      }

      public Task<IEnumerable<Room>> GetRoomsByUserAsync(string userCode)
      {
         throw new NotImplementedException();
      }
   }
}
