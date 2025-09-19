using System.ComponentModel.DataAnnotations;

namespace FlyEasy.Models
{
    public class AirPlane
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string TailNumber { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Flight> Flights { get; set; } 
    }
}













