using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class MediaBaseDto
    {
        public MediaBaseDto()
            : this(null)
        {

        }
        public MediaBaseDto(PACS_RawDatum entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.RawDataID;
                Index = entity.RawDataIndex;
                Laterality = entity.Laterality;
                DicomInstanceUId = entity.DCMInstanceUID;
                DicomFileLocation = entity.DCMFileLocation;
                ThumbImageLocation = entity.ThumbImageLocation;
                OriginalFileName = entity.OriginalFileName;
                IsActive = entity.IsActive;
                HasError = entity.HasError;
                LastError = entity.LastError;
                SeriesId = entity.SeriesID;
                CertUserId = entity.CertUserID;
                CertEquipmentId = entity.CertEquipmentID;
                MediaTypeId = entity.DataTypeID;
                MediaStatusId = entity.StatusID;

                if (!(sender is MediaTypeBaseDto) && entity.PACSDataType != null)
                {
                    MediaType = new MediaTypeFullDto(entity.PACSDataType, this);
                }

                if (!(sender is MediaStatusBaseDto) && entity.PACSRawDataStatus != null)
                {
                    MediaStatus = new MediaStatusFullDto(entity.PACSRawDataStatus, this);
                }

                switch (entity.PACSDataType.DataType.ToLower())
                {
                    case "op":
                        DicomOP = new DicomOPFullDto(entity as PACS_DicomOP);
                        break;
                    case "opt":
                        DicomOPT = new DicomOPTFullDto(entity as PACS_DicomOPT);
                        break;
                    case "epdf":
                        DicomEPDF = new DicomEPDFFullDto(entity as PACS_DicomEPDF);
                        break;
                    case "wsi":
                        DicomWSI = new DicomWSIFullDto(entity as PACS_DicomWSI);
                        break;
                }
            }
        }
        public virtual PACS_RawDatum ToEntity(PACS_RawDatum entity = null, string fields = null)
        {
            if (entity == null)
            {
                if (DicomOP != null)
                    entity = new PACS_DicomOP();
                else if (DicomOPT != null)
                    entity = new PACS_DicomOPT();
                else if (DicomEPDF != null)
                    entity = new PACS_DicomEPDF();
                else if (DicomWSI != null)
                    entity = new PACS_DicomWSI();
                else
                    entity = new PACS_RawDatum();
            }

            
            entity.RawDataID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {

                if (fieldvalidation["index"])
                    entity.RawDataIndex = Index.GetValueOrDefault();
                if (fieldvalidation["laterality"])
                    entity.Laterality = Laterality;
                if (fieldvalidation["dicominstanceuid"])
                    entity.DCMInstanceUID = DicomInstanceUId;
                if (fieldvalidation["dicomfilelocation"])
                    entity.DCMFileLocation = DicomFileLocation;
                if (fieldvalidation["thumbimagelocation"])
                    entity.ThumbImageLocation = ThumbImageLocation;
                if (fieldvalidation["originalfilename"])
                    entity.OriginalFileName = OriginalFileName;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["haserror"])
                    entity.HasError = HasError.GetValueOrDefault();
                if (fieldvalidation["lasterror"])
                    entity.LastError = LastError;
                if (fieldvalidation["seriesid"])
                    entity.SeriesID = SeriesId;
                if (fieldvalidation["certuserid"])
                    entity.CertUserID = CertUserId;
                if (fieldvalidation["certequipmentid"])
                    entity.CertEquipmentID = CertEquipmentId;
                if (fieldvalidation["datatypeid"])
                    entity.DataTypeID = MediaTypeId;
                if (fieldvalidation["statusid"])
                    entity.StatusID = MediaStatusId;
            }
               

            if(DicomOP != null && entity is PACS_DicomOP)
                entity = DicomOP.ToEntity(entity as PACS_DicomOP);
            else if (DicomOPT != null && entity is PACS_DicomOPT)
                entity = DicomOPT.ToEntity(entity as PACS_DicomOPT);
            else if (DicomEPDF != null && entity is PACS_DicomEPDF)
                entity = DicomEPDF.ToEntity(entity as PACS_DicomEPDF);
            else if (DicomWSI != null && entity is PACS_DicomWSI)
                entity = DicomWSI.ToEntity(entity as PACS_DicomWSI);

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? MediaTypeId { get; set; }
        [StringLength(512)]
        public string DicomFileLocation { get; set; }
        [StringLength(64)]
        public string DicomInstanceUId { get; set; }
        public bool? HasError { get; set; }
        public bool? IsActive { get; set; }
        public string LastError { get; set; }
        [StringLength(10)]
        public string Laterality { get; set; }
        [Range(0, long.MaxValue)]
        public long? Index { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertUserId { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertEquipmentId { get; set; }
        [Range(0, long.MaxValue)]
        public long? MediaStatusId { get; set; }
        [StringLength(512)]
        public string ThumbImageLocation { get; set; }
        [StringLength(256)]
        public string OriginalFileName { get; set; }
        public string SegmentationStatus { get; set; }

        public MediaTypeFullDto MediaType { get; set; }
        public MediaStatusFullDto MediaStatus { get; set; }
        public DicomOPFullDto DicomOP { get; set; }
        public DicomOPTFullDto DicomOPT { get; set; }
        public DicomWSIFullDto DicomWSI { get; set; }
        public DicomEPDFFullDto DicomEPDF { get; set; }
    }
}
