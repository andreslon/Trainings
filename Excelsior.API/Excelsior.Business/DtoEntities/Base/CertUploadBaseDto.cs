using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class CertUploadBaseDto
    {
        public CertUploadBaseDto()
            : this(null)
        {

        }
        public CertUploadBaseDto(CERT_UploadInfo entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CertUploadInfoID;
                UploadDate = entity.UploadDate;
                AcquisitionDate = entity.PhotoDate;
                DataFileLocation = entity.DataFileLocation;
                Note = entity.InternalNote;
                IsActive = entity.IsActive;
                IsCertified = entity.IsCertified;
                CertEquipmentId = entity.CertEquipmentID;
                CertUserId = entity.CertUserID;
                TechnicianId = entity.UserID;
                EquipmentId = entity.EquipmentID;
                MediaStatusId = entity.StatusID;
                HasError = entity.HasError;
                LastError = entity.LastError;

                if (!(sender is MediaStatusBaseDto) && entity.PACSRawDataStatus != null)
                {
                    MediaStatus = new MediaStatusFullDto(entity.PACSRawDataStatus, this);
                }
                if (!(sender is CertUserBaseDto) && entity.CERTUser != null)
                {
                    CertUser = new CertUserFullDto(entity.CERTUser, this);
                }
                if (!(sender is CertEquipmentBaseDto) && entity.CERTEquipment != null)
                {
                    CertEquipment = new CertEquipmentFullDto(entity.CERTEquipment, this);
                }
                if (!(sender is UserBaseDto) && entity.CONTACTUser != null)
                {
                    Technician = new UserFullDto(entity.CONTACTUser, this);
                }
                if (!(sender is EquipmentBaseDto) && entity.CONTACTEquipment != null)
                {
                    Equipment = new EquipmentFullDto(entity.CONTACTEquipment, this);
                }
            }
        }
        public virtual CERT_UploadInfo ToEntity(CERT_UploadInfo entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CERT_UploadInfo();
            }

            entity.CertUploadInfoID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["uploaddate"])
                    entity.UploadDate = UploadDate;
                if (fieldvalidation["acquisitiondate"])
                    entity.PhotoDate = AcquisitionDate;
                if (fieldvalidation["datafilelocation"])
                    entity.DataFileLocation = DataFileLocation;
                if (fieldvalidation["note"])
                    entity.InternalNote = Note;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["iscertified"])
                    entity.IsCertified = IsCertified.GetValueOrDefault(true);
                if (fieldvalidation["certequipmentid"])
                    entity.CertEquipmentID = CertEquipmentId;
                if (fieldvalidation["certuserid"])
                    entity.CertUserID = CertUserId;
                if (fieldvalidation["technicianid"])
                    entity.UserID = TechnicianId;
                if (fieldvalidation["equipmentid"])
                    entity.EquipmentID = EquipmentId;
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
        public bool? IsCertified { get; set; }
        [Range(0, long.MaxValue)]
        public long? MediaStatusId { get; set; }
        [Range(0, long.MaxValue)]
        public long? TechnicianId { get; set; }
        [Range(0, long.MaxValue)]
        public long? EquipmentId { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertEquipmentId { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertUserId { get; set; }
        public bool? HasError { get; set; }
        public string LastError { get; set; }

        public CertEquipmentFullDto CertEquipment { get; set; }
        public CertUserFullDto CertUser { get; set; }
        public UserFullDto Technician { get; set; }
        public EquipmentFullDto Equipment { get; set; }
        public MediaStatusFullDto MediaStatus { get; set; }
    }
}
