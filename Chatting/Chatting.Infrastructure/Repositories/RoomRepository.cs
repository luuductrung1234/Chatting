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
   public class RoomRepository : BaseRepository<Room>, IRoomRepository
   {
      public RoomRepository(IGenericDbContext dbContext)
         : base(dbContext, partitionKey: "roomCode")
      {

      }

      public Task AddRoomAsync(Room room)
      {
         throw new NotImplementedException();
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
