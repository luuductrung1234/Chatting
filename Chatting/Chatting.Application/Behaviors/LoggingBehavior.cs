﻿using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Chatting.Application.Behaviors
{
   public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
   {
      private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
      public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

      public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
      {
         try
         {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            var response = await next();
            _logger.LogInformation($"Handled {typeof(TRequest).Name}");
            return response;
         }
         catch (Exception ex)
         {
            _logger.LogError(new EventId(ex.HResult), ex, ex.Message);
            throw;
         }
      }
   }
}