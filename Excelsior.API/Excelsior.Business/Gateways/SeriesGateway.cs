using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using Excelsior.Infrastructure.Interfaces;
using Excelsior.Infrastructure.Utilities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.Gateways
{
    public class SeriesGateway : ISeriesGateway
    {
        public ISeriesRepository Repository { get; set; }
        public IGradingTemplatesRepository GradingTemplatesRepository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public IVisitsRepository VisitsRepository { get; set; }
        public IMediaRepository MediaRepository { get; set; }
        private IAuthUserRepository AuthRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }

        public SeriesGateway(ISeriesRepository repository, IGradingTemplatesRepository gtRepository, IAuditRecordsRepository auditRecordsRepository, IVisitsRepository visitsRepository, IMediaRepository mediaRepository, IAuthUserRepository authRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            GradingTemplatesRepository = gtRepository;
            AuditRecordsRepository = auditRecordsRepository;
            VisitsRepository = visitsRepository;
            MediaRepository = mediaRepository;
            AuthRepository = authRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        private void SetDtoValues(SeriesBaseDto dto, WF_Sequence entity, CONTACT_User user, string assignedTo = null)
        {
            dto.TotalComments = Repository.GetTotalComments(entity.SeriesID);
            dto.TotalUploads = Repository.GetTotalUploads(entity.SeriesID);
            dto.TotalMedia = Repository.GetTotalMedia(entity.SeriesID);
            dto.TotalQueriesPending = Repository.GetTotalQueriesPending(entity);
            dto.TotalQueriesFlagged = Repository.GetTotalQueriesFlagged(entity, user);
            dto.IsTechnicianCerified = Repository.GetTotalCertifiedUsers(entity.PACSTimePoint?.PACSSubject?.PACSSite?.PACSTrial?.TrialID, entity.CONTACTUser?.UserID, entity.PACSTPProcList?.ImgProcedureID) > 0;
            dto.IsEquipmentCerified = Repository.GetTotalCertifiedEquipment(entity.PACSTimePoint?.PACSSubject?.PACSSite?.PACSTrial?.TrialID, entity.CONTACTEquipment?.EquipmentID, entity.PACSTPProcList?.ImgProcedureID) > 0;
            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                if (assignedTo.ToLower() == "me" || assignedTo.ToLower() == "any")
                {
                    if (entity.PACSTPProcList.CERTImgProcedureList.PACSDataType.DataType == "OPT")
                        dto.SegmentationStatus = Repository.GetSegmentationStatus(entity.SeriesID);
                }
            }
        }

        private SeriesBaseDto GenerateSeriesBaseDto(WF_Sequence entity, CONTACT_User user)
        {
            var dto = new SeriesBaseDto(entity);
            SetDtoValues(dto, entity, user);
            return dto;
        }

        public ResultInfo<IList<SeriesBaseDto>> GetAll(SeriesRequestDto request)
        {
            var result = new ResultInfo<IList<SeriesBaseDto>>();
            try
            {
                if (request.StudyId <= 0)
                {
                    throw new Exception("studyId is required.");
                }

                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var seriesResult = new List<SeriesBaseDto>();
                var series = Repository.GetAll(request.UserId, request.StudyId, request.Step, request.CategoryId, request.DataType, request.TimePointListId, request.ProcedureId, request.SubjectId, request.SiteId, request.AssignedTo, request.SeriesGroupId, request.SubjectGroupId, request.SubjectCohortId, request.Search, request.Sort);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return series.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(series, result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new SeriesBaseDto(entity);
                        SetDtoValues(dto, entity, user, request.AssignedTo);
                        seriesResult.Add(dto);
                    }
                }

                result.Result = seriesResult;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<SeriesFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<SeriesFullDto>();
            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                var serie = Repository.Context.WF_Sequences.FirstOrDefault(x => x.SeriesID == id);
                if (serie == null)
                    throw new Exception("Series not found");

                var studyId = serie.PACSTPProcList.PACSTimePointsList.TrialID;
                PACS_Trial study = null;
                switch (user.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                    case "project manager":
                        study = Repository.Context.PACS_Trials.FirstOrDefault(t => t.TrialID == studyId);
                        break;
                    default:
                        study = Repository.Context.CONTACT_UserTrials.FirstOrDefault(t => t.TrialID == studyId && t.UserID == user.UserID)?.PACSTrial;
                        break;
                }

                if (study == null)
                    throw new UnauthorizedAccessException("Access denied");

                var series = Repository.GetAll(aspUserId.ToString(), study.TrialID, null, null, null, null, null, null, null, null, null, null, null, null, null);

                var entity = series.FirstOrDefault(x => x.SeriesID == id);
                //var entity = Repository.GetSingle(x => x.SeriesID == id);

                if (entity != null)
                {
                    var dto = new SeriesFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                    throw new UnauthorizedAccessException("Access denied");
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        //public ResultInfo<SeriesFullDto> GetSingle(long id)
        //{
        //    var result = new ResultInfo<SeriesFullDto>();
        //    try
        //    {
        //        var entity = Repository.GetSingle(x => x.SeriesID == id);

        //        if (entity != null)
        //        {
        //            var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
        //            var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

        //            var dto = new SeriesFullDto(entity);
        //            SetDtoValues(dto, entity, user);
        //            result.Result = dto;
        //            result.IsSuccess = true;
        //        }
        //        else
        //        {
        //            throw new Exception("Series not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Result = null;
        //        result.Exception = ex.Message;
        //        result.IsSuccess = false;
        //        result.Message = "Exception";
        //    }
        //    return result;
        //}

        public ResultInfo<SeriesFullDto> Add(SeriesFullDto request)
        {
            var result = new ResultInfo<SeriesFullDto>();
            try
            {
                var schedule = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.Context.PACS_TPProcLists.FirstOrDefault(x => x.TPProcID == request.ScheduleId);
                });

                if (schedule == null)
                {
                    throw new Exception("Schedule not found.");
                }

                if (schedule.WFTemplate == null)
                {
                    throw new Exception("Workflow not configured.");
                }

                var wfTempStep = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    if (!string.IsNullOrEmpty(request.WorkflowStepName))
                        return Repository.Context.WF_TempSteps.Where(x => !x.ShouldSkip && x.WFTemplateID == schedule.WFTemplateID && x.WFStepList.WFStepListDes.ToLower() == request.WorkflowStepName.ToLower()).OrderBy(x => x.WFStepOrder).FirstOrDefault();
                    else
                        return Repository.Context.WF_TempSteps.Where(x => !x.ShouldSkip && x.WFTemplateID == schedule.WFTemplateID).OrderBy(x => x.WFStepOrder).FirstOrDefault();
                });

                if (wfTempStep == null)
                {
                    throw new Exception("Workflow step not found.");
                }

                var visit = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.Context.PACS_TimePoints.FirstOrDefault(x => x.SubjectID == request.SubjectId
                        && x.TimePointsListID == schedule.TimePointsListID);
                });

                if (visit == null)
                {
                    //create new visit
                    visit = new PACS_TimePoint()
                    {
                        SubjectID = request.SubjectId,
                        TimePointsListID = schedule.TimePointsListID,
                    };
                    VisitsRepository.Add(visit);
                }

                var entity = request.ToEntity();
                entity.TPProcListID = schedule.TPProcID;
                entity.PACSTPProcList = schedule;
                if (visit.TimePointsID > 0)
                    entity.TimePointsID = visit.TimePointsID;
                entity.PACSTimePoint = visit;
                if (entity.WFTempStepID == null)
                    entity.WFTempStepID = wfTempStep.WFTempStepID;
                entity.LastStepCompletionDate = DateTime.UtcNow;

                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                //CHECK if series exists
                var existing = Repository.GetSingle(x => x.IsActive && x.TimePointsID == visit.TimePointsID &&
                    x.TPProcListID == schedule.TPProcID);

                if (existing != null)
                {
                    //series exists
                    var dto = new SeriesFullDto(existing);
                    SetDtoValues(dto, existing, user);
                    result.Result = dto;
                    result.Message = "Series already exists.";
                }
                else
                {
                    Repository.Add(entity);
                    var record = AuditRecordsRepository.AddRecord("SeriesCreated");
                    record.PACSSeries = entity;
                    record.TrialID = schedule.PACSTimePointsList.TrialID;
                    Repository.Commit();
                    Repository.Refresh(entity);
                    Repository.Refresh(visit);
                    Repository.Refresh(schedule);

                    //check multimodality
                    //Get procedures with same configuration
                    var procs = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return Repository.Context.PACS_TPProcLists.Count(x => x.TimePointsListID == schedule.TimePointsListID
                            && x.GTemplateID == schedule.GTemplateID && x.WFTemplateID == schedule.WFTemplateID && x.CRFTemplateID == schedule.CRFTemplateID);
                    });

                    if (procs > 1)
                    {
                        PACS_SeriesGroup group = null;
                        if (entity.PACSTimePoint.TimePointsID > 0)
                        {
                            group = Repository.Context.PACS_SeriesGroups.FirstOrDefault(x => x.TimePointsID == visit.TimePointsID
                                && x.GTemplateID == schedule.GTemplateID
                                && x.WFTemplateID == schedule.WFTemplateID
                                && x.CRFTemplateID == schedule.CRFTemplateID);
                        }

                        if (group == null)
                        {
                            group = new PACS_SeriesGroup()
                            {
                                TimePointsID = visit.TimePointsID,
                                GTemplateID = schedule.GTemplateID,
                                WFTemplateID = schedule.WFTemplateID,
                                CRFTemplateID = schedule.CRFTemplateID
                            };
                        }

                        if (group.SeriesGroupID > 0)
                            entity.SeriesGroupID = group.SeriesGroupID;
                        else
                            entity.PACSSeriesGroup = group;

                        Repository.Commit();
                        Repository.Refresh(entity);
                    }

                    var dto = new SeriesFullDto(entity);
                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.Message = "Series added successfully.";
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<SeriesFullDto> Update(SeriesFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<SeriesFullDto>();
            try
            {
                if (!string.IsNullOrEmpty(password))
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                    //Validate Password
                    if (!UserHelper.ValidatePassword(aspUser, password))
                        throw new Exception("Invalid password.");
                }

                var entity = Repository.GetSingle(x => x.SeriesID == request.Id);
                if (entity != null)
                {
                    var oldDto = new SeriesFullDto(entity);
                    entity = request.ToEntity(entity, fields);
                    var newDto = new SeriesFullDto(entity);
                    var changes = ChangeSetHelper.GetPropertiesChangeInfo(newDto, oldDto, fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("SeriesEdited");
                    record.SeriesID = request.Id;
                    record.TrialID = entity.PACSTimePoint?.PACSSubject?.PACSSite?.PACSTrial?.TrialID;
                    if (!string.IsNullOrEmpty(reason))
                        record.ReasonForChange = reason;
                    if (changes.Count > 0)
                        record.DetailsXML = ChangeSetHelper.ToXML(ChangeSetHelper.CreateEntityChangeList("Series", "Update", changes));
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new SeriesFullDto(entity);

                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    SetDtoValues(dto, entity, user);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<bool> Delete(long id)
        {
            //Perform input validation
            //---- 
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    var record = AuditRecordsRepository.AddRecord("SeriesRemoved");
                    record.SeriesID = id;
                    record.TrialID = entity.PACSTimePoint?.PACSSubject?.PACSSite?.PACSTrial?.TrialID;
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        #region Uploads

        public ResultInfo<IList<UploadFullDto>> GetUploads(long id)
        {
            var result = new ResultInfo<IList<UploadFullDto>>();
            try
            {
                var entities = Repository.GetUploads(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new UploadFullDto(x, new SeriesBaseDto())).ToList();
                });
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        #endregion

        #region Media 

        public ResultInfo<IList<MediaFullDto>> GetMedia(long id)
        {
            var result = new ResultInfo<IList<MediaFullDto>>();
            try
            {
                var entities = Repository.GetMedia(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new MediaFullDto(x, new SeriesBaseDto())).ToList();
                });
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        public ResultInfo<IList<MediaFullDto>> SetMedia(long id, IList<MediaFullDto> media)
        {
            var result = new ResultInfo<IList<MediaFullDto>>();
            try
            {
                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    AttachMedia(entity, media);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var mediaList = new List<MediaFullDto>();
                    foreach (var mediaEntity in entity.PACS_RawData.Where(x => x.IsActive))
                    {
                        var dto = new MediaFullDto(mediaEntity);
                        if (dto.DicomOPT != null)
                        {
                            dto.SegmentationStatus = MediaRepository.GetSegmentationStatus(mediaEntity.RawDataID);
                        }
                        mediaList.Add(dto);
                    }
                    result.Result = mediaList;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        private void AttachMedia(PACS_Series entity, IList<MediaFullDto> media)
        {
            var mediaEntities = RefreshOrRemoveExistingMedia(entity, media);
            for (var i = 0; i < media.Count; i++)
            {
                var mediaEntity = Repository.AddMedia(entity, mediaEntities[i]);
            }
        }

        private IList<PACS_RawDatum> RefreshOrRemoveExistingMedia(PACS_Series entity, IList<MediaFullDto> media)
        {
            var entityMedia = entity.PACS_RawData.ToList();
            foreach (var item in entityMedia)
            {
                if (!media.Any(x => x.Id == item.RawDataID))
                    Repository.Context.Delete(item);
            }

            var mediaEntities = new List<PACS_RawDatum>();
            foreach (var item in media)
            {
                var entityMediaItem = entityMedia.FirstOrDefault(x => x.RawDataID == item.Id);
                if (entityMedia != null)
                {
                    mediaEntities.Add(item.ToEntity(entityMediaItem));
                }
                else
                {
                    mediaEntities.Add(item.ToEntity());
                }
            }

            return mediaEntities;
        }

        #endregion

        #region Grading

        public ResultInfo<GradingTemplateFullDto> GetGradingTemplateForSeries(long id, bool isHierarchical)
        {
            var result = new ResultInfo<GradingTemplateFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.SeriesID == id);

                if (entity == null)
                    throw new Exception("Series not found");

                var step = entity.WFTempStep.WFStepList.WFStepListDes.ToLower();

                GRD_GradingTemplate template = null;

                if (step == "completed")
                {
                    var report = Repository.Context.GRD_Reports.LastOrDefault(x => x.IsActive && x.IsPrimaryResult && x.IsSigned && x.SeriesID == entity.SeriesID);
                    template = report?.GRDGradingTemplate;
                }
                else
                    template = entity.PACSTPProcList?.GRDGradingTemplate;

                if (template != null)
                {
                    var templateResult = new GradingTemplateFullDto(template);
                    GradingHelper.fillTemplateResult(GradingTemplatesRepository, template, templateResult, isHierarchical);
                    result.Result = templateResult;
                }
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<IList<GradingDependencyFullDto>> GetGradingDependenciesForTemplate(long templateId)
        {
            var result = new ResultInfo<IList<GradingDependencyFullDto>>();
            try
            {
                var dependencies = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return GradingTemplatesRepository.GetGradingDependenciesForTemplate(templateId)
                    .Select(x => new GradingDependencyFullDto()
                    {
                        Id = x.GDependencyID,
                        IsActionEnable = x.ActionEnable,
                        SourceAnswerID = x.GSourceAnswerID,
                        TargetAnswerID = x.GTargetAnswerID,
                        TargetQuestionID = x.GTargetQuestionID,
                    }).ToList();
                });


                result.Result = dependencies;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<GradingReportFullDto> GetGradingReportForSeries(long id)
        {
            var result = new ResultInfo<GradingReportFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    var step = entity.WFTempStep.WFStepList.WFStepListDes.ToLower();
                    GRD_Report reportEntity = null;

                    //get user
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    switch (step)
                    {
                        case "grade":
                            reportEntity = Repository.Context.GRD_Reports.LastOrDefault(x => x.IsActive && !x.IsPrimaryResult && !x.IsSigned && x.SeriesID == entity.SeriesID && x.PerformedBy == user.UserID);
                            break;
                        case "verify":
                            reportEntity = Repository.Context.GRD_Reports.LastOrDefault(x => x.IsActive && x.IsPrimaryResult && !x.IsSigned && x.SeriesID == entity.SeriesID && x.PerformedBy == user.UserID);
                            if (reportEntity == null)
                                reportEntity = Repository.Context.GRD_Reports.LastOrDefault(x => x.IsActive && x.IsPrimaryResult && x.IsSigned && x.SeriesID == entity.SeriesID);
                            if (reportEntity == null)
                                reportEntity = Repository.Context.GRD_Reports.FirstOrDefault(x => x.IsActive && !x.IsPrimaryResult && x.IsSigned && x.SeriesID == entity.SeriesID);
                            break;
                        case "completed":
                            reportEntity = Repository.Context.GRD_Reports.LastOrDefault(x => x.IsActive && x.IsPrimaryResult && x.IsSigned && x.SeriesID == entity.SeriesID);
                            break;
                    }

                    if (reportEntity != null)
                        result.Result = new GradingReportFullDto(reportEntity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<IList<GradingReportFullDto>> GetGradersGradingReports(long id)
        {
            var result = new ResultInfo<IList<GradingReportFullDto>>();
            try
            {
                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    var reportsEntities = Repository.Context.GRD_Reports.Where(x => x.IsActive && !x.IsPrimaryResult && x.IsSigned && x.SeriesID == entity.SeriesID);

                    result.Result = reportsEntities.Select(x => new GradingReportFullDto(x, null)).ToList();
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<IList<GradingReportFullDto>> GetHistoryGradingReports(long id)
        {
            var result = new ResultInfo<IList<GradingReportFullDto>>();
            try
            {
                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    //procedure
                    var procedureId = entity.PACSTPProcList.CERTImgProcedureList.ImgProcedureID;

                    //subject
                    var subjectId = entity.PACSTimePoint.SubjectID;

                    var reportsEntities = Repository.Context.GRD_Reports.Where(x => x.PACSSeries.IsActive && x.PACSSeries.PACSTimePoint.SubjectID == subjectId && x.PACSSeries.PACSTPProcList.ImgProcedureID == procedureId
                        && x.IsActive && x.IsPrimaryResult && x.IsSigned).ToList();

                    var reportsFiltered = reportsEntities.GroupBy(x => x.SeriesID, (key, x) => x.LastOrDefault());
                    result.Result = reportsFiltered.Select(x => new GradingReportFullDto(x, null)).ToList();
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<GradingReportFullDto> SaveReport(long id, GradingReportFullDto report)
        {
            var result = new ResultInfo<GradingReportFullDto>();

            try
            {
                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    //get user
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    var isVerify = entity.WFTempStep.WFStepList.WFStepListDes.ToLower() == "verify";

                    //Prepare Entities
                    var reportEntity = report.ToEntity();
                    var reportUserId = reportEntity.PerformedBy;
                    reportEntity.PerformedBy = user.UserID;
                    reportEntity.PerformedDate = DateTime.UtcNow;
                    reportEntity.PerformedTime = DateTime.UtcNow;
                    reportEntity.SeriesID = id;
                    reportEntity.IsActive = true;
                    reportEntity.IsSigned = false;
                    reportEntity.IsPrimaryResult = isVerify;
                    Repository.Context.Add(reportEntity);
                    Repository.Context.FlushChanges();

                    PrepareReportModels(report, reportEntity);

                    Repository.Context.SaveChanges();

                    Repository.Refresh(entity);
                    Repository.Refresh(reportEntity);

                    RemoveUnsavedAttachments(id, user.UserID);
                    CreateReportCopies(reportEntity, false);

                    result.Result = new GradingReportFullDto(reportEntity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        public ResultInfo<IList<SeriesBaseDto>> SignReport(long id, long currentStepId, bool isPass, bool needsReview, string subjectLaterality, bool ignoreMultiModality, GradingReportFullDto report, string password)
        {
            var result = new ResultInfo<IList<SeriesBaseDto>>();

            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                //Validate Password
                if (!UserHelper.ValidatePassword(aspUser, password))
                    throw new Exception("Invalid password.");

                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    if (entity.WFTempStepID != currentStepId)
                    {
                        throw new Exception("Data conflict.");
                    }

                    //Removed: MultiModality validation not needed at grade or verify step only in checkin
                    //if (!ignoreMultiModality && entity.SeriesGroupID != null)
                    //{
                    //    var entities = GetSeriesEntitiesInGroup(entity);
                    //    ValidateMultiModality(entity, entities);
                    //}

                    //get user
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    var isVerify = entity.WFTempStep.WFStepList.WFStepListDes.ToLower() == "verify";
                    var isGrade = entity.WFTempStep.WFStepList.WFStepListDes.ToLower() == "grade";

                    //Prepare Entities
                    var reportEntity = report.ToEntity();
                    var reportUserId = reportEntity.PerformedBy;
                    reportEntity.PerformedBy = user.UserID;
                    reportEntity.PerformedDate = DateTime.UtcNow;
                    reportEntity.PerformedTime = DateTime.UtcNow;
                    reportEntity.SeriesID = id;
                    reportEntity.IsActive = true;
                    reportEntity.IsSigned = true;
                    reportEntity.IsPrimaryResult = isVerify;
                    Repository.Context.Add(reportEntity);
                    Repository.Context.FlushChanges();

                    PrepareReportModels(report, reportEntity);

                    if (entity.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint)
                    {
                        if (isVerify)
                            entity.PACSTimePoint.PACSSubject.IsSubjectRejected = !isPass;

                        if (isPass && !string.IsNullOrEmpty(subjectLaterality))
                            entity.PACSTimePoint.PACSSubject.Laterality = subjectLaterality;
                    }

                    Repository.Context.SaveChanges();

                    Repository.Refresh(entity);
                    Repository.Refresh(reportEntity);

                    RemoveUnsavedAttachments(id, user.UserID);
                    CreateReportCopies(reportEntity);

                    IQueryable<WF_Sequence> sl;

                    if (entity.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint)
                    {
                        //Clone Series and move to next step
                        sl = GoToNextStep(id, false, isPass && entity.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsEligibilityCloningEnabled, user);
                    }
                    else
                    {
                        var needsRegrade = false;
                        if (isGrade && !needsReview)
                        {
                            var rc = Repository.Context.GRD_Reports.Count(gr => gr.IsActive && gr.SeriesID == id && gr.IsSigned && !gr.IsPrimaryResult);
                            var isSeriesReGraded = (rc > 1);

                            if (!isSeriesReGraded)
                            {
                                var pACSTPProcList = entity.PACSTPProcList;
                                var seriesSigned = (pACSTPProcList.CounterSeriesSigned == null) ? 0 : pACSTPProcList.CounterSeriesSigned.Value;
                                var percentForReview = (pACSTPProcList.PercentSeriesForReview == null) ? 0 : pACSTPProcList.PercentSeriesForReview.Value;
                                var seriesForReview = (pACSTPProcList.CounterSeriesForReview == null) ? 0 : pACSTPProcList.CounterSeriesForReview.Value;

                                seriesSigned++;
                                if (percentForReview > 0)
                                {
                                    double d = ((double)percentForReview / 100);
                                    var count2 = (int)((seriesSigned * d));
                                    if (seriesForReview < count2)
                                    {
                                        needsRegrade = true;
                                        seriesForReview++;
                                    }
                                }
                                else
                                {
                                    needsRegrade = false;
                                }

                                pACSTPProcList.CounterSeriesSigned = seriesSigned;
                                pACSTPProcList.CounterSeriesForReview = seriesForReview;

                                Repository.Context.SaveChanges();
                            }
                        }

                        sl = FinalizeSign(id, needsReview, needsRegrade, user);
                    }

                    List<SeriesBaseDto> seriesResult = new List<SeriesBaseDto>();
                    foreach (var s in sl)
                    {
                        var dto = new SeriesBaseDto(s);
                        SetDtoValues(dto, s, user);
                        seriesResult.Add(dto);
                    }

                    result.Result = seriesResult;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }

            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        private void PrepareReportModels(GradingReportFullDto report, GRD_Report reportEntity)
        {
            var attmEntities = report.Attachments.Select(x => x.ToEntity()).ToList();
            var meaEntities = report.Measurements.Select(x => x.ToEntity()).ToList();
            var resultEntities = report.Results.Select(x => x.ToEntity()).ToList();

            var meaRef = new Dictionary<long, MEA_Measurement>();
            foreach (var meaEntity in meaEntities)
            {
                var mId = meaEntity.MeasurementID;
                meaEntity.MeasurementID = 0;
                Repository.Context.Add(meaEntity);
                meaEntity.GRDReport = reportEntity;

                meaRef.Add(mId, meaEntity);
            }
            Repository.Context.FlushChanges();

            foreach (var resultEntity in resultEntities)
            {
                resultEntity.GRDReportResultID = 0;
                MEA_Measurement meaEntity = null;
                if (resultEntity.GAnswerMeasurement != null)
                {
                    meaEntity = meaRef[resultEntity.GAnswerMeasurement.Value];
                    resultEntity.GAnswerMeasurement = null;
                }
                resultEntity.MEAMeasurement = null;
                Repository.Context.Add(resultEntity);
                resultEntity.GRDReport = reportEntity;
                if (meaEntity != null)
                    resultEntity.MEAMeasurement = meaEntity;
            }
            Repository.Context.FlushChanges();

            foreach (var attmEntity in attmEntities)
            {
                var nAttmEntity = attmEntity;
                if (attmEntity.SeriesAttachmentID > 0)
                    nAttmEntity = CreateAttachmentCopy(attmEntity);
                Repository.Context.Add(nAttmEntity);
                nAttmEntity.GRDReport = reportEntity;
            }
            Repository.Context.FlushChanges();
        }

        private IQueryable<WF_Sequence> FinalizeSign(long id, bool needsReview, bool needsRegrade, CONTACT_User user)
        {
            if (needsReview)
                return GoToNextVerifyStep(id, user);
            else
            {
                if (needsRegrade)
                    return GoToNextGradeStep(id, user);
                else
                    return GoToNextStep(id, false, false, user);
            }
        }

        private void RemoveUnsavedAttachments(long seriesID, long userID)
        {
            var ua = Repository.Context.PACS_SeriesAttachments.Where(x => x.GReportID == null && x.SeriesID == seriesID && x.UserID == userID);
            foreach (var a in ua)
            {
                Repository.Context.Delete(a);
            }

            Repository.Context.SaveChanges();
        }

        private void RemoveReportDuplicates(long seriesID, long userId)
        {
            var series = Repository.Context.WF_Sequences.Single(s => s.SeriesID == seriesID);
            var seriesInGroup = Repository.Context.WF_Sequences.Where(s => s.SeriesGroupID != null && s.SeriesGroupID == series.SeriesGroupID).ToList();

            if (seriesInGroup.Count == 0)
                seriesInGroup.Add(series);

            foreach (var s in seriesInGroup)
            {
                var reports = Repository.Context.GRD_Reports.Where(r => r.SeriesID == s.SeriesID && r.PerformedBy == userId && r.IsActive).OrderBy(r => r.PerformedDate).ToList();

                if (reports.Count > 0)
                {
                    var lastPrimaryReport = reports.LastOrDefault(r => r.IsPrimaryResult);
                    if (lastPrimaryReport != null)
                        reports.Remove(lastPrimaryReport);
                    var lastReport = reports.LastOrDefault(r => !r.IsPrimaryResult);
                    if (lastReport != null)
                        reports.Remove(lastReport);

                    foreach (var r in reports)
                    {
                        r.IsActive = false;
                    }
                }
            }

            if (Repository.Context.HasChanges)
                Repository.Context.SaveChanges();
        }

        private PACS_SeriesAttachment CreateAttachmentCopy(PACS_SeriesAttachment attm)
        {
            var attmCopy = new PACS_SeriesAttachment()
            {
                IsActive = true,
                FileLocation = attm.FileLocation,
                UserID = attm.UserID,
                DateCreated = attm.DateCreated,
                Laterality = attm.Laterality,
                SeriesID = attm.SeriesID,
                StatusID = attm.StatusID
            };
            return attmCopy;
        }

        private MEA_Measurement CreateMeasurementCopy(MEA_Measurement mea)
        {
            MEA_Measurement meaCopy = null;
            if (mea is MEA_Freehand)
            {
                var f = mea as MEA_Freehand;
                meaCopy = new MEA_Freehand()
                {
                    Color = f.Color,
                    Tag = f.Tag,
                    MeasurementXML = f.MeasurementXML
                };
            }
            else if (mea is MEA_Distance)
            {
                var d = mea as MEA_Distance;
                meaCopy = new MEA_Distance()
                {
                    StartX = d.StartX,
                    StartY = d.StartY,
                    EndX = d.EndX,
                    EndY = d.EndY,
                    DistanceMm = d.DistanceMm
                };
            }
            else if (mea is MEA_Area)
            {
                var a = mea as MEA_Area;
                meaCopy = new MEA_Area()
                {
                    AreaLabel = a.AreaLabel,
                    AreaSizeDA = a.AreaSizeDA,
                    AreaSizeMm2 = a.AreaSizeMm2,
                    PerimeterMm = a.PerimeterMm,
                    DistanceToFoveaMm = a.DistanceToFoveaMm,
                    MeasurementXML = a.MeasurementXML
                };
            }
            else if (mea is MEA_DeltaVolume)
            {
                var a = mea as MEA_DeltaVolume;
                meaCopy = new MEA_DeltaVolume()
                {
                    AreaLabel = a.AreaLabel,
                    AreaSizeDA = a.AreaSizeDA,
                    AreaSizeMm2 = a.AreaSizeMm2,
                    PerimeterMm = a.PerimeterMm,
                    DistanceToFoveaMm = a.DistanceToFoveaMm,
                    MeasurementXML = a.MeasurementXML,
                    DeltaVolume = a.DeltaVolume,
                    GLD = a.GLD,
                    Volume = a.Volume
                };
            }
            else if (mea is MEA_ETDRSGrid)
            {
                var g = mea as MEA_ETDRSGrid;
                meaCopy = new MEA_ETDRSGrid()
                {
                    FoveaLocationX = g.FoveaLocationX,
                    FoveaLocationY = g.FoveaLocationY,
                    ONHLocationX = g.ONHLocationX,
                    ONHLocationY = g.ONHLocationY,
                    Sector0 = g.Sector0,
                    Sector1 = g.Sector1,
                    Sector2 = g.Sector2,
                    Sector3 = g.Sector3,
                    Sector4 = g.Sector4,
                    Sector5 = g.Sector5,
                    Sector6 = g.Sector6,
                    Sector7 = g.Sector7,
                    Sector8 = g.Sector8
                };
            }
            else if (mea is MEA_OCTGrid)
            {
                var g = mea as MEA_OCTGrid;
                meaCopy = new MEA_OCTGrid()
                {
                    CenterLocationAscan = g.CenterLocationAscan,
                    CenterLocationFrame = g.CenterLocationFrame,
                    CenterPointThicknessMm = g.CenterPointThicknessMm,
                    TotalVolumeMm3 = g.TotalVolumeMm3,
                    OCTGridLabel = g.OCTGridLabel,
                    OCTGridLayer1 = g.OCTGridLayer1,
                    OCTGridLayer2 = g.OCTGridLayer2,
                    Sector0Thick = g.Sector0Thick,
                    Sector1Thick = g.Sector1Thick,
                    Sector2Thick = g.Sector2Thick,
                    Sector3Thick = g.Sector3Thick,
                    Sector4Thick = g.Sector4Thick,
                    Sector5Thick = g.Sector5Thick,
                    Sector6Thick = g.Sector6Thick,
                    Sector7Thick = g.Sector7Thick,
                    Sector8Thick = g.Sector8Thick,
                    Sector0Vol = g.Sector0Vol,
                    Sector1Vol = g.Sector1Vol,
                    Sector2Vol = g.Sector2Vol,
                    Sector3Vol = g.Sector3Vol,
                    Sector4Vol = g.Sector4Vol,
                    Sector5Vol = g.Sector5Vol,
                    Sector6Vol = g.Sector6Vol,
                    Sector7Vol = g.Sector7Vol,
                    Sector8Vol = g.Sector8Vol
                };
            }

            meaCopy.MeasurementTypeID = mea.MeasurementTypeID;
            meaCopy.MEAMeasurementType = mea.MEAMeasurementType;
            meaCopy.RawDataID = mea.RawDataID;
            meaCopy.DicomFrameID = mea.DicomFrameID;
            return meaCopy;
        }

        private void CreateCopyforFirstGrade(GRD_Report report)
        {
            var isFirstGrade = !Repository.Context.GRD_Reports.Any(r => r.IsActive && r.GReportID != report.GReportID && r.PACSSeries.SeriesID == report.PACSSeries.SeriesID && r.IsPrimaryResult);

            if (!(report.IsSigned && isFirstGrade))
                return;

            var reportCopy = new GRD_Report()
            {
                IsPrimaryResult = true,
                IsActive = report.IsActive,
                IsSigned = report.IsSigned,
                PerformedBy = report.PerformedBy,
                PerformedDate = report.PerformedDate,
                PerformedTime = report.PerformedTime,
                GTemplateID = report.GTemplateID,
                SeriesID = report.SeriesID
            };

            Repository.Context.Add(reportCopy);

            IncludeAttachments(report, reportCopy);
            Dictionary<long, MEA_Measurement> meaCopies = IncludeMeasurements(report, reportCopy);
            IncludeResults(report, reportCopy, meaCopies);

            Repository.Context.SaveChanges();
        }

        private void IncludeResults(GRD_Report report, GRD_Report reportCopy, Dictionary<long, MEA_Measurement> meaCopies)
        {
            var rResults = Repository.Context.GRD_ReportResults.Where(item => item.GReportID == report.GReportID).ToList();
            foreach (var result in rResults)
            {
                var resultCopy = new GRD_ReportResult()
                {
                    GQuestionGroupName = result.GQuestionGroupName,
                    GQuestionString = result.GQuestionString,
                    GQuestionDes = result.GQuestionDes,
                    GAnswersString = result.GAnswersString,
                    Laterality = result.Laterality
                };

                Repository.Context.Add(resultCopy);

                if (result.MEAMeasurement != null)
                {
                    resultCopy.MEAMeasurement = meaCopies[result.MEAMeasurement.MeasurementID];
                }

                resultCopy.GRDReport = reportCopy;
            }
        }

        private Dictionary<long, MEA_Measurement> IncludeMeasurements(GRD_Report report, GRD_Report reportCopy)
        {
            var rMeasurements = Repository.Context.MEA_Measurements.Where(item => item.GReportID == report.GReportID).ToList();
            var meaCopies = new Dictionary<long, MEA_Measurement>();
            foreach (var mea in rMeasurements)
            {
                var meaCopy = CreateMeasurementCopy(mea);
                Repository.Context.Add(meaCopy);
                meaCopy.GRDReport = reportCopy;

                meaCopies.Add(mea.MeasurementID, meaCopy);
            }

            return meaCopies;
        }

        private void IncludeAttachments(GRD_Report report, GRD_Report reportCopy)
        {
            var rAttachments = Repository.Context.PACS_SeriesAttachments.Where(item => item.IsActive && item.GReportID == report.GReportID).ToList();
            foreach (var attm in rAttachments)
            {
                var attmCopy = CreateAttachmentCopy(attm);
                Repository.Context.Add(attmCopy);
                attmCopy.GRDReport = reportCopy;
            }
        }

        private void CreateCopyforMultiModality(GRD_Report report, PACS_Series series)
        {
            var reportCopy = new GRD_Report()
            {
                IsPrimaryResult = report.IsPrimaryResult,
                IsActive = report.IsActive,
                IsSigned = report.IsSigned,
                PerformedBy = report.PerformedBy,
                PerformedDate = report.PerformedDate,
                PerformedTime = report.PerformedTime,
                GTemplateID = report.GTemplateID,
                SeriesID = series.SeriesID
            };

            Repository.Context.Add(reportCopy);

            IncludeAttachments(report, reportCopy);
            Dictionary<long, MEA_Measurement> meaCopies = IncludeMeasurements(report, reportCopy);
            IncludeResults(report, reportCopy, meaCopies);

            Repository.Context.SaveChanges();

            var isFirstGrade = !Repository.Context.GRD_Reports.Any(r => r.PACSSeries.SeriesID == series.SeriesID && r.IsPrimaryResult);

            if (!(report.IsSigned && isFirstGrade))
                return;

            var reportCopy2 = new GRD_Report()
            {
                IsPrimaryResult = true,
                IsActive = reportCopy.IsActive,
                IsSigned = reportCopy.IsSigned,
                PerformedBy = reportCopy.PerformedBy,
                PerformedDate = reportCopy.PerformedDate,
                PerformedTime = reportCopy.PerformedTime,
                GTemplateID = reportCopy.GTemplateID,
                SeriesID = series.SeriesID
            };

            Repository.Context.Add(reportCopy2);

            IncludeAttachments(reportCopy, reportCopy2);
            meaCopies = IncludeMeasurements(reportCopy, reportCopy2);
            IncludeResults(reportCopy, reportCopy2, meaCopies);

            Repository.Context.SaveChanges();
        }

        private void CreateCopiesForMultiModality(GRD_Report report)
        {
            var series = report.PACSSeries as WF_Sequence;

            var seriesInGroup = GetSeriesEntitiesInGroup(series).Where(s => s.SeriesID != series.SeriesID && s.WFTempStepID == series.WFTempStepID);

            foreach (var s in seriesInGroup)
            {
                CreateCopyforMultiModality(report, s);
            }
        }

        private void CreateReportCopies(GRD_Report report, bool isSign = true)
        {
            var series = report.PACSSeries;

            if (isSign)
                CreateCopyforFirstGrade(report);

            if (series.SeriesGroupID != null)
                CreateCopiesForMultiModality(report);

            RemoveReportDuplicates(series.SeriesID, report.PerformedBy.Value);
        }

        #endregion

        #region Workflow

        public ResultInfo<IList<SeriesBaseDto>> CompleteStep(long id, long currentStepId, bool ignoreMultiModality, string receivedLaterality, string password)
        {
            var result = new ResultInfo<IList<SeriesBaseDto>>();

            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                //Validate Password
                if (!UserHelper.ValidatePassword(aspUser, password))
                    throw new Exception("Invalid password.");

                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    var currentStep = entity.WFTempStep;
                    if (currentStep == null)
                    {
                        throw new Exception("Current workflow step not found.");
                    }

                    if (currentStep.WFTempStepID != currentStepId)
                    {
                        throw new Exception("Data conflict.");
                    }

                    switch (currentStep.WFStepList.WFStepListDes.ToLower())
                    {
                        case "upload":
                        case "check-in":
                            break;
                        default:
                            throw new Exception("Current step cannot be completed by this method.");
                    }

                    List<WF_Sequence> entities = GetSeriesEntitiesInGroup(entity);

                    entity.LateralityReceived = receivedLaterality;

                    if (currentStep.WFStepList.WFStepListDes.ToLower() == "check-in" && !ignoreMultiModality)
                    {
                        ValidateMultiModality(entity, entities);
                    }

                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                    result.Result = GoToNextStep(id, false, false, user).Select(x => GenerateSeriesBaseDto(x, user)).ToList();
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        public ResultInfo<IList<SeriesBaseDto>> Review(long id, long currentStepId, string password, string reason)
        {
            var result = new ResultInfo<IList<SeriesBaseDto>>();

            try
            {
                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var aspUser = AuthRepository.GetSingle(x => x.UserId == aspUserId);

                //Validate Password
                if (!UserHelper.ValidatePassword(aspUser, password))
                    throw new Exception("Invalid password");

                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    var currentStep = entity.WFTempStep;
                    if (currentStep == null)
                    {
                        throw new Exception("Current workflow step not found");
                    }

                    if (currentStep.WFTempStepID != currentStepId)
                    {
                        throw new Exception("Data conflict.");
                    }

                    switch (currentStep.WFStepList.WFStepListDes.ToLower())
                    {
                        case "completed":
                            break;
                        default:
                            throw new Exception("Current step cannot be reviewed");
                    }

                    List<WF_Sequence> entities = GetSeriesEntitiesInGroup(entity);

                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);
                    result.Result = GoToStep(id, reason, null, false, false, "Verify", false, false, user).Select(x => GenerateSeriesBaseDto(x, user)).ToList();
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        private IQueryable<WF_Sequence> GoToNextVerifyStep(long id, CONTACT_User user)
        {
            return GoToStep(id, null, true, false, "Verify", false, false, user);
        }

        private IQueryable<WF_Sequence> GoToNextGradeStep(long id, CONTACT_User user)
        {
            return GoToStep(id, null, true, false, "Grade", false, false, user);
        }

        private IQueryable<WF_Sequence> GoToNextStep(long seriesId, bool keepAssigned, bool cloneSeries, CONTACT_User user)
        {
            return GoToStep(seriesId, null, true, true, null, keepAssigned, cloneSeries, user);
        }

        private IQueryable<WF_Sequence> GoToStep(long seriesId, string reason, bool isNextStep, bool shouldSkip, string stepName, bool keepAssigned, bool cloneSeries, CONTACT_User user)
        {
            return GoToStep(seriesId, reason, null, isNextStep, shouldSkip, stepName, keepAssigned, cloneSeries, user);
        }

        private IQueryable<WF_Sequence> GoToStep(long seriesId, string reason, long? assignedToID, bool isNextStep, bool shouldSkip, string stepName, bool keepAssigned, bool cloneSeries, CONTACT_User user)
        {
            WF_TempStep step;
            var series = Repository.GetSingle(s => s.SeriesID == seriesId);
            var curStep = series.WFTempStep.WFStepList.WFStepListDes.ToLower();
            var steps = Repository.Context.WF_TempSteps.Where(st => st.WFTemplateID == series.WFTempStep.WFTemplateID).OrderBy(st => st.WFStepOrder).ToList();

            if (isNextStep)
            {
                if (string.IsNullOrEmpty(stepName))
                {
                    step = steps.FirstOrDefault(st => (shouldSkip ? !st.ShouldSkip : true) &&
                        st.WFStepOrder > series.WFTempStep.WFStepOrder);
                }
                else
                {
                    step = steps.FirstOrDefault(st => (shouldSkip ? !st.ShouldSkip : true) &&
                        st.WFStepList.WFStepListDes == stepName && st.WFStepOrder > series.WFTempStep.WFStepOrder);
                }

                //Check if current step is grade and is second grade then should go to verify
                if (curStep == "grade" && step.WFStepList.WFStepListDes.ToLower() != "grade")
                {
                    //find signed reports
                    var totalSignedReports = Repository.Context.GRD_Reports.Count(r => r.IsActive && r.SeriesID == series.SeriesID && r.IsSigned && !r.IsPrimaryResult);

                    if (totalSignedReports > 1 && series.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.AlwaysVerifyMultipleGrades)
                    {
                        stepName = "Verify";
                        shouldSkip = false;
                        step = steps.FirstOrDefault(st => st.WFStepList.WFStepListDes == stepName && st.WFStepOrder > series.WFTempStep.WFStepOrder);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(stepName))
                {
                    step = steps.LastOrDefault(st => (shouldSkip ? !st.ShouldSkip : true) &&
                        st.WFStepOrder < series.WFTempStep.WFStepOrder);
                }
                else
                {
                    //if was completed, inactivate previous primary grading result
                    if (curStep == "completed")
                    {
                        var allPrimaryReports = Repository.Context.GRD_Reports.Where(r => r.IsActive && r.SeriesID == series.SeriesID && r.IsPrimaryResult).ToList();
                        allPrimaryReports.ForEach(r => r.IsActive = false);
                    }

                    step = steps.LastOrDefault(st => (shouldSkip ? !st.ShouldSkip : true) &&
                        st.WFStepList.WFStepListDes == stepName && st.WFStepOrder < series.WFTempStep.WFStepOrder);
                }
            }

            if (curStep == "Grade")
            {
                if (step.WFStepList.WFStepListDes == "Grade")
                {
                    var cf = Repository.Context.WF_CategoryFlags.FirstOrDefault(x => x.CategoryDes.ToLower() == "magenta");
                    if (cf != null)
                        series.CategoryFlagID = cf.CategoryFlagID;
                }
            }

            var isMultiModality = false;
            List<WF_Sequence> seriesInGroup = null;
            string newStep = null;
            if (step != null)
            {
                newStep = step.WFStepList.WFStepListDes.ToLower();

                if (cloneSeries && newStep == "completed")
                    AddToWorkerDataQueue("CloneSeriesForEligibility", seriesId, user);

                if (series.SeriesGroupID == null || (curStep == "upload" && newStep == "check-in")
                    || (curStep == "check-in" && newStep == "upload"))
                    GoToStep(series, reason, isNextStep, step, keepAssigned);
                else
                {
                    //Is multi-modality
                    isMultiModality = true;
                    seriesInGroup = GetSeriesEntitiesInGroup(series);
                    foreach (var s in seriesInGroup)
                        GoToStep(s, reason, isNextStep, step, keepAssigned);
                }
            }
            else
            {
                throw new Exception("Next workflow step not found.");
            }

            var dataContexChanged = false;
            if (Repository.Context.HasChanges)
            {
                Repository.Context.SaveChanges();
                dataContexChanged = true;
            }

            if (curStep == "upload" && isNextStep)
            {
                Task.Factory.StartNew(() =>
                {
                    NotificationsHelper.SendUploadConfirmationEmail(user.UserID, series.SeriesID);
                });
            }

            if (!keepAssigned & step != null)
            {
                //Send notifications
                switch (newStep)
                {
                    case "check-in":
                        if (series.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint)
                            NotificationsHelper.SendNotifications(user.UserID, "CheckInPool_New_Eligibility", series.PACSTimePoint.PACSSubject.PACSSite.TrialID,
                                series.PACSTimePoint.PACSSubject.PACSSite.AffiliationID, series.SeriesID, isMultiModality);
                        else
                            NotificationsHelper.SendNotifications(user.UserID, "CheckInPool_New", series.PACSTimePoint.PACSSubject.PACSSite.TrialID,
                                series.PACSTimePoint.PACSSubject.PACSSite.AffiliationID, series.SeriesID, isMultiModality);
                        break;
                    case "grade":
                        if (series.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint)
                            NotificationsHelper.SendNotifications(user.UserID, "GradePool_New_Eligibility", series.PACSTimePoint.PACSSubject.PACSSite.TrialID,
                                series.PACSTimePoint.PACSSubject.PACSSite.AffiliationID, series.SeriesID, isMultiModality);
                        else
                            NotificationsHelper.SendNotifications(user.UserID, "GradePool_New", series.PACSTimePoint.PACSSubject.PACSSite.TrialID,
                                series.PACSTimePoint.PACSSubject.PACSSite.AffiliationID, series.SeriesID, isMultiModality);
                        break;
                    case "verify":
                        if (series.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint)
                            NotificationsHelper.SendNotifications(user.UserID, "VerifyPool_New_Eligibility", series.PACSTimePoint.PACSSubject.PACSSite.TrialID,
                                series.PACSTimePoint.PACSSubject.PACSSite.AffiliationID, series.SeriesID, isMultiModality);
                        else
                            NotificationsHelper.SendNotifications(user.UserID, "VerifyPool_New", series.PACSTimePoint.PACSSubject.PACSSite.TrialID,
                                series.PACSTimePoint.PACSSubject.PACSSite.AffiliationID, series.SeriesID, isMultiModality);
                        break;
                    case "completed":
                        if (series.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint)
                            NotificationsHelper.SendNotifications(user.UserID, "Completed_New_Eligibility", series.PACSTimePoint.PACSSubject.PACSSite.TrialID,
                                series.PACSTimePoint.PACSSubject.PACSSite.AffiliationID, series.SeriesID, isMultiModality);
                        else
                            NotificationsHelper.SendNotifications(user.UserID, "Completed_New", series.PACSTimePoint.PACSSubject.PACSSite.TrialID,
                                series.PACSTimePoint.PACSSubject.PACSSite.AffiliationID, series.SeriesID, isMultiModality);
                        break;
                    default:
                        break;
                }
            }

            if (dataContexChanged)
            {
                Repository.Refresh(series);
                return GetSeriesEntitiesInGroup(series).AsQueryable();
            }
            else
                return new List<WF_Sequence>().AsQueryable();
        }

        private bool AddToWorkerDataQueue(string reqType, long dataId, CONTACT_User user)
        {
            var settings = new Settings();
            var storageAccount = CloudStorageAccount.Parse(settings.GetSetting("StorageConnection"));
            // initialize queue storage 
            CloudQueueClient queueStorage = storageAccount.CreateCloudQueueClient();
            var queue = queueStorage.GetQueueReference("process-data");
            //queue.CreateIfNotExist();//**Azure update
            queue.CreateIfNotExists();

            //set data
            queue.AddMessage(new CloudQueueMessage(String.Format("{0},{1},{2}", reqType, user.UserID, dataId)));

            return true;
        }

        private void GoToStep(WF_Sequence sequence, string reason, bool shouldAudit, WF_TempStep step, bool keepAssigned)
        {
            GoToStep(sequence, null, reason, shouldAudit, step, keepAssigned);
        }

        private void GoToStep(WF_Sequence sequence, long? assignedToID, string reason, bool shouldAudit, WF_TempStep step, bool keepAssigned)
        {
            long? currWFTempStepID = sequence.WFTempStepID;
            long nextWFTempStepID = step.WFTempStepID;

            if (reason != null)
            {
                var entitiesChanges = ChangeSetHelper.CreateEntityChangeList(
                    "WF_Sequence", "Update", "WFTempStepID",
                    currWFTempStepID.ToString(),
                    nextWFTempStepID.ToString());

                var recordOverride = AuditRecordsRepository.AddRecord("WorkflowStepOverride");
                recordOverride.ReasonForChange = reason;
                recordOverride.WFTempStepID = currWFTempStepID;
                recordOverride.TrialID = sequence.PACSTimePoint.PACSSubject.PACSSite.TrialID;
                recordOverride.SeriesID = sequence.SeriesID;
                recordOverride.DetailsXML = entitiesChanges.ToXML();
            }
            else if (shouldAudit)
            {
                var recordCompleted = AuditRecordsRepository.AddRecord("WorkflowStepCompleted");
                recordCompleted.WFTempStepID = currWFTempStepID;
                recordCompleted.TrialID = sequence.PACSTimePoint.PACSSubject.PACSSite.TrialID;
                recordCompleted.SeriesID = sequence.SeriesID;
            }

            var recordInitiated = AuditRecordsRepository.AddRecord("WorkflowStepInitiated");
            recordInitiated.WFTempStepID = nextWFTempStepID;
            recordInitiated.TrialID = sequence.PACSTimePoint.PACSSubject.PACSSite.TrialID;
            recordInitiated.SeriesID = sequence.SeriesID;

            if (sequence.WFTempStep.WFStepList.WFStepListDes.ToLower() == "grade")
            {
                if (step.WFStepList.WFStepListDes.ToLower() == "grade")
                {
                    var cf = Repository.Context.WF_CategoryFlags.FirstOrDefault(x => x.CategoryDes.ToLower() == "magenta");
                    if (cf != null)
                        sequence.CategoryFlagID = cf.CategoryFlagID;
                }
            }

            sequence.WFTempStep = step;
            sequence.WFTempStepID = nextWFTempStepID;
            sequence.LastStepCompletionDate = DateTime.UtcNow;
            sequence.LastExportDateTime = null;

            if (assignedToID != null)
            {
                sequence.AssignedToID = assignedToID;
            }
            else if (!keepAssigned)
            {
                sequence.AssignedToID = null;
            }
        }

        private void ValidateMultiModality(WF_Sequence entity, List<WF_Sequence> entities)
        {
            if (entity.SeriesGroupID == null)
                return;

            //Validate MultiModality
            var expectedProcedures = Repository.Context.PACS_TPProcLists.Where(x => x.TimePointsListID == entity.PACSTPProcList.TimePointsListID
                && x.WFTemplateID == entity.PACSTPProcList.WFTemplateID
                && x.GTemplateID == entity.PACSTPProcList.GTemplateID
                && x.CRFTemplateID == entity.PACSTPProcList.CRFTemplateID).Select(x => x.CERTImgProcedureList).OrderBy(x => x.ImgProcedureID).ToList();

            var procedures = entities.Select(x => x.PACSTPProcList.CERTImgProcedureList).Distinct().OrderBy(x => x.ImgProcedureID).ToList();

            var missingProcedures = expectedProcedures.Except(procedures);
            var isGroupComplete = missingProcedures.Count() == 0;

            var warning = new MultiModalityWarning();

            if (!isGroupComplete)
            {
                foreach (var p in missingProcedures)
                {
                    var mmp = new MultiModalityProcedure()
                    {
                        Name = p.ImgProcedureName,
                        IsMissing = true
                    };
                    warning.Procedures.Add(mmp);
                }
            }

            foreach (var s in entities)
            {
                var mp = new MultiModalityProcedure();
                mp.Name = s.PACSTPProcList.CERTImgProcedureList.ImgProcedureName;
                mp.IsReceivedLateralitySet = !string.IsNullOrEmpty(s.LateralityReceived);

                mp.IsImagesMissing = !s.PACS_RawData.Any(y => y.IsActive);

                if (!mp.IsImagesMissing)
                {
                    mp.IsImagesProcessed = !s.PACS_RawData.Any(y => y.IsActive && (y.PACSRawDataStatus != null ? y.PACSRawDataStatus.StatusName != "Ready" : true));
                    mp.IsImagesLateralitySet = !s.PACS_RawData.Any(y => y.IsActive && y.Laterality == null);

                    var isOPCalibrationReady = !s.PACS_RawData.OfType<PACS_DicomOP>().Any(y => y.IsActive && (y.PixelSpacingX == null || y.PixelSpacingY == null));
                    var isOPTCalibrationReady = !s.PACS_RawData.OfType<PACS_DicomOPT>().Any(y => y.IsActive && (y.PixelSpacingX == null || y.PixelSpacingY == null || y.FrameSpacing == null));
                    //var isWSIReady = !s.PACS_RawData.OfType<PACS_DicomWSI>().Any(y => y.IsActive && (string.IsNullOrEmpty(y.TileFormat) || y.TileOverlap == null || y.TileSizeX == null || y.TileSizeY == null));
                    mp.IsImagesCalibrationSet = isOPCalibrationReady && isOPTCalibrationReady;
                }

                if (!mp.IsReceivedLateralitySet || mp.IsImagesMissing || !mp.IsImagesProcessed || !mp.IsImagesLateralitySet || !mp.IsImagesCalibrationSet)
                    warning.Procedures.Add(mp);
            }

            if (!isGroupComplete || warning.Procedures.Count > 0)
            {
                var msgStart = "Multi-Modality Warning: ";
                var json = JsonConvert.SerializeObject(warning);
                var msg = string.Format("{0}{1}", msgStart, json);

                if (Repository.Context.HasChanges)
                    Repository.Context.ClearChanges();
                throw new Exception(msg);
            }

        }

        public ResultInfo<IList<SeriesBaseDto>> Assign(long id, bool ignoreRegrade = false)
        {
            var result = new ResultInfo<IList<SeriesBaseDto>>();

            try
            {
                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    if (entity.WFTempStep.WFStepList.WFStepListDes.ToLower() == "grade" && !ignoreRegrade)
                    {
                        //Check if user graded already
                        var hasGraded = Repository.Context.GRD_Reports.Any(x => x.SeriesID == entity.SeriesID && x.IsActive && !x.IsPrimaryResult && x.IsSigned && x.PerformedBy == user.UserID);
                        if (hasGraded)
                        {
                            throw new Exception("User already graded the series");
                        }
                    }

                    var entities = new List<WF_Sequence>();
                    if (entity.SeriesGroupID != null)
                    {
                        var seriesInGroup = DataHelpers.RetryPolicy.ExecuteAction(() =>
                        {
                            return Repository.FindBy(x => x.SeriesGroupID == entity.SeriesGroupID).ToList();
                        });
                        entities.AddRange(seriesInGroup);
                    }
                    else
                    {
                        entities.Add(entity);
                    }

                    foreach (var s in entities)
                    {
                        s.AssignedToID = user.UserID;
                        //Add audit record
                        var record = AuditRecordsRepository.AddRecord("SeriesAssigned");
                        record.TrialID = s.PACSTPProcList.PACSTimePointsList.TrialID;
                        record.SeriesID = s.SeriesID;
                        record.WFTempStepID = s.WFTempStepID;
                    }

                    Repository.Commit();

                    result.Result = GetSeriesInGroup(entity, user);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        public ResultInfo<IList<SeriesBaseDto>> Unassign(long id)
        {
            var result = new ResultInfo<IList<SeriesBaseDto>>();

            try
            {
                var entity = Repository.GetSingle(x => x.SeriesID == id);
                if (entity != null)
                {
                    var entities = GetSeriesEntitiesInGroup(entity);

                    foreach (var s in entities)
                    {
                        s.AssignedToID = null;
                        //Add audit record
                        var record = AuditRecordsRepository.AddRecord("SeriesUnassigned");
                        record.TrialID = s.PACSTPProcList.PACSTimePointsList.TrialID;
                        record.SeriesID = s.SeriesID;
                        record.WFTempStepID = s.WFTempStepID;
                    }

                    Repository.Commit();

                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    result.Result = GetSeriesInGroup(entity, user);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Series not found");
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        public ResultInfo<IList<SeriesBaseDto>> Group(SeriesGroupRequestDto request)
        {
            var result = new ResultInfo<IList<SeriesBaseDto>>();

            try
            {
                var entities = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.FindBy(x => request.SeriesIds.Contains(x.SeriesID)).ToList();
                });

                if (entities.Count > 1)
                {
                    //validate series can be grouped
                    var entity = entities.First();

                    var canGroup = entities.All(x => x.TimePointsID == entity.TimePointsID
                        && x.PACSTPProcList.GTemplateID == entity.PACSTPProcList.GTemplateID
                        && x.PACSTPProcList.WFTemplateID == entity.PACSTPProcList.WFTemplateID
                        && x.PACSTPProcList.CRFTemplateID == entity.PACSTPProcList.CRFTemplateID);

                    if (!canGroup)
                        throw new Exception("Can not group series.");

                    var sg = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return Repository.Context.PACS_SeriesGroups.FirstOrDefault(x => x.TimePointsID == entity.TimePointsID
                        && x.GTemplateID == entity.PACSTPProcList.GTemplateID
                        && x.WFTemplateID == entity.PACSTPProcList.WFTemplateID
                        && x.CRFTemplateID == entity.PACSTPProcList.CRFTemplateID);
                    });

                    if (sg == null)
                    {
                        sg = new PACS_SeriesGroup()
                        {
                            TimePointsID = entity.PACSTPProcList.TimePointsListID,
                            GTemplateID = entity.PACSTPProcList.GTemplateID,
                            WFTemplateID = entity.PACSTPProcList.WFTemplateID,
                            CRFTemplateID = entity.PACSTPProcList.CRFTemplateID,
                        };

                        Repository.Context.Add(sg);
                    }

                    foreach (var s in entities)
                    {
                        if (sg.SeriesGroupID > 0)
                            s.SeriesGroupID = sg.SeriesGroupID;
                        s.PACSSeriesGroup = sg;
                    }

                    Repository.Commit();

                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    result.Result = GetSeriesInGroup(entity, user);
                    result.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        private List<WF_Sequence> GetSeriesEntitiesInGroup(WF_Sequence entity)
        {
            var entities = new List<WF_Sequence>();
            if (entity.SeriesGroupID != null)
            {
                var seriesInGroup = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.FindBy(x => x.IsActive && x.SeriesGroupID == entity.SeriesGroupID);
                });

                entities.AddRange(seriesInGroup);
            }
            else
            {
                entities.Add(entity);
            }

            return entities;
        }

        private List<SeriesBaseDto> GetSeriesInGroup(WF_Sequence entity, CONTACT_User user)
        {
            return GetSeriesEntitiesInGroup(entity).Select(x => GenerateSeriesBaseDto(x, user)).ToList();
        }

        #endregion

        #region Comments

        public ResultInfo<IList<SeriesCommentFullDto>> GetComments(long id)
        {
            var result = new ResultInfo<IList<SeriesCommentFullDto>>();
            try
            {
                var entities = Repository.GetComments(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new SeriesCommentFullDto(x, new SeriesBaseDto())).ToList();
                });
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        public ResultInfo<SeriesCommentFullDto> AddComment(long id, string userId, string value)
        {
            var result = new ResultInfo<SeriesCommentFullDto>();

            try
            {
                var entity = Repository.AddComment(id, userId, value);
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new SeriesCommentFullDto(entity, new SeriesBaseDto());
                result.Result = dto;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        #endregion

        #region Audit

        public ResultInfo<IList<AuditRecordFullDto>> GetWorkflowAuditRecords(long id)
        {
            var result = new ResultInfo<IList<AuditRecordFullDto>>();
            try
            {
                var entities = Repository.GetWorkflowAuditRecords(id);
                result.Result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Select(x => new AuditRecordFullDto(x, new SeriesBaseDto())).ToList();
                });
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }

            return result;
        }

        #endregion
    }
}