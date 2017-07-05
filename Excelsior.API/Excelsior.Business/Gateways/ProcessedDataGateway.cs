using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class ProcessedDataGateway : IProcessedDataGateway
    {
        public IProcessedDataRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public ProcessedDataGateway(IProcessedDataRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }
        
        public ResultInfo<IList<ProcessedDataBaseDto>> GetAll(ProcessedDataRequestDto request)
        {
            var result = new ResultInfo<IList<ProcessedDataBaseDto>>();
            try
            {
                var processedDataPaged = new List<ProcessedDataBaseDto>();
                var processedData = Repository.GetAll(request.MediaId);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return processedData.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var ProcessedDataPaged = GeneralHelper.GetPagedList(processedData.OrderBy(x => x.ProcessedDataID), result.Pager);
                if (ProcessedDataPaged != null)
                {
                    foreach (var pdata in ProcessedDataPaged)
                    {
                        var dto = new ProcessedDataBaseDto(pdata);
                        processedDataPaged.Add(dto);
                    }
                }

                result.Result = processedDataPaged;
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

        public ResultInfo<ProcessedDataFullDto> Add(ProcessedDataFullDto request, string currentUserId)
        {
            var result = new ResultInfo<ProcessedDataFullDto>();
            try
            {
                var entity = request.ToEntity();

                var existing = Repository.GetSingle(x => x.RawDataID == entity.RawDataID && x.ProcessedDataLabel == entity.ProcessedDataLabel);

                var now = DateTime.UtcNow;
                if (existing == null)
                {
                    entity.DateCreated = now;
                    Repository.Add(entity);
                }
                else
                {
                    entity = existing;
                }

                entity.DateModified = now;

                if (currentUserId != null)
                {
                    entity.UserID = Repository.GetUserId(currentUserId);
                }

                //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new ProcessedDataFullDto(entity);
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