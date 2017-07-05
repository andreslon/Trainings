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
    public class AffiliationsGateway : IAffiliationsGateway
    {
        public IAffiliationsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public AffiliationsGateway(IAffiliationsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<AffiliationBaseDto>> GetAll(AffiliationsRequestDto request)
        {
            var result = new ResultInfo<IList<AffiliationBaseDto>>();
            try
            {
                var affiliationsRespose = new List<AffiliationBaseDto>();
                var entities = Repository.GetAll(request.UserId, request.IsActive, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return entities.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var entitiesPaged = GeneralHelper.GetPagedList(entities.OrderBy(x=> x.AffiliationName), result.Pager);
                if (entitiesPaged != null)
                {
                    foreach (var entity in entitiesPaged)
                    {
                        var dto = new AffiliationBaseDto(entity);
                        affiliationsRespose.Add(dto);
                    }
                }

                result.Result = affiliationsRespose;
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

        public ResultInfo<AffiliationFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<AffiliationFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.AffiliationID == id);
                if (entity != null)
                {
                    var dto = new AffiliationFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Affiliation not found");
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

        public ResultInfo<AffiliationFullDto> Add(AffiliationFullDto request)
        {
            var result = new ResultInfo<AffiliationFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new AffiliationFullDto(entity);
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

        public ResultInfo<AffiliationFullDto> Update(AffiliationFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<AffiliationFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.AffiliationID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new AffiliationFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Affiliation not found");
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
                var entity = Repository.GetSingle(x => x.AffiliationID == id);
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
                    throw new Exception("Affiliation not found");
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