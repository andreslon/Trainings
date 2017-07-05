using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class CertEquipmentBaseDto
    {
        public CertEquipmentBaseDto()
            : this(null)
        {

        }
        public CertEquipmentBaseDto(CERT_Equipment entity, object sender = null)
        {
            if (entity != null)
            {
                StudyId = entity.TrialID;
                ProcedureId = entity.ImgProcedureID;
                EquipmentId = entity.EquipmentID;
                CreatedDate = entity.DateCreated;
                CertifiedDate = entity.DateofCertification;
                IsCertified = entity.IsCertified;
                Id = entity.CertEquipmentID;
                PixelSpacingY = entity.PixelSpacingY;
                PixelSpacingX = entity.PixelSpacingX;
                FrameSpacing = entity.FrameSpacing;
                IsActive = entity.IsActive;
                CertifiedById = entity.CertifiedByID;
                AssignedToId = entity.AssignedToID;
                StudyName = entity.PACSTrial.TrialName;
                StudyAlias = entity.PACSTrial.TrialAlias;

                if (entity.CONTACTEquipment != null)
                {
                    Equipment = new EquipmentFullDto(entity.CONTACTEquipment, this);
                }
                if (entity.CERTImgProcedureList != null)
                {
                    Procedure = new ProcedureFullDto(entity.CERTImgProcedureList, this);
                }
                if (entity.CertifiedBy != null)
                {
                    CertifiedBy = new UserFullDto(entity.CertifiedBy, this);
                }
                if (entity.AssignedTo != null)
                {
                    AssignedTo = new UserFullDto(entity.AssignedTo, this);
                }
            }
        }
        public virtual CERT_Equipment ToEntity(CERT_Equipment entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CERT_Equipment();
            }

            entity.CertEquipmentID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["studyid"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["procedureid"])
                    entity.ImgProcedureID = ProcedureId;
                if (fieldvalidation["equipmentid"])
                    entity.EquipmentID = EquipmentId;
                if (fieldvalidation["createddate"])
                    entity.DateCreated = CreatedDate;
                if (fieldvalidation["certifieddate"])
                    entity.DateofCertification = CertifiedDate;
                if (fieldvalidation["pixelspacingy"])
                    entity.PixelSpacingY = PixelSpacingY;
                if (fieldvalidation["pixelspacingx"])
                    entity.PixelSpacingX = PixelSpacingX;
                if (fieldvalidation["framespacing"])
                    entity.FrameSpacing = FrameSpacing;
                if (fieldvalidation["iscertified"])
                    entity.IsCertified = IsCertified.GetValueOrDefault();
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault();
                if (fieldvalidation["certifiedbyid"])
                    entity.CertifiedByID = CertifiedById;
                if (fieldvalidation["assignedtoid"])
                    entity.AssignedToID = AssignedToId;
            }
            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        [Range(0, long.MaxValue)]
        public long? ProcedureId { get; set; }
        [Range(0, long.MaxValue)]
        public long? EquipmentId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CertifiedDate { get; set; }
        public bool? IsCertified { get; set; }
        [Range(0, double.MaxValue)]
        public double? PixelSpacingY { get; set; }
        [Range(0, double.MaxValue)]
        public double? PixelSpacingX { get; set; }
        [Range(0, double.MaxValue)]
        public double? FrameSpacing { get; set; }
        public bool? IsActive { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertifiedById { get; set; }
        [Range(0, long.MaxValue)]
        public long? AssignedToId { get; set; }

        public int TotalUploads { get; set; }
        public int TotalQueriesPending { get; set; }
        public int TotalQueriesFlagged { get; set; }
        public bool hasPrevCert { get; set; }
        public string StudyName { get; set; }
        public string StudyAlias { get; set; }
        public DateTime? LastSubmissionDate { get; set; }

        public ProcedureFullDto Procedure { get; set; }
        public EquipmentFullDto Equipment { get; set; }
        public UserFullDto CertifiedBy { get; set; }
        public UserFullDto AssignedTo { get; set; }
    }
}
