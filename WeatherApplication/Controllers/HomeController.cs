using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Establishes a connection with the API and retrieves the weather data.
        /// CHecks for a successful connection before processing data.
        /// Converts data to a readable json format, passes content back to javascript call.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Root> GetWeatherData()
        {
            HttpClient client = new HttpClient();
            Root weatherData = new Root();
            client.BaseAddress = new Uri("https://www.metaweather.com/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("https://www.metaweather.com/api/location/44544/");
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                weatherData = JsonConvert.DeserializeObject<Root>(responseBody);
            }

            return weatherData;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
