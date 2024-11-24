using PriceCalculator.Data.Data;
using PriceCalculator.Data.Interfaces;
using PriceCalculator.Data.Model;

namespace PriceCalculator.Data.Repositories
{
    public class DiscountRepository(ApplicationDbContext db) : Repository<Discount>(db), IDiscountRepository
    {

    }
}
