using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TransportApp.Application.Repositories;
using TransportApp.Domain;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace TransportApp.WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class RouteController : Controller
    {
        private readonly IRouteRepository _routeRepo;


        public RouteController(IRouteRepository routeRepo)
        {
            _routeRepo = routeRepo;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var routes = await _routeRepo.GetListRouteAsync();

            return View(routes);
        }



        //public async Task<ActionResult> GetItinerary(string start, string end)
        //{
        //    var apiKey = "AIzaSyB2AtBtUgaKNBQGFrSwH-cBsiIkQmKMViY";
        //    var httpClient = new HttpClient();

        //    var url = $"https://maps.googleapis.com/maps/api/directions/json?origin={start}&destination={end}&key={apiKey}";
        //    var response = await httpClient.GetAsync(url);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseContent = await response.Content.ReadAsStringAsync();
        //        var responseObject = JObject.Parse(responseContent);

        //        var distance = responseObject["routes"][0]["legs"][0]["distance"]["text"].Value<string>();
        //        var duration = responseObject["routes"][0]["legs"][0]["duration"]["text"].Value<string>();
        //        var directions = responseObject["routes"][0]["legs"][0]["steps"].Select(x => x["html_instructions"].Value<string>());

        //        ViewBag.Start = start;
        //        ViewBag.End = end;
        //        ViewBag.Distance = distance;
        //        ViewBag.Duration = duration;
        //        ViewBag.Directions = directions;

        //        return View();
        //    }
        //    else
        //    {
        //        // handle error
        //    }
        //}




        // GET: RouteController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RouteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransportApp.Domain.Route route)
        {
            if (ModelState.IsValid)
            {
                await _routeRepo.CreateNewRouteAsync(route);
                TempData["success"] = "A Route Has Been Created";
                return RedirectToAction("Index");
            }
            return View(route);
        }

        // GET: RouteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var route = await _routeRepo.GetRouteByIdAsync(id);

            return View(route);
        }

        // POST: RouteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TransportApp.Domain.Route route)
        {

            if (ModelState.IsValid)
            {
                await _routeRepo.UpdateRouteAsync(route);
                TempData["success"] = $"the route {route.Name} has been edited";
                return RedirectToAction("Index");
            }
            return View(route);
        }

        // GET: RouteController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var route = await _routeRepo.GetRouteByIdAsync(id);

            return View(route);
        }

        // POST: RouteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TransportApp.Domain.Route route)
        {
            await _routeRepo.DeleteRouteAsync(route);
            TempData["success"] = $"Deleted route {route.Name}";
            return RedirectToAction("Index");
        }
    }
}
