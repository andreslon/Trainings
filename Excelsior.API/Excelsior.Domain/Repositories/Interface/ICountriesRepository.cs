using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ICountriesRepository : IEntityBaseRepository<CONTACT_Country>
    {
        IQueryable<CONTACT_Country> GetAll(string search);
    }
}
