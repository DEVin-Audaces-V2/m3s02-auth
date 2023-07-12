using m3s02_auth.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace m3s02_auth.Config
{
    public class BaseMiddleware
    {
        private readonly RequestDelegate _next;
        public BaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Tudo escrito aqui será executado antes de chamar a controller da api
            
            await _next(context);
            // Tudo escrito aqui será executado depois de chamar a controller da api

        }
    }
}
