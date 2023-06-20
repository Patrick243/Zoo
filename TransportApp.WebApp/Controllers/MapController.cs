using Microsoft.AspNetCore.Mvc;
using TransportApp.Application.Repositories;
using TransportApp.WebApp.Models;

namespace TransportApp.WebApp.Controllers
{
    public class MapController : Controller
    {
        private readonly IRouteRepository _routeRepository;

        public MapController(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var route = await _routeRepository.GetRouteByIdAsync(id);

            RouteModel routeModel = new RouteModel();
            routeModel.Id = route.Id;
            routeModel.Depart = route.InitialPlace;
            routeModel.End = route.FinalPlace;

            return View(routeModel);
        }
    }
}
