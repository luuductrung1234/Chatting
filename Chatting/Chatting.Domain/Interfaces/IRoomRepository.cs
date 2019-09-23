using System;
using System.Collections.Generic;

namespace Chatting.Domain.Interfaces
{
   public interface IRoomRepository
   {
      void CreateRoom(Room room);

      IEnumerable<Room> GetRooms();

      IEnumerable<Room> GetRoomsByUser(string userCode);

      Room GetRoom(Guid id);

      Room GetRoom(string roomCode);
   }
}
