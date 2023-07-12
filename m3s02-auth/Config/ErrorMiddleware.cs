using m3s02_auth.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace m3s02_auth.Config
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {  
            try { 
                await _next(context);
            }
            catch (Exception ex)
            {
                await TratamentoExcecao(context, ex);
            }

        }
        public async Task TratamentoExcecao(HttpContext context, Exception ex)
        {
            int status;
            string message;

            if (ex is NotFoundException)
            {
                status  = StatusCodes.Status404NotFound;
                message=  ex.Message;
            }
            else if (ex is ArgumentException)
            {
                status = StatusCodes.Status406NotAcceptable;
                message = ex.Message;
            }
            else
            {
                status = StatusCodes.Status500InternalServerError;
                message = "Ocorreu um erro, tente novamente mais tarde";
            }

            context.Response.StatusCode = status;
            await context.Response.WriteAsync(message);
        }
    }
}
