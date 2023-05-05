using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportApp.Application.Repositories;
using TransportApp.Domain;
using TransportApp.WebApp.Models;

namespace TransportApp.WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class HouseController : Controller
    {
        public IHouseRepository _houseRepo;
        public HouseController(IHouseRepository houseRepo)
        {
            _houseRepo = houseRepo;
        }
        // GET: HouseController
        [AllowAnonymous]
        public async Task<IActionResult> Index(int id)
        {
            KeepId.StreetId = id;
            var houses = await _houseRepo.GetListHouseAsync();
            var list = (from house in houses
                       where house.StreetId == id
                       select house).ToList();
            return View(list);
        }

        // GET: HouseController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HouseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(House house)
        {
            if (ModelState.IsValid)
            {
                house.StreetId = KeepId.StreetId;
                await _houseRepo.CreateNewHouseAsync(house);
                TempData["success"] = $"A new House Has Been Added";
                return RedirectToAction("Index", new { id = KeepId.StreetId }); ;
            }
            return View(house);
        }

        // GET: HouseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var house = await _houseRepo.GetHouseByIdAsync(id);
            return View(house);
        }

        // POST: HouseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(House house)
        {
            if (ModelState.IsValid)
            {
                house.StreetId = KeepId.StreetId;
                await _houseRepo.UpdateHouseAsync(house);
                TempData["success"] = "A record Has Been Edited";
                return RedirectToAction("Index", new { id = KeepId.StreetId });
            }
            return View(house);
        }

        // GET: HouseController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var house = await _houseRepo.GetHouseByIdAsync(id);
            return View(house);
        }

        // POST: HouseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(House house)
        {
            house.StreetId = KeepId.StreetId;
            await _houseRepo.DeleteHouseAsync(house);
            TempData["success"] = "A record Has Been Deleted";
            return RedirectToAction("Index", new { id = KeepId.StreetId }); ;
        }
    }
}
