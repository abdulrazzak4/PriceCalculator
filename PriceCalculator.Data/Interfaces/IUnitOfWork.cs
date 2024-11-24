namespace PriceCalculator.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IResourceRepository Resources { get; }
        IMSPTierRepository MSPTiers { get; }
        IDiscountRepository Discounts { get; }
        IScopeRepository Scopes { get; }
        IDiscountResourceRepository DiscountResources { get; }
        Task SaveAsync();
    }
}
