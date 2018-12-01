using System.Threading.Tasks;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MissioServer
{
    public class HandleUserCredentialsExceptions
    {
        private readonly RequestDelegate _next;

        public HandleUserCredentialsExceptions(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (LogInException e)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 401;

                var error = JsonConvert.SerializeObject(e.ErrorMessage);
                await httpContext.Response.WriteAsync(error);
            }
        }
    }
}