using PriceCalculator.App.Data;
using PriceCalculator.App.Interfaces;
using PriceCalculator.App.Model;

namespace PriceCalculator.App.Repositories
{
    public class ResourceRepository(ApplicationDbContext db) : Repository<Resource>(db), IResourceRepository
    {

    }
}
