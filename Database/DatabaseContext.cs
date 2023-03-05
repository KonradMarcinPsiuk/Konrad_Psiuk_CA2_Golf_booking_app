using Microsoft.EntityFrameworkCore;


using System.ComponentModel.DataAnnotations;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<Tee> Tees { get; set; }
        public DbSet<TeeBooking> TeeBookings { get; set; }

        public string DatabaseFilepath { get; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabaseFilepath}")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DatabaseContext()
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            DatabaseFilepath = Path.Join(folder, "golfDb.db");
        }
        
        
    }

    public class Golfer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }  = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string EmailAddress { get; set; } = null!;
        [Required]
        public string Sex { get; set; } = null!;
        public int Handicap { get; set; }
        
        public List<TeeBooking> TeeBookings { get; set; }
    
    }

    public class Tee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string nNme { get; set; }
    }

    public class TeeBooking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime BookingTime { get; set; }
        public List<Golfer> Golfers { get; set; }
        [Required]
        public Tee BookedTee { get; set; }
    }
}
