using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Domain.Repositories;
using System.Collections.Generic;

namespace Excelsior.Business.Gateways
{
    public interface ISeriesGateway : IBaseGateway<SeriesFullDto, SeriesBaseDto, SeriesRequestDto>
    {
        ISeriesRepository Repository { get; set; }
        ResultInfo<GradingTemplateFullDto> GetGradingTemplateForSeries(long seriesId, bool isHierarchical);
        ResultInfo<IList<GradingDependencyFullDto>> GetGradingDependenciesForTemplate(long templateId);
        ResultInfo<IList<SeriesCommentFullDto>> GetComments(long id);
        ResultInfo<SeriesCommentFullDto> AddComment(long id, string userId, string value);
        ResultInfo<IList<UploadFullDto>> GetUploads(long id);
        ResultInfo<IList<MediaFullDto>> GetMedia(long id);
        ResultInfo<IList<MediaFullDto>> SetMedia(long id, IList<MediaFullDto> media);
        ResultInfo<IList<SeriesBaseDto>> CompleteStep(long id, long currentStepId, bool ignoreMultiModality, string receivedLaterality, string password);
        ResultInfo<IList<SeriesBaseDto>> Review(long id, long currentStepId, string password, string reason);
        ResultInfo<IList<SeriesBaseDto>> Assign(long id, bool ignoreRegrade = false);
        ResultInfo<IList<SeriesBaseDto>> Unassign(long id);
        ResultInfo<IList<SeriesBaseDto>> Group(SeriesGroupRequestDto request);
        ResultInfo<GradingReportFullDto> GetGradingReportForSeries(long id);
        ResultInfo<IList<GradingReportFullDto>> GetGradersGradingReports(long id);
        ResultInfo<IList<GradingReportFullDto>> GetHistoryGradingReports(long id);
        ResultInfo<GradingReportFullDto> SaveReport(long id, GradingReportFullDto report);
        ResultInfo<IList<SeriesBaseDto>> SignReport(long id, long currentStepId, bool isPass, bool needsReview, string subjectLaterality, bool ignoreMultiModality, GradingReportFullDto report, string password);
        ResultInfo<IList<AuditRecordFullDto>> GetWorkflowAuditRecords(long id);
    }
}