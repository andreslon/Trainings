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
    public class RolesGateway : IRolesGateway
    {
        public IRolesRepository Repository { get; set; }

        public RolesGateway(IRolesRepository repository)
        {
            Repository = repository;
        }

        public ResultInfo<IList<RoleBaseDto>> GetAll(RolesRequestDto request)
        {
            //Perform input validation
            //----
            
            //Get the result
            var result = new ResultInfo<IList<RoleBaseDto>>();
            try
            {
                var rolesResult = new List<RoleBaseDto>();
                var roles = Repository.GetAll(request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return roles.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var docsPaged = GeneralHelper.GetPagedList(roles.OrderBy(x => x.RoleName), result.Pager);
                if (docsPaged != null)
                {
                    foreach (var doc in docsPaged)
                    {
                        var dto = new RoleBaseDto(doc);
                        rolesResult.Add(dto);
                    }
                }

                result.Result = rolesResult;
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
        public ResultInfo<RoleFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----
            var result = new ResultInfo<RoleFullDto>();
            return result;
        }
        public ResultInfo<RoleFullDto> GetSingle(Guid id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<RoleFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.ApplicationId == id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new RoleFullDto(entity);

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
        
        public ResultInfo<RoleFullDto> Add(RoleFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<RoleFullDto>();
            return result;
        }

        public ResultInfo<RoleFullDto> Update(RoleFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<RoleFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.ApplicationId == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity);
                    Repository.Update(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new RoleFullDto(entity);
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
            var result = new ResultInfo<bool>();/*
            try
            {
                var entity = Repository.GetSingle(x => x.ApplicationId == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
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
            }*/
            return result;
        }
        public ResultInfo<bool> Delete(Guid id)
        {
            //Perform input validation
            //---- 
            var result = new ResultInfo<bool>();
            try
            {
                var entity = Repository.GetSingle(x => x.ApplicationId == id);
                if (entity != null)
                {
                    Repository.Delete(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
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