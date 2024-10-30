using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using PriceCalculator.App.Model;

namespace PriceCalculator.App.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<MSPTier> MSPTiers { get; set; }

    }
}
