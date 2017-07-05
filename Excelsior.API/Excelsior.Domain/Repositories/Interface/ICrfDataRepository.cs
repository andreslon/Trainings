using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ICrfDataRepository : IEntityBaseRepository<CRF_Datum>
    {
        IQueryable<CRF_Datum> GetAll(long? seriesId, long? subjectId, long? timePointId, long? procedureId, bool? isActive);
        IQueryable<CRF_DataResult> GetResults(long id);
        CRF_DataResult AddResult(CRF_Datum entity, CRF_DataResult result);
        CRF_TemplateAnswer AddResultAnswer(CRF_DataResult entity, CRF_TemplateAnswer answer);
    }
}