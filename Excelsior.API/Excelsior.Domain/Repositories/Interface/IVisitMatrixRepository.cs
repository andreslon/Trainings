using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface IVisitMatrixRepository
    {
        DataModel Context { get; set; }

        IQueryable<PACS_Subject> GetSubjects(long? siteId, string userId, string search, long? procedureId, long? stepId);

        IQueryable<PACS_TimePointsList> GetTimePoints(long? siteId);

        KeyValuePair<string, WF_Sequence> GetTimePointStatus(IEnumerable<WF_Sequence> seriesList, long timePointsListId, long? procedureId, long? stepId);

        IQueryable<CERT_ImgProcedureList> GetTimePointProcedures(long timePointsListId);

        IQueryable<CERT_ImgProcedureList> GetSiteProcedures(long? siteId);

        WF_Sequence GetSeries(long subjectId, long timePointsListId, long procedureId, long? stepId);

        IQueryable<WF_Sequence> GetSeries(long subjectId, long timePointsListId);

        IQueryable<WF_Sequence> GetSeries(long subjectId);

        string GetFrameLocation(long subjectId, long timePointListId, long procedureId);
    }
}