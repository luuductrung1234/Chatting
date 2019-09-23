using Chatting.Domain;
using Chatting.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatting.Infrastructure.Repositories
{
   public class RoomRepository : IRoomRepository
   {
      public void CreateRoom(Room room)
      {
         throw new NotImplementedException();
      }

      public Room GetRoom(Guid id)
      {
         throw new NotImplementedException();
      }

      public Room GetRoom(string roomCode)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<Room> GetRooms()
      {
         throw new NotImplementedException();
      }

      public IEnumerable<Room> GetRoomsByUser(string userCode)
      {
         throw new NotImplementedException();
      }
   }
}
