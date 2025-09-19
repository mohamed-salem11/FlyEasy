using FlyEasy.Data;
using FlyEasy.Models;
using FlyEasy.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;


namespace FlyEasy.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unit;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(IUnitOfWork unit, UserManager<ApplicationUser> userManager)
        {
            _unit = unit;
            _userManager = userManager;
        }

        // as user apply for flight
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Apply(int Id)
        {
            var flight = await _unit.Bookings.GetFlightByIdAsync(Id);
            if (flight == null) return NotFound();

            ViewBag.AvailableSeats = flight.AvailableSeats;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Apply(int NumberOfTickets, int Id)
        {
            var flight = await _unit.Bookings.GetFlightByIdAsync(Id);
            if (flight == null) return NotFound();

            HttpContext.Session.SetInt32("FlightId", Id);
            HttpContext.Session.SetInt32("NumberOfTickets", NumberOfTickets);

            if (NumberOfTickets > flight.AvailableSeats)
            {
                ModelState.AddModelError("NumberOfTickets", $"Only {flight.AvailableSeats} seats are available.");
                return View();
            }

            return RedirectToAction("DataOfPasserngers");
        }
        // take temporary data of passengers and save it after paying
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> DataOfPasserngers()
        {
            int? count = HttpContext.Session.GetInt32("NumberOfTickets");
            if (count == null) return RedirectToAction("Apply");

            var userId = _userManager.GetUserId(User);
            var passengers = await _unit.Bookings.GeneratePassengerList(count.Value, userId);
            return View(passengers);
        }

        [HttpPost]
        public IActionResult DataOfPasserngers(List<Passenger> passengers)
        {
            var userId = _userManager.GetUserId(User);
            passengers.ForEach(p => p.ApplicationUserId = userId);

            string json = JsonConvert.SerializeObject(passengers);
            HttpContext.Session.SetString("DataOfPasserngers", json);

            return RedirectToAction("Payment");
        }

        // payment using stripe
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Payment()
        {
            int flightId = (int)HttpContext.Session.GetInt32("FlightId");
            int ticketCount = (int)HttpContext.Session.GetInt32("NumberOfTickets");

            var flight = await _unit.Bookings.GetFlightByIdAsync(flightId);
            if (flight == null) return NotFound();

            var options = new SessionCreateOptions
            {
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Booking", null, Request.Scheme),
                CancelUrl = Url.Action("Cancel", "Booking", null, Request.Scheme),
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = flight.Price * 100,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{flight.From} to {flight.To} at {flight.DepartureTime}"
                        }
                    },
                    Quantity = ticketCount
                }
            }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);
            return Redirect(session.Url);
        }

        public IActionResult Cancel() => View("Cancel");

        public async Task<IActionResult> Success()
        {
            int flightId = (int)HttpContext.Session.GetInt32("FlightId");
            int ticketCount = (int)HttpContext.Session.GetInt32("NumberOfTickets");
            string passengersJson = HttpContext.Session.GetString("DataOfPasserngers");
            var userId = _userManager.GetUserId(User);

            var flight = await _unit.Bookings.GetFlightByIdAsync(flightId);
            if (flight == null || string.IsNullOrEmpty(passengersJson)) return NotFound();

            var passengers = JsonConvert.DeserializeObject<List<Passenger>>(passengersJson);

            var booking = new Booking
            {
                BookingDate = DateTime.Now,
                TotalPrice = ticketCount * flight.Price,
                NumberOfTickets = ticketCount,
                ApplicationUserId = userId,
                FlightId = flightId
            };

            await _unit.Bookings.AddAsync(booking);
            await _unit.CompleteAsync();

            passengers.ForEach(p => p.BookingId = booking.Id);
            await _unit.Bookings.SavePassengersAsync(passengers);
            await _unit.CompleteAsync();

            await _unit.Bookings.UpdateFlightSeatsAsync(flightId);
            await _unit.CompleteAsync();

            HttpContext.Session.Remove("FlightId");
            HttpContext.Session.Remove("NumberOfTickets");
            HttpContext.Session.Remove("DataOfPasserngers");

            return RedirectToAction("MyBookings");
        }
        // as user get to flights i have booked 
        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyBookings()
        {
            var userId = _userManager.GetUserId(User);
            var bookings = await _unit.Bookings.GetUserBookingsAsync(userId);
            return View(bookings);
        }

        // as admin see tickets of  flights 
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ViewApplicants(int id)
        {
            var applicants = await _unit.Bookings.GetBookingsByFlightIdAsync(id);
            return View(applicants);
        }
        //  show passengers of booking
        public async Task<IActionResult> ViewPassengers(int id)
        {
            var passengers = await _unit.Bookings.GetPassengersByBookingIdAsync(id);
            return View(passengers);
        }
    }


}


















