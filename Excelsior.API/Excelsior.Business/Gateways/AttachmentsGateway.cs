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
    public class AttachmentsGateway : IAttachmentsGateway
    {
        public IAttachmentsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }
        public AttachmentsGateway(IAttachmentsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        private string GetDefaultFileLocation(PACS_SeriesAttachment entity)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}/Check-Ins/Attachment_{5}.", entity.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.TrialID, entity.PACSSeries.PACSTimePoint.PACSSubject.SiteID, entity.PACSSeries.PACSTimePoint.SubjectID, entity.PACSSeries.TimePointsID, entity.SeriesID, entity.SeriesAttachmentID);
        }

        public ResultInfo<IList<AttachmentBaseDto>> GetAll(AttachementsRequestDto request)
        {
            //Perform input validation
            //----
            
            //Get the result
            var result = new ResultInfo<IList<AttachmentBaseDto>>();
            try
            {
                var attachmentsResult = new List<AttachmentBaseDto>();
                var attachments = Repository.GetAll(request.SeriesId, request.UserId, request.Laterality, request.IsActive, request.Search);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return attachments.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var attachmentsPaged = GeneralHelper.GetPagedList(attachments.OrderBy(x => x.SeriesAttachmentID), result.Pager);
                if (attachmentsPaged != null)
                {
                    foreach (var att in attachmentsPaged)
                    {
                        var dto = new AttachmentBaseDto(att);
                        if (string.IsNullOrEmpty(dto.FileLocation))
                            dto.FileLocation = GetDefaultFileLocation(att);
                        attachmentsResult.Add(dto);
                    }
                }

                result.Result = attachmentsResult;
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
        
        public ResultInfo<AttachmentFullDto> GetSingle(long id)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<AttachmentFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.SeriesAttachmentID == id);

                if (entity != null)
                {
                    //Convert to Dto
                    var dto = new AttachmentFullDto(entity);
                    if (string.IsNullOrEmpty(dto.FileLocation))
                        dto.FileLocation = GetDefaultFileLocation(entity);
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
        private void SetMediaStatus(PACS_SeriesAttachment entity, string statusName)
        {
            entity.StatusID = Repository.Context.PACS_RawDataStatus.Single(x => x.StatusName == statusName).StatusID;
        }
        public ResultInfo<AttachmentFullDto> Add(AttachmentFullDto request, string userId)
        {
            //Perform input validation
            //----
            var result = new ResultInfo<AttachmentFullDto>();
            try
            {
                var entity = request.ToEntity();
                SetMediaStatus(entity, "Saved");
                entity.DateCreated = DateTime.UtcNow;
                if (userId == null)
                {
                    entity.UserID = request.UserId;
                }
                else
                {
                    entity.UserID = Repository.GetUserId(userId);
                }
                Repository.Add(entity);
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new AttachmentFullDto(entity);
                if (string.IsNullOrEmpty(dto.FileLocation))
                    dto.FileLocation = GetDefaultFileLocation(entity);
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

        public ResultInfo<AttachmentFullDto> Update(AttachmentFullDto request)
        {
            var result = new ResultInfo<AttachmentFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.SeriesAttachmentID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity);
                    entity.StatusID = 3;
                    Repository.Update(entity);
                    //var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    result.Result = new AttachmentFullDto(entity);
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
                var entity = Repository.GetSingle(x => x.SeriesAttachmentID == id);
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