using FunctionMonkey.Abstractions.Http;
using FunctionMonkey.Commanding.Abstractions.Validation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Proxet.Tournament.WebApi.Handlers
{
    public class HttpResponseHandler : IHttpResponseHandler
    {
        public Task<IActionResult> CreateResponseFromException<TCommand>(TCommand command, Exception ex)
        {
            throw ex;
        }

        public Task<IActionResult> CreateResponse<TCommand, TResult>(TCommand command, TResult result)
        {
            IActionResult actionResult = result as IActionResult;
            return Task.FromResult(actionResult);
        }

        public Task<IActionResult> CreateResponse<TCommand>(TCommand command)
        {
            return null;
        }

        public Task<IActionResult> CreateValidationFailureResponse<TCommand>(TCommand command, ValidationResult validationResult)
        {
            return null;
        }
    }
}