using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingLot.Models;

namespace ParkingLot.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Spot> Spots { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        
    }
}
