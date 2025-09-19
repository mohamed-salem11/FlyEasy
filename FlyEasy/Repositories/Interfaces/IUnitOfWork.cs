using FlyEasy.Data;
using FlyEasy.Models;
using FlyEasy.Repositories.Implementations;

namespace FlyEasy.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get; }
        IFlightRepository Flights { get; }
        GenericRepository<AirPlane> AirPlanes { get; }
        Task<int> CompleteAsync();
    }

 }
