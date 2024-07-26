using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using TreeDomainLibrary.ErrorHandlingHelper;

namespace TreeStructureWebApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Something went wrong: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is NullReferenceException)
            {
                context.Response.StatusCode = 404;
            }
            else if (exception is InvalidOperationException)
            {
                context.Response.StatusCode = 400;
            }
            else if (context.Response.StatusCode == 200 || context.Response.StatusCode.ToString().IsNullOrEmpty())
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problemDetails = new()
            {
                Title = exception.Message,
                Status = context.Response.StatusCode,
                Message = exception.Message,
            };

            var data = problemDetails.ToString();
            return context.Response.WriteAsync(data);
        }
    }
}
