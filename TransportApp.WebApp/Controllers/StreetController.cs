using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportApp.Application.Repositories;
using TransportApp.WebApp.Models;

namespace TransportApp.WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class StreetController : Controller
    {
        public IStreetRepository _streetRepo;

        public StreetController(IStreetRepository streetRepo)
        {
            _streetRepo = streetRepo;
        }
        [AllowAnonymous]
        // GET: StreetController
        public async Task<IActionResult> Index(int id)
        {
            KeepId.BusStopId = id;
            var list = await _streetRepo.GetListStreetAsync();
            var listNew = (from street in list
                          where street.BusStopId == id
                          select street).ToList();
            return View(listNew);
        }

        // GET: StreetController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: StreetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransportApp.Domain.Street street)
        {
            if (ModelState.IsValid)
            {
                street.BusStopId = KeepId.BusStopId;
                await _streetRepo.CreateNewStreetAsync(street);
                TempData["success"] = "The street Has Been Added";
                return RedirectToAction("Index", new { id = KeepId.BusStopId });
            }
            return View(street);
        }

        // GET: StreetController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var street = await _streetRepo.GetStreetByIdAsync(id);
            return View(street); 
        }

        // POST: StreetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TransportApp.Domain.Street street)
        {
            if (ModelState.IsValid)
            {
                street.BusStopId = KeepId.BusStopId;
                await _streetRepo.UpdateStreetAsync(street);
                TempData["success"] = "A record Has Been Updated";
                return RedirectToAction("Index", new { id = KeepId.BusStopId });
            }
            return View();
        }

        // GET: StreetController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var street = await _streetRepo.GetStreetByIdAsync(id);
            return View(street);
        }

        // POST: StreetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TransportApp.Domain.Street street)
        {
            street.BusStopId = KeepId.BusStopId;
            await _streetRepo.DeleteStreetAsync(street);
            TempData["sucess"] = "A record Has Been Deleted";
            return  RedirectToAction( "Index",new { id = KeepId.BusStopId });
        }
    }
}
