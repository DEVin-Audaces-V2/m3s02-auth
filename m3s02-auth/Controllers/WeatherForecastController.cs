using m3s02_auth.Exceptions;
using m3s02_auth.Model;
using Microsoft.AspNetCore.Authorization;
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
    public class WeatherForecastController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;


        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                        IConfiguration configuration)
                                        : base(configuration)
        {
            _logger = logger;

        }

        [HttpGet]
        [Authorize(Roles = "Aluno,Professor")]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            var nome = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Nome").Value;
            var interno = bool.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Interno").Value);
            if (!interno)
            {
                return Ok("Usuario Externo");
            }

            if(nome == "vitor")
            {
                return Ok("Usuario Bloqueado");
            }


            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Error")]
        [Authorize(Roles = "Aluno")]
        public ActionResult<IEnumerable<WeatherForecast>> GetError()
        {
            throw new NotImplementedException("Não encontrado");
        }

        [HttpPost]
        [Authorize(Roles = "Professor")]
        public ActionResult Post(WeatherForecast weatherForecast)
        {
            return Ok(weatherForecast);
        }

        [HttpPost("Auth")]
        [Authorize]
        public ActionResult Post()
        {
            var clienteToken = GetCliente();

            return Ok(clienteToken);
        }

      
       
    }
}
