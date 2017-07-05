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
    public class DocumentRolesGateway : IDocumentRolesGateway
    {
        public IDocumentRolesRepository Repository { get; set; }
        public IRolesRepository RolesRepository { get; set; } 

        public DocumentRolesGateway(IDocumentRolesRepository repository, IRolesRepository rolesRepository)
        {
            Repository = repository;
            RolesRepository = rolesRepository;
        }

        public ResultInfo<IList<DocumentRoleBaseDto>> GetAll(DocumentRolesRequestDto request)
        {
            //Perform input validation
            //----
            
            //Get the result
            var result = new ResultInfo<IList<DocumentRoleBaseDto>>();
            try
            {
                var rolesResult = new List<DocumentRoleBaseDto>();
                var roles = Repository.GetAll(request.DocumentId, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return roles.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var docsPaged = GeneralHelper.GetPagedList(roles.OrderBy(x => x.AspnetRole.RoleName), result.Pager);
                if (docsPaged != null)
                {
                    foreach (var doc in docsPaged)
                    {
                        var dto = new DocumentRoleBaseDto(doc);
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
        
        public ResultInfo<DocumentRoleFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<DocumentRoleFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DocumentID == id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new DocumentRoleFullDto(entity);

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
        
        public ResultInfo<DocumentRoleFullDto> Add(DocumentRoleFullDto request)
        {
            //Perform input validation
            //----
            
            var result = new ResultInfo<DocumentRoleFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new DocumentRoleFullDto(entity);
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
 
        public ResultInfo<DocumentRoleFullDto> Update(DocumentRoleFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<DocumentRoleFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DocumentRoleID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new DocumentRoleFullDto(entity);
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
                var entity = Repository.GetSingle(x => x.DocumentRoleID == id);
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