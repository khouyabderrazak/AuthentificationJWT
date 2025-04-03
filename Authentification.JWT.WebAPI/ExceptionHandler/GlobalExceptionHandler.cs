
using Microsoft.AspNetCore.Diagnostics;
using NLog;
using System.Net;

namespace Authentification.JWT.WebAPI.Middlwares
{
    public  class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly NLog.ILogger _logger;
        public GlobalExceptionHandler(NLog.ILogger logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.Error(exception);

            var statusCode= httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(new { Code = statusCode , Message = "Some error occured"}, cancellationToken);

            return true;
        }
    }
}
