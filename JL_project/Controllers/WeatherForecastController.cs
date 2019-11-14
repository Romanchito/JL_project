using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JL_project.Context;
using JL_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JL_project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private MyContext db;

        public WeatherForecastController(MyContext db)
        {
            this.db = db;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;        

        [HttpGet]
        public IEnumerable<Film> Get()
        {

            return db.Films.ToArray();

        }
    }
}
