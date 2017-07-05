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
    public class StencilsGateway : IStencilsGateway
    {
        public IStencilsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public StencilsGateway(IStencilsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }


        public ResultInfo<IList<StencilsBaseDto>> GetAll(StencilsRequestDto request)
        {
            //Perform input validation
            //----
            
            //Get the result
            var result = new ResultInfo<IList<StencilsBaseDto>>();
            try
            {
                var stencilsResult = new List<StencilsBaseDto>();
                var stencils = Repository.GetAll(request.StudyId);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return stencils.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var stencilsPaged = GeneralHelper.GetPagedList(stencils.OrderBy(x => x.StencilID), result.Pager);
                if (stencilsPaged != null)
                {
                    foreach (var att in stencilsPaged)
                    {
                        var dto = new StencilsBaseDto(att);
                        stencilsResult.Add(dto);
                    }
                }

                result.Result = stencilsResult;
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
        
        public ResultInfo<StencilsFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<StencilsFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.StencilID == id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new StencilsFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Document not found");
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
        public ResultInfo<StencilsFullDto> Add(StencilsFullDto request)
        {
            //Perform input validation
            //----
            var result = new ResultInfo<StencilsFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new StencilsFullDto(entity);
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

        public ResultInfo<StencilsFullDto> Update(StencilsFullDto request)
        {
            var result = new ResultInfo<StencilsFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.StencilID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity);
                    Repository.Update(entity);
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new StencilsFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Document not found");
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
                var entity = Repository.GetSingle(x => x.StencilID == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    Repository.Commit();
                    result.Result = true;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Document not found");
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