using IdentityModel.Client;
using identityServer4Test.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;






namespace identityServer4Test.Controllers
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
        private readonly ITokenService _tokenService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITokenService tokenService)
        {
            _logger = logger;
           _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
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

        [HttpGet]
        [Route("Test")]
        public async Task <IEnumerable<WeatherForecast>> Test()
        {
            //using ()
            //{

            //    RestClient client = new RestClient("https://localhost:44304/WeatherForecast")
            //    var tokenResponse = await  _tokenService.GetToken("weatherapi.read");

            //    client.SetBearerToken(tokenResponse.AccessToken);
            //    var request = new RestRequest(Method.GET);
            //    IRestResponse response = client.Execute(request);
            //    Console.WriteLine(response.Content);

            //}

            var tokenResponse = await _tokenService.GetToken("weatherapi.read");

            var client = new RestClient("https://localhost:44304/WeatherForecast");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", tokenResponse.AccessToken));
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(response.Content);
           


            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }

    
}
