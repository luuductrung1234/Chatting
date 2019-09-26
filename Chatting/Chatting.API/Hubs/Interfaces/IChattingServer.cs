using System.Threading.Tasks;

// Chatting API Hub
using Chatting.API.Hubs.Requests;

namespace Chatting.API.Hubs.Interfaces
{
   public interface IChattingServer
   {
      Task DoSomethings(int seed);

      Task SendMessage(TextMessageRequest request);
   }
}
