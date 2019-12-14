using Microsoft.EntityFrameworkCore;
using System;
using ZTDB.SQLDatabase.Models;

namespace ZTDB.SQLDatabase
{
    public class SQLContext : DbContext
    {
        public DbSet<DataToImport> DataToImport { get; set; }

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
