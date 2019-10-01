﻿using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// SalesHub OrderManagement Domain
using Chatting.Application.Exceptions;

namespace Chatting.Application.Behaviors
{
   public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
   {
      private readonly IValidator<TRequest>[] _validators;
      public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

      public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
      {
         var failures = _validators
             .Select(v => v.Validate(request))
             .SelectMany(result => result.Errors)
             .Where(error => error != null)
             .ToList();

         if (failures.Any())
         {
            throw new ChattingRequestValidationException(typeof(TRequest).Name, new ValidationException("Validation exception", failures));
         }

         var response = await next();
         return response;
      }
   }
}
