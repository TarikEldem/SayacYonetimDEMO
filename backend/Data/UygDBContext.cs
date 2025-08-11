using SayacApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace SayacApi.Data
{
    public class UygDbContext : DbContext
    {
        public UygDbContext(DbContextOptions<UygDbContext> options)
            : base(options) { }

        public DbSet<Sayac> Sayaclar { get; set; }
    }
}