using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

// Chatting Domain
using Chatting.Domain;
using Chatting.Domain.Interfaces;

// Chatting API Hubs
using Chatting.API.Hubs.Interfaces;
using Chatting.API.Utils;
using Chatting.API.Hubs.Requests;

namespace Chatting.API.Hubs
{
   public class ChattingHub : Hub<IChattingClient>, IChattingServer
   {
      private readonly IChatMessageRepository _messageRepository;

      public ChattingHub(IChatMessageRepository messageRepository)
      {
         _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
      }

      #region Business Methods

      public async Task DoSomethings(int seed)
      {
         var random = new Random(seed);

         var sentinel = random.Next(1, 100);

         do
         {
            Thread.Sleep(1000);

            await Clients.Caller.ReceiveSomethingsHappen($"Current sentinel is {sentinel}");

            sentinel = random.Next(1, 100);
         }
         while (sentinel != 50);

         await Clients.Caller.Finished();
      }

      public async Task SendMessage(TextMessageRequest request)
      {
         var newMessage = new ChatMessage("",
            request.SenderCode,
            request.ReceiverCode,
            request.Message);

         await _messageRepository.AddChatMessageAsync(newMessage);

         await Clients.Group(request.ReceiverCode).ReceiveMessage(newMessage);
      }

      #endregion

      #region Connection Event Methods

      public async override Task OnConnectedAsync()
      {
         await AddConnectionToGroup(HubConstants.UserIdentityKey);
      }

      public async override Task OnDisconnectedAsync(Exception exception)
      {
         await RemoveConnectionToGroup(HubConstants.UserIdentityKey);
      }

      #endregion

      #region Helper Methods

      public async Task<string> AddConnectionToGroup(string key)
      {
         var query = this.Context.GetHttpContext().Request.Query;

         if (query.ContainsKey(key))
         {
            var groupName = query[key];
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);

            return groupName;
         }

         return string.Empty;
      }

      public async Task<string> RemoveConnectionToGroup(string key)
      {
         var query = this.Context.GetHttpContext().Request.Query;

         if (query.ContainsKey(key))
         {
            var groupName = query[key];
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, groupName);

            return groupName;
         }

         return string.Empty;
      }

      #endregion
   }
}
