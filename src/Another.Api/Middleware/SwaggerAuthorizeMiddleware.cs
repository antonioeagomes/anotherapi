using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Another.Api.Middleware
{
    public class SwaggerAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public SwaggerAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Path.StartsWithSegments("/swagger") && !context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next.Invoke(context);
        }
    }
}
