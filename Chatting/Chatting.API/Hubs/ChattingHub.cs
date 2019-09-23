using System;
using System.Threading;
using System.Threading.Tasks;
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

      public override Task OnConnectedAsync()
      {
         var connectionId = Context.ConnectionId;

         return base.OnConnectedAsync();
      }

      public override Task OnDisconnectedAsync(Exception exception)
      {
         var connectionId = Context.ConnectionId;

         return base.OnDisconnectedAsync(exception);
      }
   }
}
