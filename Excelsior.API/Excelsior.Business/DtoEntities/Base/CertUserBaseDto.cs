using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class CertUserBaseDto
    {
        public CertUserBaseDto()
            : this(null)
        {

        }
        public CertUserBaseDto(CERT_User entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CertUserID;
                ProcedureId = entity.ImgProcedureID;
                StudyUserId = entity.UserTrialID;
                CreatedDate = entity.DateCreated;
                CertifiedDate = entity.DateofCertification;
                IsCertified = entity.IsCertified;
                IsActive = entity.IsActive;
                CertifiedById = entity.CertifiedByID;
                AssignedToId = entity.AssignedToID;

                if (entity.CONTACTUserTrial != null)
                {
                    StudyUser = new StudyUserFullDto(entity.CONTACTUserTrial, this);
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
        public virtual CERT_User ToEntity(CERT_User entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new CERT_User();
            }
            entity.CertUserID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["studyuserid"])
                    entity.UserTrialID = StudyUserId;
                if (fieldvalidation["createddate"])
                    entity.DateCreated = CreatedDate;
                if (fieldvalidation["certifieddate"])
                    entity.DateofCertification = CertifiedDate;
                if (fieldvalidation["iscertified"])
                    entity.IsCertified = IsCertified.GetValueOrDefault();
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault();
                if (fieldvalidation["certifiedbyid"])
                    entity.CertifiedByID = CertifiedById;
                if (fieldvalidation["assignedtoid"])
                    entity.AssignedToID = AssignedToId;
                if (fieldvalidation["procedureid"])
                    entity.ImgProcedureID = ProcedureId; 
            } 
            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyUserId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? CertifiedDate { get; set; }
        public bool? IsCertified { get; set; }
        [Range(0, long.MaxValue)]
        public long? ProcedureId { get; set; }
        public bool? IsActive { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertifiedById { get; set; }
        [Range(0, long.MaxValue)]
        public long? AssignedToId { get; set; }

        public int TotalUploads { get; set; }
        public int TotalQueriesPending { get; set; }
        public int TotalQueriesFlagged { get; set; }
        public bool hasPrevCert { get; set; }
        public DateTime? LastSubmissionDate { get; set; }

        public ProcedureFullDto Procedure { get; set; }
        public StudyUserFullDto StudyUser { get; set; }
        public UserFullDto CertifiedBy { get; set; }
        public UserFullDto AssignedTo { get; set; }
    }
}
