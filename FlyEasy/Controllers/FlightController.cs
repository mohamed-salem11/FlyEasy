using FlyEasy.Data;
using FlyEasy.Models;
using FlyEasy.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyEasy.Controllers
{
    
    public class FlightController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<ApplicationUser> _userManager;

        public FlightController(IUnitOfWork unit, UserManager<ApplicationUser> userManager)
        {
            _unit = unit;
            _userManager = userManager;
        }

        // GET: Flight for admin and user
        [Authorize]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            int pageSize = 6;
            var allFlights = await _unit.Flights.GetAllAsync();
            var Flights = allFlights .Skip((page - 1) * pageSize) .Take(pageSize).ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(allFlights.Count() / (double)pageSize);
            return View(Flights);
        }


        // Search :Flight for admin,user

        [HttpGet]
        public async Task<IActionResult> Search() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchResults(string from, string to, DateTime departureDate)
        {
            var flights = await _unit.Flights.search(from, to, departureDate);
            return View(flights);
        }


        // GET: Flight/Create for admin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var airPlanes =  await _unit.AirPlanes.GetAllAsync();
            ViewData["AirPlaneId"] = new SelectList(airPlanes, "Id", "Id");
            return View();
        }

        // POST: Flight/Create
      
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,From,To,DepartureTime,ArrivalTime,Price,AvailableSeats,TotalSeats,AirPlaneId,AllowedBaggage,FlightClass")] Flight flight)
        {
              
                flight.AvailableSeats = flight.TotalSeats;
                await _unit.Flights.AddAsync(flight);
                await _unit.CompleteAsync();
                return RedirectToAction(nameof(GetAll));

          
        }

        // GET: Flight/Update/5 for admin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _unit.Flights.GetByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            var airPlanes = await _unit.AirPlanes.GetAllAsync();
            ViewData["AirPlaneId"] = new SelectList(airPlanes, "Id", "Id");
            return View(flight);
        }

        // POST: Flight/Update/5


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,From,To,DepartureTime,ArrivalTime,Price,TotalSeats,AirPlaneId,AllowedBaggage,FlightClass")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }
       
           _unit.Flights.Update(flight);
           await _unit.Flights.UpdateFlightSeatsAsync(id);
            await _unit.CompleteAsync();
            return RedirectToAction(nameof(GetAll));
 
        }

        // GET: Flight/Delete/5 for admin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _unit.Flights.GetByIdAsync(id);


            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _unit.Flights.GetByIdAsync(id);
            if (flight != null)
            {
                _unit.Flights.Remove(flight);
            }

            await _unit.CompleteAsync();
            return RedirectToAction(nameof(GetAll));
        }
      

    }
}










