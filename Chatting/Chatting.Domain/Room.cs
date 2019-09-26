﻿using System;
using System.Collections.Generic;

// SalesHub Common
using SalesHub.Common.DDDCore;

namespace Chatting.Domain
{
   public class Room : Entity, IAggregateRoot
   {
      public string RoomCode { get; private set; }

      public string RoomName { get; private set; }

      public IEnumerable<string> UserCodes { get; private set; }

      public DateTime CreatedDate { get; private set; }

      public Room(string roomCode, string roomName, IEnumerable<string> userCodes, DateTime createdDate)
      {
         RoomCode = roomCode;
         RoomName = roomName;
         UserCodes = userCodes;
         CreatedDate = createdDate;
      }
   }
}
