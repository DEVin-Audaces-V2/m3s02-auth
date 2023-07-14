using m3s02_auth.DTO;
using m3s02_auth.Interfaces.Services;
using m3s02_auth.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m3s02_auth.Config
{
    public class AuthBasicMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAutenticacaoServices _autenticacaoServices;
        public AuthBasicMiddleware(RequestDelegate next, IAutenticacaoServices autenticacao)
        {
            _autenticacaoServices = autenticacao;
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
            try
            {
                var header = context.Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value.ToString();
                var base64 = header.Split(" ")[1];
                var loginSenhaByte = Convert.FromBase64String(base64);
                var loginSenha = System.Text.Encoding.UTF8.GetString(loginSenhaByte).Split(":");

                var loginDTO = new LoginDTO()
                {
                    Usuario = loginSenha[0],
                    Senha = loginSenha[1]
                };

                return _autenticacaoServices.Autenticar(loginDTO);
            }
            catch {
                return false;
            }
        }
    }
}
