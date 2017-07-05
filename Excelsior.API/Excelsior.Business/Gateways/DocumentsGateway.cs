using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Base;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Helpers;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using Excelsior.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Gateways
{
    public class DocumentsGateway : IDocumentsGateway
    {
        public IDocumentsRepository Repository { get; set; }
        public IDocumentVersionsRepository DocumentVersionsRepository { get; set; } 
        public IDocumentGroupsRepository DocumentGroupsRepository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }

        public DocumentsGateway(IDocumentsRepository repository, IDocumentVersionsRepository documentVersionsRepository, IDocumentGroupsRepository documentGroupsRepository, IAuditRecordsRepository auditRecordsRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            DocumentVersionsRepository = documentVersionsRepository;
            DocumentGroupsRepository = documentGroupsRepository;
            AuditRecordsRepository = auditRecordsRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        public ResultInfo<IList<DocumentBaseDto>> GetAll(DocumentsRequestDto request)
        {
            var result = new ResultInfo<IList<DocumentBaseDto>>();
            try
            {
                var docsResult = new List<DocumentBaseDto>();
                var documents = Repository.GetAll(request.StudyId, request.UserId, request.IsActive, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return documents.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var docsPaged = GeneralHelper.GetPagedList(documents.OrderBy(x => x.DocumentID), result.Pager);
                if (docsPaged != null)
                {
                    foreach (var doc in docsPaged)
                    {
                        var dto = new DocumentBaseDto(doc);
                        docsResult.Add(dto);
                    }
                }

                result.Result = docsResult;
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
        
        public ResultInfo<DocumentFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<DocumentFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DocumentID == id);

                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    var roles = entity.DOCU_DocumentRoles.Select(x => x.AspnetRole.LoweredRoleName);
                    var hasAccess = false;
                    var studyId = entity.DOCUDocumentGroup.TrialID;
                    PACS_Trial study = null;
                    switch (user.AspnetRole.LoweredRoleName)
                    {
                        case "administrator":
                        case "project manager":
                            hasAccess = true;
                            study = Repository.Context.PACS_Trials.FirstOrDefault(t => t.TrialID == studyId);
                            break;
                        default:
                            hasAccess = roles.Contains(user.AspnetRole.LoweredRoleName);
                            study = Repository.Context.CONTACT_UserTrials.FirstOrDefault(t => t.TrialID == studyId && t.UserID == user.UserID)?.PACSTrial;
                            break;
                    }

                    if (study == null)
                        throw new UnauthorizedAccessException("Access denied");

                    if (!hasAccess)
                        throw new UnauthorizedAccessException("Access denied");

                    //Convert to Dto
                    var dto = new DocumentFullDto(entity);

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
        
        public ResultInfo<DocumentFullDto> Add(DocumentFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<DocumentFullDto>();
            try
            {
                var entity = request.ToEntity();
                Repository.Add(entity);
                //var nextDocumentId = Repository.Order;
                if (request.LatestVersion != null)
                {
                    //this is where you add the version
                    var version = request.LatestVersion.ToEntity();
                    version.IsActive = true;
                    DocumentVersionsRepository.Add(version);
                    version.DOCUDocument = entity;
                }
                if (request.StudyId != null)
                {
                    //this is where you assign the group
                    //you must first check if there is a group already for the studyId
                    var group = DocumentGroupsRepository.GetSingle(x => x.TrialID == request.StudyId);
                    if(group != null)
                    {
                        entity.DocumentGroupID = group.DocumentGroupID;
                    }
                    else
                    {
                        group = new DOCU_DocumentGroup()
                        {
                            TrialID = request.StudyId,
                            DocuAuthorizationID = 2
                        };
                        DocumentGroupsRepository.Add(group);
                        entity.DOCUDocumentGroup = group;
                    }
                }
                //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                result.Result = new DocumentFullDto(entity);
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
 
        private string GetDefaultDataFileLocation(DocumentFullDto entity)
        {
            return string.Format("{0}/{1}{2}/{3}/{4}{5}{6}", entity.StudyId, "Documents", entity.Id, entity.LatestVersion.Version, entity.Name.Replace(" ", "_"), "_", entity.LatestVersion.Version);
        }

        public ResultInfo<DocumentFullDto> Update(DocumentFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<DocumentFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DocumentID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity,fields);
                    Repository.Update(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new DocumentFullDto(entity);
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
                var entity = Repository.GetSingle(x => x.DocumentID == id);
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