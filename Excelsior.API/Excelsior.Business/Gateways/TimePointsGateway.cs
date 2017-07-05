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
    public class TimePointsGateway : ITimePointsGateway
    {
        public ITimePointsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public TimePointsGateway(ITimePointsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<TimePointBaseDto>> GetAll(TimePointsRequestDto request)
        {
            var result = new ResultInfo<IList<TimePointBaseDto>>();
            try
            {
                var timePointListResponse = new List<TimePointBaseDto>();

                IQueryable<PACS_TimePointsList> tpListItems = Repository.GetAll(request.StudyId, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return tpListItems.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var tpListItemsPaged = GeneralHelper.GetPagedList(tpListItems.OrderBy(x => x.TimePointsSeq), result.Pager);
                if (tpListItemsPaged != null)
                {
                    foreach (var tpList in tpListItemsPaged)
                    {
                        var dto = new TimePointBaseDto(tpList);
                        timePointListResponse.Add(dto);
                    }
                }

                result.Result = timePointListResponse;
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

        public ResultInfo<TimePointFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TimePointFullDto>();
            try
            {
                var timePoint = Repository.GetSingle(x => x.TimePointsListID == id);
                if (timePoint != null)
                {
                    var dto = new TimePointFullDto(timePoint);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("TimePoint not found");
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

        public ResultInfo<TimePointFullDto> Add(TimePointFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<TimePointFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new TimePointFullDto(entity);
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

        public ResultInfo<TimePointFullDto> Update(TimePointFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<TimePointFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.TimePointsListID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new TimePointFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("TimePoint not found");
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
                var entity = Repository.GetSingle(x => x.TimePointsListID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("TimePoint not found");
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
