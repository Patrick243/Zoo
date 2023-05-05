 using TransportApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportApp.Application.Repositories;
using TransportApp.WebApp.Models;


namespace TransportApp.WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class BusStopController : Controller
    {
        private readonly IBusStopRepository _busStopRepo;

        public BusStopController(IBusStopRepository busStopRepo)
        {
            _busStopRepo = busStopRepo;
        }

        [AllowAnonymous]
        // GET: BusStopController
        public async Task<IActionResult> Index(int id)
        {
            KeepId.RouteId = id;
            var list = await _busStopRepo.GetListBusStopAsync();
            var busStops = (from busStop in list
                            where busStop.RouteId == id
                            select busStop).ToList();
            return View(busStops);
        }

        // GET: BusStopController/Create
        public async  Task<IActionResult> Create()
        {
            return View();
        }

        // POST: BusStopController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BusStop busStop)
        {

            if (ModelState.IsValid)
            {
                busStop.RouteId = KeepId.RouteId;

                await _busStopRepo.CreateNewBusStopAsync(busStop);

                TempData["success"] = "A new bus Stop Has Been Added";

                
                return RedirectToAction("Index", new {id =KeepId.RouteId});
            }
            return View(busStop);
        }

        // GET: BusStopController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var busStop = await _busStopRepo.GetBusStopByIdAsync(id);
            return View(busStop);
        }

        // POST: BusStopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BusStop busStop)
        {
            if (ModelState.IsValid)
            {
                busStop.RouteId = KeepId.RouteId;
                await _busStopRepo.UpdateBusStopAsync(busStop);

                TempData["success"] = $"The Bus Stop {busStop.Name} has Been edited";

                //var view = await Index(KeepId.RouteId);
                return RedirectToAction("Index" , "BusStop" ,new { id = KeepId.RouteId });
            }
            return View(busStop);
        }

        // GET: BusStopController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
           
            var busStop = await _busStopRepo.GetBusStopByIdAsync(id);
             return View(busStop);
        }

        // POST: BusStopController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(BusStop busStop)
        {
            if (ModelState.IsValid)
            {
                busStop.RouteId = KeepId.RouteId;
                await _busStopRepo.DeleteBusStopAsync(busStop);
                TempData["success"] = $"The Bus Stop {busStop.Name} has Been Deleted" ;
                return RedirectToAction("Index" , new {id = KeepId.RouteId});
            }
            return View(busStop);
        }
    }
}
