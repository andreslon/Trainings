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
    public class AuthScopeGateway : IAuthScopeGateway
    {
        public IAuthScopeRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public AuthScopeGateway(IAuthScopeRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        } 

        public ResultInfo<IList<AuthScopeBaseDto>> GetAll(AuthScopesRequestDto request)
        {
            //Perform input validation
            //----

            //Get the result
            var result = new ResultInfo<IList<AuthScopeBaseDto>>();
            try
            {
                var templatesResult = new List<AuthScopeBaseDto>();
                var templates = Repository.GetAll();
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return templates.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var templatesPaged = GeneralHelper.GetPagedList(templates.OrderBy(x => x.ScopeId), result.Pager);
                if (templatesPaged != null)
                {
                    foreach (var entity in templatesPaged)
                    {
                        var dto = new AuthScopeBaseDto(entity);
                        templatesResult.Add(dto);
                    }
                }

                result.Result = templatesResult;
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

        public ResultInfo<AuthScopeFullDto> GetSingle(long id)
        {
            throw new NotImplementedException();
        }

        public ResultInfo<AuthScopeFullDto> GetSingle(Guid id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<AuthScopeFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.ScopeId ==id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new AuthScopeFullDto(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Scope not found");
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

        public ResultInfo<AuthScopeFullDto> Add(AuthScopeFullDto request)
        {

            //Perform input validation
            //----

            var result = new ResultInfo<AuthScopeFullDto>();
            try
            {
                var entity = request.ToEntity();

                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new AuthScopeFullDto(entity);
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

        public ResultInfo<AuthScopeFullDto> Update(AuthScopeFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<AuthScopeFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.ScopeId == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new AuthScopeFullDto(entity);
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Scope not found");
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
            throw new NotImplementedException();
        }

        public ResultInfo<bool> Delete(Guid id)
        {
            //Perform input validation
            //---- 
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.ScopeId == id);
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
                    throw new Exception("Scope not found");
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