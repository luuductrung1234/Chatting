using System;

// SalesHub Common
using SalesHub.Common.DDDCore;

namespace Chatting.Domain
{
   public class ChatMessage : Entity, IAggregateRoot
   {
      public string ChatMessageCode { get; private set; }

      public string RoomCode { get; private set; }

      public string SenderCode { get; private set; }

      public string ReceiverCode { get; private set; }

      public DateTime CreatedDate { get; private set; }

      public string Message { get; private set; }

      public ChatMessage(
         string roomCode,
         string senderCode,
         string receiverCode,
         string message)
      {
         ChatMessageCode = GenerateCode();
         RoomCode = roomCode;
         SenderCode = senderCode;
         ReceiverCode = receiverCode;

         Message = message;

         CreatedDate = DateTime.Now;
      }

      private string GenerateCode()
      {
         var value = DateTime.Now;
         return $"ON-{value.ToString("yyyyMMddHHmmssffff")}";
      }
   }
}
