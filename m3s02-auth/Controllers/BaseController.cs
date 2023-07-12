using m3s02_auth.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace m3s02_auth.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly List<TokenCliente> _tokensClientes;
        public BaseController(IConfiguration configuration)
        {
            _tokensClientes = configuration.GetSection("tokenCliente").Get<List<TokenCliente>>();
        }
        protected TokenCliente GetCliente()
        {
            var requestToken = Request.Headers.FirstOrDefault(x => x.Key == "api-key").Value;
            return _tokensClientes.FirstOrDefault(x => x.Token == requestToken);

        }
    }
}
