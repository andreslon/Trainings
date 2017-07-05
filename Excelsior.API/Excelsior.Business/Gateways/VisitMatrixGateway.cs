using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using Excelsior.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class VisitMatrixGateway : IVisitMatrixGateway
    {
        public IVisitMatrixRepository Repository { get; set; }
        public ISeriesRepository SeriesRepository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }
        public VisitMatrixGateway(IVisitMatrixRepository repository, ISeriesRepository seriesRepository, IAuditRecordsRepository auditRecordsRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            SeriesRepository = seriesRepository;
            AuditRecordsRepository = auditRecordsRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        public ResultInfo<IList<VisitMatrixSubjectFullDto>> GetSubjects(VisitMatrixSubjectsRequestDto request)
        {
            var result = new ResultInfo<IList<VisitMatrixSubjectFullDto>>();
            try
            {
                var subjectsResponse = new List<VisitMatrixSubjectFullDto>();
                var subjects = Repository.GetSubjects(request.SiteId, request.UserId, request.Search, request.ProcedureId, request.StepId);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return subjects.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var subjectsPaged = GeneralHelper.GetPagedList(subjects.OrderBy(x => x.RandomizedSubjectID).ThenBy(x => x.AlternativeRandomizedSubjectID), result.Pager);
                if (subjectsPaged != null)
                {
                    List<PACS_TimePointsList> timePointList = null;
                    List<CERT_ImgProcedureList> procedureList = null;

                    if (request.TimePointId.HasValue)
                    {
                        procedureList = DataHelpers.RetryPolicy.ExecuteAction(() =>
                        {
                            return Repository.GetTimePointProcedures(request.TimePointId.Value).ToList();
                        });
                    }
                    else
                    {
                        timePointList = DataHelpers.RetryPolicy.ExecuteAction(() =>
                        {
                            return Repository.GetTimePoints(request.SiteId).ToList();
                        });
                    }

                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    foreach (var subject in subjectsPaged)
                    {
                        var seriesList = DataHelpers.RetryPolicy.ExecuteAction(() =>
                        {
                            return Repository.GetSeries(subject.SubjectID).ToList();
                        });
                        timePointList = DataHelpers.RetryPolicy.ExecuteAction(() =>
                        {
                            return Repository.GetTimePoints(request.SiteId).ToList();
                        });
                        var dto = new VisitMatrixSubjectFullDto(subject);

                        if (procedureList != null)
                        {
                            //Find the status for each procedure
                            dto.Procedures = new List<VisitMatrixProcedureFullDto>();
                            foreach (var tpProc in procedureList)
                            {
                                var procDto = new VisitMatrixProcedureFullDto(tpProc);
                                procDto.TimePointId = request.TimePointId.Value;

                                WF_Sequence series = seriesList.FirstOrDefault(x =>
                                    x.PACSTimePoint.TimePointsListID == request.TimePointId.Value &&
                                    x.PACSTPProcList.ImgProcedureID == tpProc.ImgProcedureID);
                                if(series != null)
                                {
                                    procDto.SeriesId = series.SeriesID;
                                    procDto.Status = series.WFTempStep.WFStepList.WFStepListDes;
                                    procDto.StudyDate = series.StudyDate;
                                    procDto.totalQueriesPending = SeriesRepository.GetTotalQueriesPending(series);
                                    procDto.totalQueriesFlagged = SeriesRepository.GetTotalQueriesFlagged(series, user);
                                }
                                else
                                {
                                    procDto.Status = "Empty";
                                    procDto.SeriesId = null;
                                }
                                dto.Procedures.Add(procDto);
                            }
                        }
                        else if(timePointList != null)
                        {
                            //Find the status for each timepoint
                            dto.TimePoints = new List<VisitMatrixTimePointFullDto>();                            
                            foreach (var etp in timePointList)
                            {
                                var tpDto = new VisitMatrixTimePointFullDto(etp);

                                var statusPair = Repository.GetTimePointStatus(seriesList, etp.TimePointsListID, request.ProcedureId, request.StepId);
                                var series = statusPair.Value;
                                tpDto.Status = statusPair.Key;
                                if (series != null)
                                {
                                    tpDto.SeriesId = series.SeriesID;
                                    tpDto.StudyDate = series.StudyDate;
                                    tpDto.totalQueriesPending = SeriesRepository.GetTotalQueriesPending(series);
                                    tpDto.totalQueriesFlagged = SeriesRepository.GetTotalQueriesFlagged(series, user);
                                }
                                dto.TimePoints.Add(tpDto);
                            }
                        }

                        subjectsResponse.Add(dto);
                    }
                }

                result.Result = subjectsResponse;
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

        public ResultInfo<IList<VisitMatrixProcedureFullDto>> GetProcedures(VisitMatrixProceduresRequestDto request)
        {
            var result = new ResultInfo<IList<VisitMatrixProcedureFullDto>>();
            try
            {
                var proceduresResponse = new List<VisitMatrixProcedureFullDto>();

                var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                if (request.TimePointId.HasValue)
                { 
                    AddTimePointProcedures(request, proceduresResponse, user);
                }
                else if(request.SubjectId.HasValue)
                {
                    AddSubjectProcedures(request, proceduresResponse, user);
                }
                else
                {
                    AddSiteProcedures(request, proceduresResponse);
                }

                result.Result = proceduresResponse;
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

        private void AddTimePointProcedures(VisitMatrixProceduresRequestDto request, List<VisitMatrixProcedureFullDto> proceduresResponse, CONTACT_User user)
        {
            var timePointProcedures = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Repository.GetTimePointProcedures(request.TimePointId.Value).ToList();
            });
            var subjectSeries = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Repository.GetSeries(request.SubjectId.Value, request.TimePointId.Value).ToList();
            });

            foreach (var proc in timePointProcedures)
            {
                var dto = new VisitMatrixProcedureFullDto(proc);
                dto.TimePointId = request.TimePointId.Value;

                var serie = subjectSeries.FirstOrDefault(x => x.PACSTPProcList.ImgProcedureID == proc.ImgProcedureID);
                if (serie != null)
                {
                    dto.SeriesId = serie.SeriesID;
                    dto.Status = serie.WFTempStep.WFStepList.WFStepListDes;
                    dto.StudyDate = serie.StudyDate;
                    dto.totalQueriesPending = SeriesRepository.GetTotalQueriesPending(serie);
                    dto.totalQueriesFlagged = SeriesRepository.GetTotalQueriesFlagged(serie, user);
                }
                else
                {
                    dto.SeriesId = null;
                    dto.Status = "Empty";
                }
                proceduresResponse.Add(dto);
            }
        }
        private void AddSubjectProcedures(VisitMatrixProceduresRequestDto request, List<VisitMatrixProcedureFullDto> proceduresResponse, CONTACT_User user)
        {
            var trialProcedures = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Repository.GetSiteProcedures(request.SiteId).ToList();
            });
            var timePointList = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Repository.GetTimePoints(request.SiteId).ToList();
            });
            var subjectSeries = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Repository.GetSeries(request.SubjectId.Value).ToList();
            });

            foreach (var proc in trialProcedures)
            {
                var dto = new VisitMatrixProcedureFullDto(proc);

                //Find the status for each timepoint
                dto.TimePoints = new List<VisitMatrixTimePointFullDto>();
                foreach (var etp in timePointList)
                {                    
                    var tpDto = new VisitMatrixTimePointFullDto(etp);

                    var requieredProcedures = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return Repository.GetTimePointProcedures(etp.TimePointsListID)
                            .Count(x => x.ImgProcedureID == proc.ImgProcedureID);
                    });
                    if (requieredProcedures == 0)
                    {
                        tpDto.SeriesId = null;
                        tpDto.Status = "NA";
                    }
                    else
                    {
                        var serie = subjectSeries.FirstOrDefault(x =>
                            x.PACSTPProcList.ImgProcedureID == proc.ImgProcedureID &&
                            x.PACSTimePoint.TimePointsListID == etp.TimePointsListID);
                        if (serie != null)
                        {
                            tpDto.SeriesId = serie.SeriesID;
                            tpDto.Status = serie.WFTempStep.WFStepList.WFStepListDes;
                            tpDto.StudyDate = serie.StudyDate;
                            tpDto.totalQueriesPending = SeriesRepository.GetTotalQueriesPending(serie);
                            tpDto.totalQueriesFlagged = SeriesRepository.GetTotalQueriesFlagged(serie, user);
                        }
                        else
                        {
                            tpDto.SeriesId = null;
                            tpDto.Status = "Empty";
                        }
                    }
                    dto.TimePoints.Add(tpDto);
                }

                proceduresResponse.Add(dto);
            }
        }
        private void AddSiteProcedures(VisitMatrixProceduresRequestDto request, List<VisitMatrixProcedureFullDto> proceduresResponse)
        {
            var trialProcedures = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Repository.GetSiteProcedures(request.SiteId).ToList();
            });
            foreach (var proc in trialProcedures)
            {
                var dto = new VisitMatrixProcedureFullDto(proc);
                proceduresResponse.Add(dto);
            }
        }

        public ResultInfo<IList<TimePointFullDto>> GetPendingTimePoints(VisitMatrixProceduresRequestDto request)
        {
            var result = new ResultInfo<IList<TimePointFullDto>>();
            try
            {
                var response = new List<TimePointFullDto>();
                long? studyId = null;
                PACS_Subject subject = null;
                PACS_Site site = null;
                if (request.SubjectId.HasValue)
                {
                    subject = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return Repository.Context.PACS_Subjects.FirstOrDefault(x => x.SubjectID == request.SubjectId);
                    });
                }
                else
                {
                    if(request.SiteId.HasValue)
                    {
                        site = DataHelpers.RetryPolicy.ExecuteAction(() =>
                        {
                            return Repository.Context.PACS_Sites.FirstOrDefault(x => x.SiteID == request.SiteId);
                        });
                    }
                }

                if (subject != null)
                    studyId = subject.PACSSite.TrialID;
                if (site != null)
                    studyId = site.TrialID;

                var schedules = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Repository.Context.PACS_TPProcLists.Where(x => x.PACSTimePointsList.TrialID == studyId).ToList();
                });
                var timePoints = schedules.Select(x => x.PACSTimePointsList).Distinct();

                List<WF_Sequence> series = null;

                if(subject != null)
                {
                    series = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return Repository.Context.WF_Sequences.Where(x => x.IsActive && x.PACSTimePoint.SubjectID == subject.SubjectID).ToList();
                    });
                }
                else
                {
                    if(site != null)
                    {
                        series = DataHelpers.RetryPolicy.ExecuteAction(() =>
                        {
                            return Repository.Context.WF_Sequences.Where(x => x.IsActive && x.PACSTimePoint.PACSSubject.SiteID == site.SiteID).ToList();
                        });
                    }
                }

                foreach(var tp in timePoints)
                {
                    var sh = schedules.Where(x => x.TimePointsListID == tp.TimePointsListID).ToList();
                    var add = false;
                    foreach (var p in sh)
                    {
                        var s = series.FirstOrDefault(x => x.TPProcListID == p.TPProcID);
                        if (s == null)
                        {
                            add = true;
                            break;
                        }
                    }
                    if(add)
                        response.Add(new TimePointFullDto(tp));
                }

                result.Result = response;
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
    }
}