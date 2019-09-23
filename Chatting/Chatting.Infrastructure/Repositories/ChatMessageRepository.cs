using Chatting.Domain;
using Chatting.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatting.Infrastructure.Repositories
{
   public class ChatMessageRepository : IChatMessageRepository
   {
      public void CreateChatMessage(ChatMessage chatMessage)
      {
         throw new NotImplementedException();
      }

      public ChatMessage GetChatMessage(Guid id)
      {
         throw new NotImplementedException();
      }

      public ChatMessage GetChatMessage(string messageCode)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<ChatMessage> GetChatMessages()
      {
         throw new NotImplementedException();
      }

      public IEnumerable<ChatMessage> GetChatMessagesByRoom(string roomCode)
      {
         throw new NotImplementedException();
      }

      public IEnumerable<ChatMessage> GetChatMessagesByUser(string userCode)
      {
         throw new NotImplementedException();
      }
   }
}
