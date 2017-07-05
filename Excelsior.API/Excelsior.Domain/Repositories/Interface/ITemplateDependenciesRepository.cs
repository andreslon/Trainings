using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ITemplateDependenciesRepository : IEntityBaseRepository<CRF_TemplateDependency>
    {
        IQueryable<CRF_TemplateDependency> GetAll(long? answerId, bool? actionEnable);
        IQueryable<CRF_TemplateDependencySource> GetSources(long id);
        CRF_TemplateDependencySource AddSource(CRF_TemplateDependency entity, CRF_TemplateDependencySource source);
    }
}
