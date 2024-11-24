using Microsoft.EntityFrameworkCore;
using PriceCalculator.Data.Model;

namespace PriceCalculator.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiscountResource>().HasKey(ds => new
            {
                ds.DiscountId,
                ds.ResourceId
            });

            modelBuilder.Entity<DiscountResource>().HasOne(m => m.Discount).WithMany(am => am.DiscountResources).HasForeignKey(m => m.DiscountId);
            modelBuilder.Entity<DiscountResource>().HasOne(m => m.Resource).WithMany(am => am.DiscountResources).HasForeignKey(m => m.ResourceId);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<MSPTier> MSPTiers { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountResource> DiscountResources { get; set; }


    }
}
