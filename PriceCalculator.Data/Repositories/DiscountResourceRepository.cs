using PriceCalculator.Data.Data;
using PriceCalculator.Data.Interfaces;
using PriceCalculator.Data.Model;

namespace PriceCalculator.Data.Repositories
{
    public class DiscountResourceRepository(ApplicationDbContext db) : Repository<DiscountResource>(db), IDiscountResourceRepository
    {

    }
}
