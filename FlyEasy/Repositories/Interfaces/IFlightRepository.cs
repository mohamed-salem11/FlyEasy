using FlyEasy.Models;

namespace FlyEasy.Repositories.Interfaces
{
    public interface IFlightRepository:IGenericRepository<Flight>
    {
        Task <List<Flight>> search(string from, string to, DateTime departureDate);
        Task  UpdateFlightSeatsAsync(int flightId);
    }
}
