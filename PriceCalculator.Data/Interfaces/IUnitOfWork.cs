namespace PriceCalculator.App.Interfaces
{
    public interface IUnitOfWork
    {
        IResourceRepository Resources { get; }
        IMSPTierRepository MSPTiers { get; }
        Task SaveAsync();
    }
}
