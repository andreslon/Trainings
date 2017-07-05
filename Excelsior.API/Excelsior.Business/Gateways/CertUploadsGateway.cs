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
    public class CertUploadsGateway : ICertUploadsGateway
    {
        public ICertUploadsRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }

        public CertUploadsGateway(ICertUploadsRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        private string GetDefaultDataFileLocation(CERT_UploadInfo entity)
        {
            long? trialId = null;
            if (entity.CERTUser != null)
                trialId = entity.CERTUser.CONTACTUserTrial.TrialID;
            else if (entity.CERTEquipment != null)
                trialId = entity.CERTEquipment.TrialID;
            else
                return null;
            return string.Format("/Certification/{0}/File_{1}.", trialId.GetValueOrDefault(), entity.CertUploadInfoID);
        }

        public ResultInfo<IList<CertUploadBaseDto>> GetAll(CertUploadsRequestDto request)
        {
            var result = new ResultInfo<IList<CertUploadBaseDto>>();
            try
            {
                var listDto = new List<CertUploadBaseDto>();
                var listResult = Repository.GetAll(request.CertUserId, request.CertEquipmentId, request.IsCertified, request.IsActive);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return listResult.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var listPaged = GeneralHelper.GetPagedList(listResult.OrderBy(x => x.UploadDate), result.Pager);
                if (listPaged != null)
                {
                    foreach (var entity in listPaged)
                    {
                        CertUploadBaseDto dto;
                        if(request.CertUserId != null)
                            dto = new CertUploadBaseDto(entity, new CertUserBaseDto());
                        else if(request.CertEquipmentId != null)
                            dto = new CertUploadBaseDto(entity, new CertEquipmentBaseDto());
                        else
                            dto = new CertUploadBaseDto(entity);
                        if (string.IsNullOrEmpty(dto.DataFileLocation))
                            dto.DataFileLocation = GetDefaultDataFileLocation(entity);
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

        private void SetMediaStatus(CERT_UploadInfo entity, string statusName)
        {
            entity.StatusID = Repository.Context.PACS_RawDataStatus.Single(x => x.StatusName == statusName).StatusID;
        }

        public MediaStatusFullDto GetMediaStatus(string statusName)
        {
            var entity = Repository.Context.PACS_RawDataStatus.Single(x => x.StatusName == statusName);
            return new MediaStatusFullDto(entity);
        }

        public ResultInfo<CertUploadFullDto> Add(CertUploadFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<CertUploadFullDto>();
            try
            {
                var entity = request.ToEntity();
                SetMediaStatus(entity, "Saved");
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new CertUploadFullDto(entity);
                if (string.IsNullOrEmpty(dto.DataFileLocation))
                    dto.DataFileLocation = GetDefaultDataFileLocation(entity);
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

        public ResultInfo<CertUploadFullDto> Update(CertUploadFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<CertUploadFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CertUploadInfoID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity, fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new CertUploadFullDto(entity);
                    if (string.IsNullOrEmpty(dto.DataFileLocation))
                        dto.DataFileLocation = GetDefaultDataFileLocation(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Certification Upload not found");
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
                var entity = Repository.GetSingle(x => x.CertUploadInfoID == id);
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
                    throw new Exception("Certification Upload not found");
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

        public ResultInfo<CertUploadFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<CertUploadFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.CertUploadInfoID == id);
                if (entity != null)
                {
                    var dto = new CertUploadFullDto(entity);
                    if (string.IsNullOrEmpty(dto.DataFileLocation))
                        dto.DataFileLocation = GetDefaultDataFileLocation(entity);
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Certification Upload not found");
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