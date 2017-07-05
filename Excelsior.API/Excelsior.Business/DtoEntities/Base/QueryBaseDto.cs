using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class QueryBaseDto
    {
        public QueryBaseDto()
            : this(null)
        {
        }
        public QueryBaseDto(QRY_Query entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.QueryID;
                CreatedById = entity.SenderID;
                StatusId = entity.StatusID;
                StudyId = entity.TrialID;
                SeriesId = entity.SeriesID;
                CertUserId = entity.CertUserID;
                CertEquipmentId = entity.CertEquipmentID;
                Title = entity.Subject;
                IsActive = entity.IsActive;
                IsResolved = entity.IsResolved;
                DateCreated = entity.InitiateDate;
                DateResolved = entity.DateResolved;

                if (entity.QRYStatus != null)
                {
                    Status = entity.QRYStatus.StatusName;
                }
                if (!(sender is UserBaseDto) && entity.Sender != null)
                {
                    CreatedBy = new UserFullDto(entity.Sender, this);
                }
                if (!(sender is SeriesBaseDto) && entity.PACSSeries != null)
                {
                    Series = new SeriesFullDto(entity.PACSSeries as WF_Sequence, this);
                }
                if (!(sender is CertUserBaseDto) && entity.CERTUser != null)
                {
                    CertUser = new CertUserFullDto(entity.CERTUser, this);
                }
                if (!(sender is CertEquipmentBaseDto) && entity.CERTEquipment != null)
                {
                    CertEquipment = new CertEquipmentFullDto(entity.CERTEquipment, this);
                }
            }
        }
        public virtual QRY_Query ToEntity(QRY_Query entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new QRY_Query();
            }
            entity.QueryID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["createdbyid"])
                    entity.SenderID = CreatedById;
                if (fieldvalidation["statusid"])
                    entity.StatusID = StatusId;
                if (fieldvalidation["studyid"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["seriesid"])
                    entity.SeriesID = SeriesId;
                if (fieldvalidation["certuserid"])
                    entity.CertUserID = CertUserId;
                if (fieldvalidation["certequipmentid"])
                    entity.CertEquipmentID = CertEquipmentId;
                if (fieldvalidation["title"])
                    entity.Subject = Title;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["isresolved"])
                    entity.IsResolved = IsResolved.GetValueOrDefault(true);
                if (fieldvalidation["datecreated"])
                    entity.InitiateDate = DateCreated;
                if (fieldvalidation["dateresolved"])
                    entity.DateResolved = DateResolved;
            }

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? CreatedById { get; set; }
        [Range(0, long.MaxValue)]
        public long? StatusId { get; set; }
        [Range(0, long.MaxValue)]
        [Required]
        public long? StudyId { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertUserId { get; set; }
        [Range(0, long.MaxValue)]
        public long? CertEquipmentId { get; set; }
        [Required]
        public string Title { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsResolved { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateResolved { get; set; }

        public string QueryType
        {
            get
            {
                if (SeriesId.HasValue)
                    return "Imaging";
                else if (CertUserId.HasValue || CertEquipmentId.HasValue)
                    return "Certification";
                else
                    return "Other";
            }
        }
        public string Status { get; set; }
        public bool IsFlagged { get; set; }
        public int TotalMessages { get; set; }
        public QueryMessageFullDto LastMessage { get; set; }

        public UserFullDto CreatedBy { get; set; }
        public SeriesFullDto Series { get; set; }
        public CertUserFullDto CertUser { get; set; }
        public CertEquipmentFullDto CertEquipment { get; set; }
    }
}