using GoogleMaps.LocationServices;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Web;
using WebApplication1.Models;
using System.Net.Http;

namespace WebApplication1.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<ActionResult> ShowRoute(string startAddress, string endAddress)
        {
            var locationService = new GoogleLocationService();
            var startLocation = locationService.GetLatLongFromAddress(startAddress);
            var endLocation = locationService.GetLatLongFromAddress(endAddress);

            var apiKey = "AIzaSyB2AtBtUgaKNBQGFrSwH-cBsiIkQmKMViY";
            var baseUrl = "https://maps.googleapis.com/maps/api/directions/json";
            var url = $"{baseUrl}?origin={startLocation.Latitude},{startLocation.Longitude}&destination={endLocation.Latitude},{endLocation.Longitude}&key={apiKey}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var directions = JsonConvert.DeserializeObject<DirectionsResponse>(content);
                    return View(directions);
                }
                else
                {
                    return View("Error");
                }
            }
        }
        [HttpPost]
        public async Task<ActionResult> GetRoute(string startAddress, string endAddress)
        {
            var request = new DirectionsRequest
            {
                Origin = startAddress,
                Destination = endAddress,
                ApiKey = "AIzaSyB2AtBtUgaKNBQGFrSwH-cBsiIkQmKMViY"
            };

            var builder = new UriBuilder("https://maps.googleapis.com/maps/api/directions/json");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["origin"] = request.Origin;
            query["destination"] = request.Destination;
            query["key"] = request.ApiKey;
            builder.Query = query.ToString();
            var url = builder.ToString();

            var client = new HttpClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error
            }

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<DirectionsResponse>(json);

            return View("ShowRoute", result);
        }


    }
}