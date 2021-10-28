using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCApp_IdentityServer_Test.Models;
using MVCApp_IdentityServer_Test.Services;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp_IdentityServer_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _tokenService;

        public HomeController(ILogger<HomeController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public  IActionResult Index()
        {

            return View();
        }

        [Authorize]
        public async Task<IActionResult>  Weather()
        {
            var tokenResponse = await _tokenService.GetToken("weatherapi.read");

            var client = new RestClient("https://localhost:44304/WeatherForecast");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", string.Format("Bearer {0}", tokenResponse.AccessToken));
            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(response.Content);
            return View(result);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
