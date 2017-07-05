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
    public class AnswerTypesGateway : IAnswerTypesGateway
    {
        public IAnswerTypesRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public AnswerTypesGateway(IAnswerTypesRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        public ResultInfo<IList<AnswerTypeBaseDto>> GetAll(AnswerTypesRequestDto request)
        {
            var result = new ResultInfo<IList<AnswerTypeBaseDto>>();
            try
            {
                var respose = new List<AnswerTypeBaseDto>();
                var entities = Repository.GetAll(request.Search);
                var count = 0;
                try
                {
                    count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return entities.Count();
                    });
                }
                catch (Exception e)
                {
                }

                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderBy(x => x.AnswerTypeName), result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new AnswerTypeBaseDto(entity);
                        respose.Add(dto);
                    }
                }

                result.Result = respose;
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

        public ResultInfo<AnswerTypeFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<AnswerTypeFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFAnswerTypeID == id);
                if (entity != null)
                {
                    var dto = new AnswerTypeFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Answer Type not found");
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

        public ResultInfo<AnswerTypeFullDto> Add(AnswerTypeFullDto request)
        {
            var result = new ResultInfo<AnswerTypeFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new AnswerTypeFullDto(entity);
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

        public ResultInfo<AnswerTypeFullDto> Update(AnswerTypeFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<AnswerTypeFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFAnswerTypeID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new AnswerTypeFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Answer Type not found");
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
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.CRFAnswerTypeID == id);
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
                    throw new Exception("Answer Type not found");
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
