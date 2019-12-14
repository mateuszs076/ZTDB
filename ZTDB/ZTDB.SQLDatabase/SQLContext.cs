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
        }
    }
}