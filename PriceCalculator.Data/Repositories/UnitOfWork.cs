using PriceCalculator.Data.Data;
using PriceCalculator.Data.Interfaces;

namespace PriceCalculator.Data.Repositories
{
    public class UnitOfWork(ApplicationDbContext db, IMSPTierRepository mspTiers, IResourceRepository resources, IDiscountRepository discounts, IScopeRepository scopes, IDiscountResourceRepository discountResources) : IUnitOfWork
    {
        public IMSPTierRepository MSPTiers { get; } = mspTiers;
        public IResourceRepository Resources { get; } = resources;
        public IDiscountRepository Discounts { get; } = discounts;
        public IScopeRepository Scopes { get; } = scopes;
        public IDiscountResourceRepository DiscountResources { get; } = discountResources;

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
