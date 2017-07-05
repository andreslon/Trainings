using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Business.Helpers;
using Excelsior.Business.Logic;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.API.Repositories
{
    public class RawDataRepository
    {
        public DataModel db { get; set; }
        public RawDataRepository(DataModel context)
        {
            db = context;
        }

        public ResultInfo<IList<RawDataResponseDto>> GetRawDataBySeriesID(RawDataRequestDto dto)
        {
            var result = new ResultInfo<IList<RawDataResponseDto>>();
            try
            {
                List<RawDataResponseDto> listDto = new List<RawDataResponseDto>();
                RawDataHandler handler = new RawDataHandler(db);
                var listResult = handler.GetRawDataBySeriesID(dto.SeriesId.GetValueOrDefault());
                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return listResult.Count();
                });
                result.SetPager(count, dto.Page, dto.PageSize);
                var listPaged = GeneralHelper.GetPagedList(listResult.OrderBy(x=> x.RawDataIndex), result.Pager);
                if (listPaged != null)
                {
                    foreach (var item in listPaged)
                    {
                        var dtoNew = ConvertToDto(item);
                        dtoNew.SegmentationStatus = GetSegmentationStatus(item.RawDataID);
                        listDto.Add(dtoNew);
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
        public ResultInfo<RawDataResponseDto> GetRawDataByID(CommonRequestDto request)
        {
            var result = new ResultInfo<RawDataResponseDto>();
            try
            {
                List<RawDataResponseDto> listDto = new List<RawDataResponseDto>();
                RawDataHandler handler = new RawDataHandler(db);
                var entity = handler.GetRawDataByID(request.CommonId);
                var dto = ConvertToDto(entity);
                if (dto != null && dto.DicomOPT != null)
                    dto.SegmentationStatus = GetSegmentationStatus(dto.RawDataID);
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
        private string GetSegmentationStatus(long? rawDataID)
        {
            if (!rawDataID.HasValue)
                return string.Empty;

            RawDataHandler handler = new RawDataHandler(db);
            var result = handler.GetSegmentationStatus(rawDataID.Value);
            return result;
        }
        private RawDataResponseDto ConvertToDto(PACS_RawDatum item)
        {
            if (item == null)
                return null;

            var dto = new RawDataResponseDto()
            {
                RawDataID = item.RawDataID,
                SeriesID = item.SeriesID,
                DataTypeID = item.DataTypeID,
                ThumbImageLocation = item.ThumbImageLocation,
                DCMInstanceUID = item.DCMInstanceUID,
                DCMFileLocation = item.DCMFileLocation,
                Laterality = item.Laterality,
                IsActive = item.IsActive,
                LastError = item.LastError,
                StatusID = item.StatusID,
                HasError = item.HasError,
            };

            if (item.PACSDataType != null)
            {
                dto.DataType = new DataTypeResponseDto()
                {
                    DataTypeID = item.PACSDataType.DataTypeID,
                    DataType = item.PACSDataType.DataType
                };
            }

            if (item.PACSRawDataStatus != null)
            {
                dto.Status = new RDStatusResponseDto()
                {
                    StatusID = item.PACSRawDataStatus.StatusID,
                    StatusName = item.PACSRawDataStatus.StatusName,
                };
            }

            if (item is PACS_DicomOP)
            {
                var op = item as PACS_DicomOP;
                dto.DicomOP = new DicomOPResponseDto()
                {
                    AcquisitionTime = op.AcquisitionTime,
                    BolusTime = op.BolusTime,
                    ImageHeight = op.ImageHeight,
                    ImageWidth = op.ImageWidth,
                    PixelSpacingX = op.PixelSpacingX,
                    PixelSpacingY = op.PixelSpacingY,
                    RawDataID = op.RawDataID,
                };
            }

            if (item is PACS_DicomOPT)
            {
                var op = item as PACS_DicomOPT;
                dto.DicomOPT = new DicomOPTResponseDto()
                {
                    FrameSpacing = op.FrameSpacing,
                    ImageHeight = op.ImageHeight,
                    ImageWidth = op.ImageWidth,
                    PixelSpacingX = op.PixelSpacingX,
                    PixelSpacingY = op.PixelSpacingY,
                    RawDataID = op.RawDataID,
                    RefDCMInstanceUID = op.RefDCMInstanceUID,
                    RefImageCoveredArea = op.RefImageCoveredArea,
                    RefRawDataID = op.RefRawDataID,
                    ScanType = op.ScanType,                    
                };
            }

            if (item is PACS_DicomWSI)
            {
                var op = item as PACS_DicomWSI;
                dto.DicomWSI = new DicomWSIResponseDto()
                {
                    PixelSpacingX = op.PixelSpacingX,
                    PixelSpacingY = op.PixelSpacingY,
                    RawDataID = op.RawDataID,
                    TileFormat = op.TileFormat,
                    TileOverlap = op.TileOverlap,
                    TileSizeX = op.TileSizeX,
                    TileSizeY = op.TileSizeY,
                    WSIImageHeight = op.WSIImageHeight,
                    WSIImageWidth = op.WSIImageWidth,
                };
            }

            return dto;
        }
        public List<PACS_RawDatum> UpdateSatatusAndLoadRawData(long rawDataID, string status,bool? hasError)
        {
            var rd = db.PACS_RawData.Single(item => item.RawDataID == rawDataID);
            rd.StatusID = db.PACS_RawDataStatus.Single(item => item.StatusName == status).StatusID;

            if (hasError != null)
                rd.HasError = hasError.Value;

            DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                db.SaveChanges();
            });

            return db.PACS_RawData.Where(item => item.RawDataID == rawDataID).ToList();
        }
    }
}
