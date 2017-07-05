using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IAnimalSpeciesRepository : IEntityBaseRepository<CFG_AnimalSpecy>
    {
        IQueryable<CFG_AnimalSpecy> GetAll(string search);
    }
}
