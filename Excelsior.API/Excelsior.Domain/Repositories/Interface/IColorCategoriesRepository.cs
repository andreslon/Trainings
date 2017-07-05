using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IColorCategoriesRepository : IEntityBaseRepository<WF_CategoryFlag>
    {
        IQueryable<WF_CategoryFlag> GetAll(string search);
    }
}
