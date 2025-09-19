using Microsoft.AspNetCore.Identity;

namespace FlyEasy.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
