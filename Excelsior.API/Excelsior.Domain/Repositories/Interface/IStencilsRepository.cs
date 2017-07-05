using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IStencilsRepository : IEntityBaseRepository<MEA_Stencil>
    {
        IQueryable<MEA_Stencil> GetAll(long? trialId);

    }
}
