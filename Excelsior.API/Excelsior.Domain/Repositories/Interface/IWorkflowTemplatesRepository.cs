using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IWorkflowTemplatesRepository : IEntityBaseRepository<WF_Template>
    {
        IQueryable<WF_Template> GetAll(long? trialId, bool? isActive, bool? isLocked, string search);
        IQueryable<WF_TempStep> GetSteps(WF_Template entity);
        WF_TempStep AddStep(WF_Template entity, WF_TempStep step);
        WF_Template Clone(WF_Template entity, long? trialId);
    }
}
