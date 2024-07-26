using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TreeStructureWebApi.Middleware
{
    public class CustMiddleware
    {
        private readonly RequestDelegate _next;
  
        public CustMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("This is invoked from Custom Middleware");
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustMiddleware(this IApplicationBuilder builder)
        {
            Console.WriteLine("This is invoked from Custom Middleware Extensions class......");
            return builder.UseMiddleware<CustMiddleware>();
        }
    }
}

