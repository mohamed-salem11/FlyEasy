using FlyEasy.Data;
using FlyEasy.Models;
using FlyEasy.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlyEasy.Controllers
{

  [Authorize(Roles = "Admin")]
    public class AirPlaneController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<ApplicationUser> _userManager;

        public AirPlaneController(IUnitOfWork unit, UserManager<ApplicationUser> userManager)
        {
            _unit = unit;
            _userManager = userManager;
        }


        //Get All AirPlanes for admin &&& using pagination
        public async Task<IActionResult> GetAll(int page = 1)
        {
            int pageSize = 6; 
            var allAirplanes = await _unit.AirPlanes.GetAllAsync();

            var pagedAirplanes = allAirplanes.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(allAirplanes.Count() / (double)pageSize);

            return View(pagedAirplanes);
        }

        //create  new AirPlane for admin
        public async Task<IActionResult> Create() 
        {
            return View(); 
        
        }
    
        
        [HttpPost]
        public async Task<IActionResult> Create(AirPlane airPlane)
        {

                _unit.AirPlanes.AddAsync(airPlane);
                await _unit.CompleteAsync();
                return RedirectToAction(nameof(GetAll));
        }

        //update AirPlane for admin
        public async Task<IActionResult> Update(int id)
        {
            var airplane=await _unit.AirPlanes.GetByIdAsync(id);
            if (airplane == null)
            {
                return NotFound();
            }
            return View(airplane);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AirPlane airplane)
        {

                _unit.AirPlanes.Update(airplane);
                await _unit.CompleteAsync();
                return RedirectToAction(nameof(GetAll));
        }

        //delete AirPlane for admin
        public async Task<IActionResult> Delete(int id)
        {
            var airplane =await _unit.AirPlanes.GetByIdAsync(id);
            if (airplane == null)
            {
                return NotFound();
            }
            return View(airplane);
           
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airplane = await _unit.AirPlanes.GetByIdAsync(id);
            if(airplane == null)
            {
                return NotFound();
            }
            _unit.AirPlanes.Remove(airplane);
            await _unit.CompleteAsync();
            return RedirectToAction(nameof(GetAll));
        }

    }
}















