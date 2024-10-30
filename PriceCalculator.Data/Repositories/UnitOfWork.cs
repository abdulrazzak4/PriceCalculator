using PriceCalculator.App.Data;
using PriceCalculator.App.Interfaces;

namespace PriceCalculator.App.Repositories
{
    public class UnitOfWork(ApplicationDbContext db, IMSPTierRepository mspTiers, IResourceRepository resources) : IUnitOfWork
    {
        public IMSPTierRepository MSPTiers { get; } = mspTiers;
        public IResourceRepository Resources { get; } = resources;

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
