using System;
using System.Collections.Generic;

namespace Chatting.Domain.Interfaces
{
   public interface IChatMessageRepository
   {
      void CreateChatMessage(ChatMessage chatMessage);

      IEnumerable<ChatMessage> GetChatMessages();

      IEnumerable<ChatMessage> GetChatMessagesByUser(string userCode);

      IEnumerable<ChatMessage> GetChatMessagesByRoom(string roomCode);

      ChatMessage GetChatMessage(Guid id);

      ChatMessage GetChatMessage(string messageCode);
   }
}
