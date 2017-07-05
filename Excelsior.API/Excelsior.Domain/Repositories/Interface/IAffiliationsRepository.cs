using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IAffiliationsRepository : IEntityBaseRepository<CONTACT_Affiliation>
    {
        IQueryable<CONTACT_Affiliation> GetAll(string userId, bool? isActive, string search);
    }
}
