using FlyEasy.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FlyEasy.Data;

public class FlyEasyContext : IdentityDbContext<ApplicationUser>
{
    public FlyEasyContext(DbContextOptions<FlyEasyContext> options)
        : base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<AirPlane> AirPlanes { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Passenger> Passengers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Flight>()
            .HasIndex(f => new { f.From, f.To, f.DepartureTime })
            .HasDatabaseName("indexonsearchparamters");

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "admin-role-id",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "admin-concurrency-stamp"
            },
            new IdentityRole
            {
                Id = "user-role-id",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "user-concurrency-stamp"
            }
        );

        var hasher = new PasswordHasher<ApplicationUser>();

        var adminUser = new ApplicationUser
        {
            Id = "admin-user-id",
            UserName = "adminFlyEasy@gmail.com",
            NormalizedUserName = "ADMINFLYEASY@GMAIL.COM",
            Email = "adminFlyEasy@gmail.com",
            NormalizedEmail = "ADMINFLYEASY@GMAIL.COM",
            EmailConfirmed = true,
            FullName = "admin",
            SecurityStamp = Guid.NewGuid().ToString()
        };
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "M!medo012729762879");

        builder.Entity<ApplicationUser>().HasData(adminUser);

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "admin-user-id",
                RoleId = "admin-role-id"
            }
        );

        builder.Entity<Passenger>()
            .HasOne(p => p.ApplicationUser)
            .WithMany(u => u.Passengers)
            .HasForeignKey(p => p.ApplicationUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Booking>()
            .HasOne(b => b.ApplicationUser)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Booking>()
            .HasMany(b => b.Passengers)
            .WithOne(p => p.Booking)
            .HasForeignKey(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Booking>()
            .HasOne(b => b.Flight)
            .WithMany(f => f.Bookings)
            .HasForeignKey(b => b.FlightId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Flight>()
            .HasOne(f => f.AirPlane)
            .WithMany(a => a.Flights)
            .HasForeignKey(f => f.AirPlaneId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Flight>()
           .HasMany(f => f.Bookings)
           .WithOne(a => a.Flight)
           .HasForeignKey(f => f.FlightId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
