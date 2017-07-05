using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ITemplatesRepository : IEntityBaseRepository<CRF_Template>
    {
        IQueryable<CRF_Template> GetAll(long? trialId, long? timePointId, long? procedureId, bool? isActive, bool? isLocked, string search);
        IQueryable<CRF_TemplateGroup> GetGroups(long id);
        CRF_TemplateGroup AddGroup(CRF_Template entity, CRF_TemplateGroup group);
        CRF_Template Clone(CRF_Template entity, long? trialId);
    }
}
