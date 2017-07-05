using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class UploadBaseDto
    {
        public UploadBaseDto()
            : this(null)
        {

        }
        public UploadBaseDto(UPLD_UploadInfo entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.UploadInfoID;
                UploadDate = entity.UploadDate;
                AcquisitionDate = entity.PhotoDate;
                DataFileLocation = entity.DataFileLocation;
                Note = entity.UploadNote;
                IsActive = entity.IsActive;
                SeriesId = entity.SeriesID;
                UploaderId = entity.UploaderID;
                MediaStatusId = entity.StatusID;
                HasError = entity.HasError;
                LastError = entity.LastError;

                if (!(sender is MediaStatusBaseDto) && entity.PACSRawDataStatus != null)
                {
                    MediaStatus = new MediaStatusFullDto(entity.PACSRawDataStatus, this);
                }
                if (!(sender is UserBaseDto) && entity.CONTACTUser != null)
                {
                    Technician = new UserFullDto(entity.CONTACTUser, this);
                }
            }
        }
        public virtual UPLD_UploadInfo ToEntity(UPLD_UploadInfo entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new UPLD_UploadInfo();
            }

            entity.UploadInfoID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["uploaddate"])
                    entity.UploadDate = UploadDate;
                if (fieldvalidation["acquisitiondate"])
                    entity.PhotoDate = AcquisitionDate;
                if (fieldvalidation["datafilelocation"])
                    entity.DataFileLocation = DataFileLocation;
                if (fieldvalidation["note"])
                    entity.UploadNote = Note;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["seriesid"])
                    entity.SeriesID = SeriesId;
                if (fieldvalidation["uploaderid"])
                    entity.UploaderID = UploaderId;
                if (fieldvalidation["mediastatusid"])
                    entity.StatusID = MediaStatusId;
                if (fieldvalidation["haserror"])
                    entity.HasError = HasError.GetValueOrDefault();
                if (fieldvalidation["lasterror"])
                    entity.LastError = LastError;
            }

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? UploadDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? AcquisitionDate { get; set; }
        [StringLength(512)]
        public string DataFileLocation { get; set; }
        public string Note { get; set; }
        public bool? IsActive { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }
        [Range(0, long.MaxValue)]
        public long? UploaderId { get; set; }
        [Range(0, long.MaxValue)]
        public long? MediaStatusId { get; set; }
        public bool? HasError { get; set; }
        public string LastError { get; set; }

        public SeriesFullDto Series { get; set; }
        public UserFullDto Technician { get; set; }
        public MediaStatusFullDto MediaStatus { get; set; }
    }
}
