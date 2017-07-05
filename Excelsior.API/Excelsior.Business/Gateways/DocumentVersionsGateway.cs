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
    public class DocumentVersionsGateway : IDocumentVersionsGateway
    {
        public IDocumentVersionsRepository Repository { get; set; }
        public IDocumentGroupsRepository DocumentGroupsRepository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public IResourceOwnerData ResourceOwnerData { get; set; }

        public DocumentVersionsGateway(IDocumentVersionsRepository repository, IDocumentGroupsRepository documentGroupsRepository, IAuditRecordsRepository auditRecordsRepository, IResourceOwnerData resourceOwnerData)
        {
            Repository = repository;
            DocumentGroupsRepository = documentGroupsRepository;
            AuditRecordsRepository = auditRecordsRepository;
            ResourceOwnerData = resourceOwnerData;
        }

        private string GetDefaultFileLocation(DOCU_DocumentVersion entity)
        {
            return string.Format("{0}/documents/{1}/file_{2}.", entity.DOCUDocument.DOCUDocumentGroup.TrialID, entity.DocumentID, entity.DocumentVersion);
        }
        private string GetDefaultAttachmentFileLocation(DOCU_DocumentVersion entity)
        {
            return string.Format("{0}/documents/{1}/attachment_{2}.", entity.DOCUDocument.DOCUDocumentGroup.TrialID, entity.DocumentID, entity.DocumentVersion);
        }

        public ResultInfo<IList<DocumentVersionBaseDto>> GetAll(DocumentVersionsRequestDto request)
        {
            var result = new ResultInfo<IList<DocumentVersionBaseDto>>();
            try
            {
                var listDto = new List<DocumentVersionBaseDto>();
                var listResult = Repository.GetAll(request.StudyId, request.DocumentId, request.IsActive);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return listResult.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var listPaged = GeneralHelper.GetPagedList(listResult.OrderBy(x => x.DocumentVersionID), result.Pager);
                if (listPaged != null)
                {
                    foreach (var entity in listPaged)
                    {
                        var dto = new DocumentVersionBaseDto(entity);
                        if (string.IsNullOrEmpty(dto.FileLocation))
                            dto.FileLocation = GetDefaultFileLocation(entity);
                        if (string.IsNullOrEmpty(dto.AttachmentFileLocation))
                            dto.AttachmentFileLocation = GetDefaultAttachmentFileLocation(entity);
                        listDto.Add(dto);
                    }
                }
                result.Result = listDto;
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

        private void SetMediaStatus(DOCU_DocumentVersion entity, string statusName)
        {
            entity.StatusID = Repository.Context.PACS_RawDataStatus.Single(x => x.StatusName == statusName).StatusID;
        }

        public MediaStatusFullDto GetMediaStatus(string statusName)
        {
            var entity = Repository.Context.PACS_RawDataStatus.Single(x => x.StatusName == statusName);
            return new MediaStatusFullDto(entity);
        }

        public ResultInfo<DocumentVersionFullDto> Add(DocumentVersionFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<DocumentVersionFullDto>();
            try
            {
                var entity = request.ToEntity();
                SetMediaStatus(entity, "Saved");
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new DocumentVersionFullDto(entity);
                if (string.IsNullOrEmpty(dto.FileLocation))
                {
                    dto.FileLocation = GetDefaultFileLocation(entity);
                }
                if (string.IsNullOrEmpty(dto.AttachmentFileLocation))
                {
                    dto.AttachmentFileLocation = GetDefaultAttachmentFileLocation(entity);
                }

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

        public ResultInfo<DocumentVersionFullDto> Update(DocumentVersionFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<DocumentVersionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DocumentVersionID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity, fields);
                    entity.StatusID = 3;
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new DocumentVersionFullDto(entity);
                    if (string.IsNullOrEmpty(dto.FileLocation))
                        dto.FileLocation = GetDefaultFileLocation(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Upload not found");
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

        public ResultInfo<DocumentVersionFullDto> UpdateAttachment(DocumentVersionFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<DocumentVersionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DocumentVersionID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity, fields);
                    entity.AttachmentStatusID = 3;
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new DocumentVersionFullDto(entity);
                    if (string.IsNullOrEmpty(dto.FileLocation))
                        dto.FileLocation = GetDefaultFileLocation(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Upload not found");
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
                var entity = Repository.GetSingle(x => x.DocumentVersionID == id);
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
                    throw new Exception("Upload not found");
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

        public ResultInfo<DocumentVersionFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<DocumentVersionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DocumentVersionID == id);
                if (entity != null)
                {
                    var aspUserId = Guid.Parse(ResourceOwnerData.GetUserId());
                    var user = Repository.Context.CONTACT_Users.FirstOrDefault(x => x.AspUserID == aspUserId);

                    var roles = entity.DOCUDocument.DOCU_DocumentRoles.Select(x => x.AspnetRole.RoleName);
                    var hasAccess = false;
                    var studyId = entity.DOCUDocument.DOCUDocumentGroup.TrialID;
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

                    if(!hasAccess)
                        throw new UnauthorizedAccessException("Access denied");

                    var dto = new DocumentVersionFullDto(entity);
                    if (string.IsNullOrEmpty(dto.FileLocation))
                        dto.FileLocation = GetDefaultFileLocation(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Upload not found");
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

        public ResultInfo<DocumentVersionFullDto> GetSingleAttachment(long id)
        {
            var result = new ResultInfo<DocumentVersionFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.DocumentVersionID == id);
                if (entity != null)
                {
                    var dto = new DocumentVersionFullDto(entity);
                    if (string.IsNullOrEmpty(dto.AttachmentFileLocation))
                        dto.AttachmentFileLocation = GetDefaultAttachmentFileLocation(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Upload not found");
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
    }
}