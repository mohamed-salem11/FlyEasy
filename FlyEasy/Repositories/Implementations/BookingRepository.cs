using FlyEasy.Data;
using FlyEasy.Models;
using FlyEasy.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace FlyEasy.Repositories.Implementations
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly FlyEasyContext _context;

        public BookingRepository(FlyEasyContext context) : base(context)
        {
            _context = context;
        }


        public async Task<Flight?> GetFlightByIdAsync(int id)
        {
            return await _context.Flights.FindAsync(id);
        }

        public async Task<int> GetAvailableSeatsAsync(int flightId)
        {
            var flight = await _context.Flights.FindAsync(flightId);
            return flight?.AvailableSeats ?? 0;
        }

        public async Task<List<Passenger>> GeneratePassengerList(int count, string userId)
        {
            var passengers = new List<Passenger>();
            for (int i = 0; i < count; i++)
            {
                passengers.Add(new Passenger { ApplicationUserId = userId });
            }

            return await Task.FromResult(passengers);
        }


        public async Task SavePassengersAsync(List<Passenger> passengers)
        {
            await _context.Passengers.AddRangeAsync(passengers);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetUserBookingsAsync(string userId)
        {
                var bookings= await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Passengers)
                .Where(b => b.ApplicationUserId == userId)
                .AsNoTracking().ToListAsync();
                 return bookings;
        }

        public async Task<List<Booking>> GetBookingsByFlightIdAsync(int flightId)
        {
            return await _context.Bookings
                .Include(b => b.Passengers)
                .Where(b => b.FlightId == flightId)
                .AsNoTracking().ToListAsync();
        }

        public async Task<List<Passenger>> GetPassengersByBookingIdAsync(int bookingId)
        {
            return await _context.Passengers
                .Where(p => p.BookingId == bookingId)
                .AsNoTracking().ToListAsync();
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











