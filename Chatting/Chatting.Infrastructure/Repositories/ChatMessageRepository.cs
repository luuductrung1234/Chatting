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
   public class ChatMessageRepository : BaseRepository<ChatMessage>, IChatMessageRepository
   {
      public ChatMessageRepository(IGenericDbContext dbContext)
         : base(dbContext, partitionKey: "roomCode")
      {

      }

      public async Task AddChatMessageAsync(ChatMessage chatMessage)
      {
         await this.AddOneAsync(chatMessage);
      }

      public Task<ChatMessage> GetChatMessageAsync(Guid id)
      {
         throw new NotImplementedException();
      }

      public Task<ChatMessage> GetChatMessageAsync(string messageCode)
      {
         throw new NotImplementedException();
      }

      public Task<IEnumerable<ChatMessage>> GetChatMessagesAsync()
      {
         throw new NotImplementedException();
      }

      public Task<IEnumerable<ChatMessage>> GetChatMessagesByRoomAsync(string roomCode)
      {
         throw new NotImplementedException();
      }

      public Task<IEnumerable<ChatMessage>> GetChatMessagesByUserAsync(string userCode)
      {
         throw new NotImplementedException();
      }
   }
}
