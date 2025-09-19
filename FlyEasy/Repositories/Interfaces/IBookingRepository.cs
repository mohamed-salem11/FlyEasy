using FlyEasy.Models;

namespace FlyEasy.Repositories.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {

        Task<List<Booking>> GetBookingsByFlightIdAsync(int flightId);
        Task<List<Passenger>> GetPassengersByBookingIdAsync(int bookingId);
        Task UpdateFlightSeatsAsync(int flightId);
        Task<int> GetAvailableSeatsAsync(int flightId);
        Task<Flight?> GetFlightByIdAsync(int id);
        Task SavePassengersAsync(List<Passenger> passengers);
        Task<List<Passenger>> GeneratePassengerList(int count, string userId);
        Task<List<Booking>> GetUserBookingsAsync(string userId);


    }

}
















