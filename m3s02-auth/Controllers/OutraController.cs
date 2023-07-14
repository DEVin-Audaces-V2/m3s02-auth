using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace m3s02_auth.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class OutraController : BaseController
    {
        public OutraController(IConfiguration configuration) : base(configuration)
        {
        }

        [HttpGet]
        public ActionResult Get()
        {
            var rng = new Random();
            return Ok(rng.Next());
        }

    }
}
