using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways.Interfaces;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using Excelsior.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class StudyReportsGateway : IStudyReportsGateway
    {
        public IStudyReportsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public StudyReportsGateway(IStudyReportsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        public ResultInfo<IList<StudyReportBaseDto>> GetAll(StudyReportRequestDto request)
        {
            var result = new ResultInfo<IList<StudyReportBaseDto>>();
            try
            {
                var reportsStudyListResponse = new List<StudyReportBaseDto>();

                IQueryable<RPT_TrialReport> tpListItems = Repository.GetAll(request.UserId, request.StudyId);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return tpListItems.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var tpListItemsPaged = GeneralHelper.GetPagedList(tpListItems.OrderBy(x => x.ReportID), result.Pager);
                if (tpListItemsPaged != null)
                {
                    foreach (var tpList in tpListItemsPaged)
                    {
                        var dto = new StudyReportBaseDto(tpList);
                        reportsStudyListResponse.Add(dto);
                    }
                }

                result.Result = reportsStudyListResponse;
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

        public ResultInfo<StudyReportFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<StudyReportFullDto>();
            try
            {
                var reportStudy = Repository.GetSingle(x => x.ReportID == id);
                if (reportStudy != null)
                {
                    var dto = new StudyReportFullDto(reportStudy);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Study report not found");
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

        public ResultInfo<StudyReportFullDto> Add(StudyReportFullDto request)
        {
            return null;
        }

        public ResultInfo<StudyReportFullDto> Update(StudyReportFullDto request, string fields = null, string password = null, string reason = null)
        {
            return null;
        }

        public ResultInfo<bool> Delete(long id)
        {
            return null;
        }
    }
}
