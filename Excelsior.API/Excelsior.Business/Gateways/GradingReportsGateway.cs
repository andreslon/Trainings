using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class GradingReportsGateway : IGradingReportsGateway
    {
        public IReportsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public GradingReportsGateway(IReportsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        public ResultInfo<IList<GradingReportBaseDto>> GetAll(GradingReportsRequestDto request)
        {
            var result = new ResultInfo<IList<GradingReportBaseDto>>();
            try
            {
                var reportsResponse = new List<GradingReportBaseDto>();

                IQueryable<GRD_Report> entities = null;
                entities = Repository.GetAll(request.SeriesId, request.PerformedById, request.IsActive, request.IsPrimary, request.IsSigned);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderBy(x => x.PerformedDate), result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var report in entitiesPaged)
                    {
                        var dto = new GradingReportBaseDto(report);
                        reportsResponse.Add(dto);
                    }
                }

                result.Result = reportsResponse;
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

        public ResultInfo<GradingReportFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<GradingReportFullDto>();
            try
            {
                var report = Repository.GetSingle(x => x.GReportID == id);
                if (report != null)
                {
                    result.Result = new GradingReportFullDto(report);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Report not found");
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

        public ResultInfo<GradingReportFullDto> Add(GradingReportFullDto request)
        {
            var result = new ResultInfo<GradingReportFullDto>();
            try
            {
                //var entity = request.ToEntity();
                //Repository.Add(entity);
                //Repository.Commit();
                //Repository.Refresh(entity);
                //var dto = new GradingReportFullDto(entity);
                //result.Result = dto;
                //result.IsSuccess = true;
                throw new NotImplementedException();
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

        public ResultInfo<GradingReportFullDto> Update(GradingReportFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<GradingReportFullDto>();
            try
            {
                //var entity = Repository.GetSingle(x => x.GReportID == request.Id);
                //if (entity != null)
                //{
                //    entity = request.ToEntity(entity, fields);
                //    Repository.Update(entity);
                //    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                //    Repository.Commit();
                //    Repository.Refresh(entity);
                //    var dto = new GradingReportFullDto(entity);
                //    result.Result = dto;
                //    result.IsSuccess = true;
                //}
                //else
                //{
                //    throw new Exception("Grading report not found");
                //}
                throw new NotImplementedException();
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
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.GReportID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Grading report not found");
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
    }
}
