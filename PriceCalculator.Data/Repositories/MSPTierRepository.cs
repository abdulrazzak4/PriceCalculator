using PriceCalculator.Data.Data;
using PriceCalculator.Data.Model;
using PriceCalculator.Data.Interfaces;

namespace PriceCalculator.Data.Repositories
{
    public class MSPTierRepository(ApplicationDbContext db): Repository<MSPTier>(db) , IMSPTierRepository
    {
    }
}
