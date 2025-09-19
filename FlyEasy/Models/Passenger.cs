using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlyEasy.Models
{
    public class Passenger
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; } 
        public  virtual ApplicationUser ApplicationUser { get; set; } 
 
    }

}


















