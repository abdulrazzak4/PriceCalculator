using PriceCalculator.Data.Data;
using PriceCalculator.Data.Interfaces;
using PriceCalculator.Data.Model;

namespace PriceCalculator.Data.Repositories
{
    public class ScopeRepository(ApplicationDbContext db) : Repository<Scope>(db), IScopeRepository
    {

    }
}
