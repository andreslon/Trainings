using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AuditRecordBaseDto
    {
        public AuditRecordBaseDto()
            : this(null)
        {
        }
        public AuditRecordBaseDto(AUDIT_Record entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.AuditRecordID;
                Reason = entity.ReasonForChange;
                Details = entity.DetailsXML;
                PerformedDate = entity.PerformedDateTime;
                PerformedById = entity.UserID;
                AuditActionId = entity.AuditActionID;
                StudyId = entity.TrialID;
                SeriesId = entity.SeriesID;
                WorkflowTemplateStepId = entity.WFTempStepID;
                CertUserId = entity.CertUserID;
                CertEquipmentId = entity.CertEquipmentID;
                RelatedUserId = entity.RelatedUserID;
                SeriesId = entity.SeriesID;
                SeriesId = entity.SeriesID;
                SeriesId = entity.SeriesID;

                if (entity.CONTACTUser != null)
                {
                    PerformedBy = new UserFullDto(entity.CONTACTUser, this);
                }
                if (!(sender is AuditActionBaseDto) && entity.AUDITAction != null)
                {
                    AuditAction = new AuditActionFullDto(entity.AUDITAction, this);
                }
                if (!(sender is StudyBaseDto) && entity.PACSTrial != null)
                {
                    Study = new StudyFullDto(entity.PACSTrial, this);
                }
                if (!(sender is SeriesBaseDto) && entity.PACSSeries != null)
                {
                    Series = new SeriesFullDto(entity.PACSSeries as WF_Sequence, this);
                }
                if (!(sender is WorkflowTemplateStepBaseDto) && entity.WFTempStep != null)
                {
                    WorkflowTemplateStep = new WorkflowTemplateStepFullDto(entity.WFTempStep);
                }
                if (!(sender is CertUserBaseDto) && entity.CERTUser != null)
                {
                    CertUser = new CertUserFullDto(entity.CERTUser, this);
                }
                if (!(sender is CertEquipmentBaseDto) && entity.CERTEquipment != null)
                {
                    CertEquipment = new CertEquipmentFullDto(entity.CERTEquipment, this);
                }
                if (!(sender is SubjectBaseDto) && entity.PACSSubject != null)
                {
                    Subject = new SubjectFullDto(entity.PACSSubject, this);
                }
                if (!(sender is UserBaseDto) && entity.RelatedUser != null)
                {
                    RelatedUser = new UserFullDto(entity.RelatedUser, this);
                }
            }
        }
        public virtual AUDIT_Record ToEntity(AUDIT_Record entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new AUDIT_Record();
            }
            entity.AuditRecordID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["actionid"])
                    entity.AuditActionID = AuditActionId;
                if (fieldvalidation["reason"])
                    entity.ReasonForChange = Reason;
                if (fieldvalidation["details"])
                    entity.DetailsXML = Details;
                if (fieldvalidation["performeddate"])
                    entity.PerformedDateTime = PerformedDate;
                if (fieldvalidation["performedbyid"])
                    entity.UserID = PerformedById;
                if (fieldvalidation["actionid"])
                    entity.AuditActionID = AuditActionId;
                if (fieldvalidation["studyid"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["seriesid"])
                    entity.SeriesID = SeriesId;
                if (fieldvalidation["worflowtemplatestepid"])
                    entity.WFTempStepID = WorkflowTemplateStepId;
                if (fieldvalidation["certuserid"])
                    entity.CertUserID = CertUserId;
                if (fieldvalidation["certequipmentid"])
                    entity.CertEquipmentID = CertEquipmentId;
                if (fieldvalidation["subjectid"])
                    entity.SubjectID = SubjectId;
                if (fieldvalidation["relateduserid"])
                    entity.RelatedUserID = RelatedUserId;
            }

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        public string Reason { get; set; }
        public string Details { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? PerformedDate { get; set; }
        [Range(0, long.MaxValue)]
        public long? PerformedById { get; set; }
        [Range(0, long.MaxValue)]
        public long? AuditActionId { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }
        [Range(0, long.MaxValue)]
        public long? WorkflowTemplateStepId { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertUserId { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertEquipmentId { get; set; }
        [Range(0, long.MaxValue)]
        public long? SubjectId { get; set; }
        [Range(0, long.MaxValue)]
        public long? RelatedUserId { get; set; }

        public UserFullDto PerformedBy { get; set; }
        public AuditActionBaseDto AuditAction { get; set; }
        public StudyFullDto Study { get; set; }
        public SeriesFullDto Series { get; set; }
        public WorkflowTemplateStepFullDto WorkflowTemplateStep { get; set; }
        public CertUserFullDto CertUser { get; set; }
        public CertEquipmentFullDto CertEquipment { get; set; }
        public SubjectFullDto Subject { get; set; }
        public UserFullDto RelatedUser { get; set; }
    }
}