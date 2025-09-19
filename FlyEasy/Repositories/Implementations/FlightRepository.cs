using FlyEasy.Data;
using FlyEasy.Models;
using FlyEasy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlyEasy.Repositories.Implementations
{
    public class FlightRepository: GenericRepository<Flight>, IFlightRepository
    {
        private readonly FlyEasyContext _context;

        public FlightRepository(FlyEasyContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Flight>> search(string from, string to, DateTime departureDate)
        {
            var flights = await _context.Flights.Where(a => a.From.Contains(from) && a.To.Contains(to) && a.DepartureTime >= departureDate).AsNoTracking().ToListAsync();
            return flights;
        }
        public async Task UpdateFlightSeatsAsync(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);
            int booked = await _context.Bookings
                .Where(b => b.FlightId == flightId)
                .SumAsync(b => b.NumberOfTickets);

            if (flight != null)
            {
                flight.AvailableSeats = flight.TotalSeats - booked;
                _context.Flights.Update(flight);
                await _context.SaveChangesAsync();
            }
        }
    }
}














