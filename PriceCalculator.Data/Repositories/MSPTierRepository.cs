using PriceCalculator.App.Data;
using PriceCalculator.App.Model;
using PriceCalculator.App.Interfaces;

namespace PriceCalculator.App.Repositories
{
    public class MSPTierRepository(ApplicationDbContext db): Repository<MSPTier>(db) , IMSPTierRepository
    {
    }
}
