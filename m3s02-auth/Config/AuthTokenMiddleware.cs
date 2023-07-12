using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m3s02_auth.Config
{
    public class AuthTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly List<string> _tokens;
        public AuthTokenMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _tokens = configuration.GetSection("token").Get<List<string>>();
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!ValidateLogin(context))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }

        private bool ValidateLogin(HttpContext context)
        {
            var requestToken = context.Request.Headers.FirstOrDefault(x => x.Key == "api-key").Value;

            return _tokens.Contains(requestToken); ;
        }
    }
}
