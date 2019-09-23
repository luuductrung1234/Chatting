using System;
using System.Collections.Generic;
using System.Text;

namespace Chatting.Domain
{
   public class ChatMessage
   {
      public string ChatMessageCode { get; private set; }

      public string RoomCode { get; private set; }

      public string SenderCode { get; private set; }

      public string ReceiverCode { get; private set; }

      public DateTime CreatedDate { get; private set; }

      public string Message { get; private set; }

      public ChatMessage(string chatMessageCode,
         string roomCode,
         string senderCode,
         string receiverCode,
         DateTime createdDate,
         string message)
      {
         ChatMessageCode = chatMessageCode;
         RoomCode = roomCode;
         SenderCode = senderCode;
         ReceiverCode = receiverCode;
         CreatedDate = createdDate;
         Message = message;
      }
   }
}
