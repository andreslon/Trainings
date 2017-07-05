using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public interface ISeriesRepository : IEntityBaseRepository<WF_Sequence>
    {
        IQueryable<WF_Sequence> GetAll(string aspUserId, long studyId, string step, long? categoryId, string dataType, long? timePointListId, long? procedureId, long? subjectId, long? siteId, string assignedTo, long? seriesGroupId, long? subjectGroupId, long? subjectCohortId, string search, string sort);
        string GetSegmentationStatus(long seriesId);
        int GetTotalCertifiedUsers(long? trialId, long? userId, long? procedureId);
        int GetTotalCertifiedEquipment(long? trialId, long? equipmentId, long? procedureId);
        int GetTotalComments(long? seriesId);
        int GetTotalUploads(long? seriesId);
        int GetTotalMedia(long? seriesId);
        int GetTotalQueriesPending(WF_Sequence entity);
        int GetTotalQueriesFlagged(WF_Sequence entity, CONTACT_User user);
        IQueryable<PACS_SeriesComment> GetComments(long id);
        PACS_SeriesComment AddComment(long id, string userId, string value);
        IQueryable<UPLD_UploadInfo> GetUploads(long id);
        IQueryable<PACS_RawDatum> GetMedia(long id);
        PACS_RawDatum AddMedia(PACS_Series entity, PACS_RawDatum media);
        IQueryable<AUDIT_Record> GetWorkflowAuditRecords(long id);
    }
}
