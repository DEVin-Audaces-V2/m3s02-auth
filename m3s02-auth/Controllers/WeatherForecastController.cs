using m3s02_auth.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace m3s02_auth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly List<string> _tokens;

        private readonly List<TokenCliente> _tokensClientes;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _tokens = configuration.GetSection("token").Get<List<string>>();
            _tokensClientes = configuration.GetSection("tokenCliente").Get<List<TokenCliente>>();
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            if (!ValidateLogin())
                return Unauthorized();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost]
        public ActionResult Post(WeatherForecast weatherForecast)
        {
            if (!ValidateLogin(out var clienteToken))
                return Unauthorized();

            return Ok(weatherForecast) ;
        }


        [HttpPost("Auth")]
        public ActionResult Post()
        {
            if (!ValidateLogin(out var clienteToken))
                return Unauthorized();

            return Ok(clienteToken);
        }

        private bool ValidateLogin()
        {
            var requestToken = Request.Headers.FirstOrDefault(x => x.Key == "api-key").Value;

            return _tokens.Contains(requestToken); ;
        }
        private bool ValidateLogin( out TokenCliente clienteToken)
        {
            var requestToken = Request.Headers.FirstOrDefault(x => x.Key == "api-key").Value;

             clienteToken = _tokensClientes.FirstOrDefault(x => x.Token == requestToken);

            return  clienteToken != null ;
        }
    }
}
