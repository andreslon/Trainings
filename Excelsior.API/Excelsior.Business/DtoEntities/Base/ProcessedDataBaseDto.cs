using Excelsior.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class ProcessedDataBaseDto
    {
        public ProcessedDataBaseDto()
            : this(null)
        {
        }
        public ProcessedDataBaseDto(PACS_ProcessedDatum entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.ProcessedDataID;
                MediaId = entity.RawDataID;
                Label = entity.ProcessedDataLabel;
                XmlString = entity.ProcessDataXMLString;
                UserId = entity.UserID;
                FrameId = entity.DicomFrameID;
                DateCreated = entity.DateCreated;
                DateModified = entity.DateModified;
            }
        }
        public virtual PACS_ProcessedDatum ToEntity(PACS_ProcessedDatum entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_ProcessedDatum();
            }
            
            entity.ProcessedDataID = Id.GetValueOrDefault(0);
            entity.RawDataID = MediaId;
            entity.ProcessedDataLabel = Label;
            entity.ProcessDataXMLString = XmlString;
            entity.UserID = UserId;
            entity.DicomFrameID = FrameId; 
            entity.DateCreated = DateCreated;
            entity.DateModified = DateModified; 

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? MediaId { get; set; }
        [StringLength(50)]
        [Required]
        public string Label { get; set; }
        public string XmlString { get; set; }
        [Range(0, long.MaxValue)]
        public long? UserId { get; set; }
        [Range(0, long.MaxValue)]
        public long? FrameId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateModified { get; set; }
    }
}
