using FlyEasy.Data;
using FlyEasy.Models;
using FlyEasy.Repositories.Interfaces;

namespace FlyEasy.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FlyEasyContext _context;

        public IBookingRepository Bookings { get; }

        public IFlightRepository Flights { get; }

        public GenericRepository<AirPlane> AirPlanes { get; }

        public UnitOfWork(FlyEasyContext context)
        {
            _context = context;
            Bookings = new BookingRepository(_context);
            AirPlanes = new GenericRepository<AirPlane>(_context);
            Flights = new FlightRepository(_context);
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }

}

















