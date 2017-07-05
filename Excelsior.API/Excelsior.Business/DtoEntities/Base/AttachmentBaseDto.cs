using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AttachmentBaseDto
    {
        public AttachmentBaseDto()
            : this(null)
        {

        }
        public AttachmentBaseDto(PACS_SeriesAttachment entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.SeriesAttachmentID;
                UserId = entity.UserID;
                SeriesId = entity.SeriesID;
                IsActive = entity.IsActive;
                DateCreated = entity.DateCreated;
                Laterality = entity.Laterality;
                FileLocation = entity.FileLocation;
                StatusId = entity.StatusID;
                ReportId = entity.GReportID;
                if (entity.CONTACTUser != null)
                {
                    UserFirstName = entity.CONTACTUser.FirstName;
                    UserLastName = entity.CONTACTUser.LastName;
                } 
            }
        }
        public virtual PACS_SeriesAttachment ToEntity(PACS_SeriesAttachment entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new PACS_SeriesAttachment();
            }

            entity.SeriesAttachmentID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["seriesid"])
                    entity.SeriesID = SeriesId;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["datecreated"])
                    entity.DateCreated = DateCreated;
                if (fieldvalidation["laterality"])
                    entity.Laterality = Laterality;
                if (fieldvalidation["userid"])
                    entity.UserID = UserId;
                if (fieldvalidation["statusid"])
                    entity.StatusID = StatusId;
                if (fieldvalidation["reportid"])
                    entity.GReportID = ReportId;
                if (fieldvalidation["filelocation"])
                    entity.FileLocation = FileLocation;

            }   
            return entity;
        }
        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? UserId { get; set; }
        [StringLength(512)]
        public string UserFirstName { get; set; }
        [StringLength(512)]
        public string UserLastName { get; set; }
        [Required]
        [StringLength(512)]
        public string Laterality { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }
        [Range(0, long.MaxValue)]
        public long? ReportId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
        public string FileLocation { get; set; }
        public bool? IsActive { get; set; }
        [Range(0, long.MaxValue)]
        public long? StatusId { get; set; }
 
    }
}
