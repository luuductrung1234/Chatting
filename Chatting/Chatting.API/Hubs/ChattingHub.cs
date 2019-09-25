using System;
using System.Threading;
using System.Threading.Tasks;
using Chatting.API.Utils;
using Microsoft.AspNetCore.SignalR;

namespace Chatting.API.Hubs
{
   public class ChattingHub : Hub
   {
      private bool _isUserOnScreen = false;

      public ChattingHub()
      {

      }

      public async Task DoSomethings(int seed)
      {
         var random = new Random(seed);

         var sentinel = random.Next(1, 100);

         do
         {
            Thread.Sleep(1000);

            await Clients.Caller.SendAsync("ReceiveSomethingsHappen", $"Current sentinel is {sentinel}");

            sentinel = random.Next(1, 100);
         }
         while (sentinel != 50);

         await Clients.Caller.SendAsync("Finished");
      }

      public async Task SendMessage(string message, string receiverCode)
      {
         await Clients.Group(receiverCode).SendAsync("ReceiveMessage", message);
      }

      public async override Task OnConnectedAsync()
      {
         await AddConnectionToGroup(HubConstants.UserIdentityKey);
      }

      public async override Task OnDisconnectedAsync(Exception exception)
      {
         await RemoveConnectionToGroup(HubConstants.UserIdentityKey);
      }

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
