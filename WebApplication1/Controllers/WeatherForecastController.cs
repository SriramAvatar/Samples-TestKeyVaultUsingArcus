using Arcus.Security.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestKeyvault.Controllers
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
        private readonly ISecretProvider _secretProvider;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISecretProvider secretProvider)
        {
            _logger = logger;
            _secretProvider = secretProvider;
        }

        [HttpGet]
        public async Task<String> Get()
        {
            var rng = new Random();
            var secret = await _secretProvider.GetSecretAsync("<YourKeyvaultSecretName>");
            return secret.Value;
        }
    }
}
