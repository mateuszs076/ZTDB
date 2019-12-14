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
                optionsBuilder.UseSqlServer(@"Server=.\AREK-LAPTOP;Database=Flights;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
