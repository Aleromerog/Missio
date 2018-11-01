using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MissioServer.Services.Services;
using Newtonsoft.Json;
using StringResources;

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
            catch (InvalidPasswordException)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 401;

                var error = JsonConvert.SerializeObject(AppResources.InvalidPassword);
                await httpContext.Response.WriteAsync(error);
            }
            catch (InvalidUserNameException)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = 401;

                var error = JsonConvert.SerializeObject(AppResources.InvalidUserName);
                await httpContext.Response.WriteAsync(error);
            }
        }
    }
}