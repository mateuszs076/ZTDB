using Microsoft.EntityFrameworkCore;
using ZTDB.SQLDatabase.Models;

namespace ZTDB.SQLDatabase
{
    public class SQLContext : DbContext
    {
        public DbSet<DataToImport> DataToImport { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<CancelCode> CancelCode { get; set; }
        public DbSet<Airline> Airline { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=Flights;User Id=sa;Password=admin123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>()
                .HasOne(i => i.OriginLocation)
                .WithMany(c => c.OriginFlights)
                .IsRequired()
                .HasForeignKey(a => a.OriginLocationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Flight>()
                .HasOne(i => i.DestinationLocation)
                .WithMany(c => c.DestinationFlights)
                .IsRequired()
                .HasForeignKey(a => a.DestinationLocationId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Flight>()
                .HasOne(i => i.Airline)
                .WithMany(c => c.Flights)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Flight>()
                .HasOne(i => i.CancelCode)
                .WithMany(c => c.Flights)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}