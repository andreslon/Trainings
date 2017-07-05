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
    public class MediaGateway : IMediaGateway
    {
        public IMediaRepository Repository { get; set; }
        public IAuditRecordsRepository AuditRecordsRepository { get; set; }

        public MediaGateway(IMediaRepository repository, IAuditRecordsRepository auditRecordsRepository)
        {
            Repository = repository;
            AuditRecordsRepository = auditRecordsRepository;
        }

        private string GetDefaultDicomFileLocation(PACS_RawDatum entity)
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}/Check-Ins/File_{5}.", entity.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.TrialID, entity.PACSSeries.PACSTimePoint.PACSSubject.SiteID, entity.PACSSeries.PACSTimePoint.SubjectID, entity.PACSSeries.TimePointsID, entity.SeriesID, entity.RawDataID);
        }

        public ResultInfo<IList<MediaBaseDto>> GetAll(MediaRequestDto request)
        {
            var result = new ResultInfo<IList<MediaBaseDto>>();
            try
            {
                List<MediaBaseDto> listDto = new List<MediaBaseDto>();
                var listResult = Repository.GetAll(request.SeriesId, request.CertUserId, request.CertEquipmentId, request.DataType, request.IsActive, request.Ids);
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return listResult.Count();
                });
                result.SetPager(count, request.Page, request.PageSize);
                var listPaged = GeneralHelper.GetPagedList(listResult.OrderBy(x => x.RawDataIndex), result.Pager);
                if (listPaged != null)
                {
                    foreach (var entity in listPaged)
                    {
                        var dto = new MediaBaseDto(entity, new SeriesBaseDto());
                        if (string.IsNullOrEmpty(dto.DicomFileLocation))
                            dto.DicomFileLocation = GetDefaultDicomFileLocation(entity);
                        if (dto.DicomOPT != null)
                            dto.SegmentationStatus = Repository.GetSegmentationStatus(entity.RawDataID);
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

        private void SetMediaType(PACS_RawDatum entity)
        {
            if(entity is PACS_DicomOP)
            {
                entity.DataTypeID = Repository.Context.PACS_DataTypes.Single(x => x.DataType == "OP").DataTypeID;
            }
            else if(entity is PACS_DicomOPT)
            {
                entity.DataTypeID = Repository.Context.PACS_DataTypes.Single(x => x.DataType == "OPT").DataTypeID;
            }
            else if (entity is PACS_DicomEPDF)
            {
                entity.DataTypeID = Repository.Context.PACS_DataTypes.Single(x => x.DataType == "EPDF").DataTypeID;
            }
            else if (entity is PACS_DicomWSI)
            {
                entity.DataTypeID = Repository.Context.PACS_DataTypes.Single(x => x.DataType == "WSI").DataTypeID;
            }
        }

        private void SetMediaStatus(PACS_RawDatum entity, string statusName)
        {
            entity.StatusID = Repository.Context.PACS_RawDataStatus.Single(x => x.StatusName == statusName).StatusID;
        }

        public MediaStatusFullDto GetMediaStatus(string statusName)
        {
            var entity = Repository.Context.PACS_RawDataStatus.Single(x => x.StatusName == statusName);
            return new MediaStatusFullDto(entity);
        }

        public ResultInfo<MediaFullDto> Add(MediaFullDto request)
        {
            //Perform input validation
            //----

            var result = new ResultInfo<MediaFullDto>();
            try
            {
                PACS_RawDatum entity = null;
                if(request.MediaTypeId != null)
                {
                    var mediaType = Repository.Context.PACS_DataTypes.FirstOrDefault(x => x.DataTypeID == request.MediaTypeId);
                    if (mediaType.DataType == "OP")
                        entity = new PACS_DicomOP();
                    else if (mediaType.DataType == "OPT")
                        entity = new PACS_DicomOPT();
                    else if (mediaType.DataType == "EPDF")
                        entity = new PACS_DicomEPDF();
                    else if (mediaType.DataType == "WSI")
                        entity = new PACS_DicomWSI();
                    else
                        entity = new PACS_RawDatum();
                }
                entity = request.ToEntity(entity);
                if(entity.PACSDataType == null)
                {
                    SetMediaType(entity);
                }
                SetMediaStatus(entity, "Saved");
                Repository.Add(entity);
                var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                Repository.Commit();
                Repository.Refresh(entity);
                var dto = new MediaFullDto(entity);
                if (string.IsNullOrEmpty(dto.DicomFileLocation))
                    dto.DicomFileLocation = GetDefaultDicomFileLocation(entity);
                if (dto.DicomOPT != null)
                    dto.SegmentationStatus = Repository.GetSegmentationStatus(dto.Id.GetValueOrDefault());
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

        public ResultInfo<MediaFullDto> Update(MediaFullDto request, string fields = null, string password = null, string reason = null)
        {
            var result = new ResultInfo<MediaFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.RawDataID == request.Id);
                if (entity != null)
                {
                    entity = request.ToEntity(entity, fields);
                    Repository.Update(entity);
                    var record = AuditRecordsRepository.AddRecord("ActionUndefined");
                    Repository.Commit();
                    Repository.Refresh(entity);
                    var dto = new MediaFullDto(entity);
                    if (string.IsNullOrEmpty(dto.DicomFileLocation))
                        dto.DicomFileLocation = GetDefaultDicomFileLocation(entity);
                    if (dto.DicomOPT != null)
                        dto.SegmentationStatus = Repository.GetSegmentationStatus(dto.Id.GetValueOrDefault());
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Media not found");
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
                var entity = Repository.GetSingle(x => x.RawDataID == id);
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
                    throw new Exception("Media not found");
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

        public ResultInfo<MediaFullDto> GetSingle(long id)
        {
            var result = new ResultInfo<MediaFullDto>();
            try
            {
                var entity = Repository.GetSingle(x => x.RawDataID == id);
                if (entity != null)
                {
                    var dto = new MediaFullDto(entity);
                    if (string.IsNullOrEmpty(dto.DicomFileLocation))
                        dto.DicomFileLocation = GetDefaultDicomFileLocation(entity);
                    if (dto.DicomOPT != null)
                        dto.SegmentationStatus = Repository.GetSegmentationStatus(dto.Id.GetValueOrDefault());
                    result.Result = dto;
                    result.IsSuccess = true;
                }
                else
                {
                    throw new Exception("Media not found");
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