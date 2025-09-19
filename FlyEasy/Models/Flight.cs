using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlyEasy.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int Price { get; set; }
        public int TotalSeats { get; set; }
        public int? AvailableSeats {  get; set; }
        public string AllowedBaggage { get; set; }
        public string FlightClass {  get; set; }    

        [ForeignKey("AirPlane")]
        public int AirPlaneId { get; set; }
        public virtual AirPlane AirPlane { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}











