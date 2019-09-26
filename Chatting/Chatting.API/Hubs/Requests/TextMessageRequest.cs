using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatting.API.Hubs.Requests
{
   public class TextMessageRequest
   {
      public string SenderCode { get; set; }

      public string ReceiverCode { get; set; }

      public string Message { get; set; }
   }
}
