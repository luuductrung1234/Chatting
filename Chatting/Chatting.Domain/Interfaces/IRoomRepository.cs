﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatting.Domain.Interfaces
{
   public interface IRoomRepository
   {
      Task AddRoomAsync(Room room);

      Task<IEnumerable<Room>> GetRoomsAsync();

      Task<IEnumerable<Room>> GetRoomsByUserAsync(string userCode);

      Task<Room> GetRoomAsync(Guid id);

      Task<Room> GetRoomAsync(string roomCode);
   }
}
