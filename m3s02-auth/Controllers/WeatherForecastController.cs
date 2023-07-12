﻿using m3s02_auth.Exceptions;
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
    public class WeatherForecastController :  BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        
        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
                                        IConfiguration configuration) 
                                        : base (configuration)
        {
            _logger = logger;
           
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
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
        public ActionResult<IEnumerable<WeatherForecast>> GetError()
        {
                throw new NotImplementedException("Não encontrado");
        }

        [HttpPost]
        public ActionResult Post(WeatherForecast weatherForecast)
        {
            return Ok(weatherForecast) ;
        }

        [HttpPost("Auth")]
        public ActionResult Post()
        {
            var clienteToken = GetCliente();

            return Ok(clienteToken);
        }

      
       
    }
}
