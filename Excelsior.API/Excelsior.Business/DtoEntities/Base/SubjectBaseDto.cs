using Excelsior.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Excelsior.Infrastructure.Utilities;

namespace Excelsior.Business.DtoEntities.Base
{
    public class SubjectBaseDto : IValidatableObject
    {
        public SubjectBaseDto()
            : this(null)
        {
        }
        public SubjectBaseDto(PACS_Subject entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.SubjectID;
                SiteId = entity.SiteID;
                RandomizedId = entity.RandomizedSubjectID;
                AlternativeRandomizedId = entity.AlternativeRandomizedSubjectID;
                Laterality = entity.Laterality;
                NameCode = entity.NameCode;
                Gender = entity.Gender;
                BirthYear = entity.BirthYear;
                EnrollmentDate = entity.SubjectEnrollmentDate;
                IsActive = entity.IsActive;
                IsValidated = entity.IsValidated;
                IsRejected = entity.IsSubjectRejected;
                IsTesting = entity.IsTestingSubject;
                IsDismissed = entity.IsDismissed;
            }
        }
        public virtual PACS_Subject ToEntity(PACS_Subject entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new PACS_Subject();
            }

            entity.SubjectID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["siteid"])
                    entity.SiteID = SiteId;
                if (fieldvalidation["randomizedid"])
                    entity.RandomizedSubjectID = RandomizedId;
                if (fieldvalidation["alternativerandomizedid"])
                    entity.AlternativeRandomizedSubjectID = AlternativeRandomizedId;
                if (fieldvalidation["laterality"])
                    entity.Laterality = Laterality;
                if (fieldvalidation["gender"])
                    entity.Gender = Gender;
                if (fieldvalidation["birthyear"])
                    entity.BirthYear = BirthYear;
                if (fieldvalidation["namecode"])
                    entity.NameCode = NameCode;
                if (fieldvalidation["enrollmentdate"])
                    entity.SubjectEnrollmentDate = EnrollmentDate;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["isvalidated"])
                    entity.IsValidated = IsValidated.GetValueOrDefault();
                if (fieldvalidation["isrejected"])
                    entity.IsSubjectRejected = IsRejected.GetValueOrDefault();
                if (fieldvalidation["istesting"])
                    entity.IsTestingSubject = IsTesting.GetValueOrDefault();
                if (fieldvalidation["isdismissed"])
                    entity.IsDismissed = IsDismissed.GetValueOrDefault(); 
            }
            

            return entity;
        }

        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? SiteId { get; set; }
        [StringLength(50)]
        public string RandomizedId { get; set; }
        [StringLength(50)]
        public string AlternativeRandomizedId { get; set; }
        [StringLength(10)]
        public string NameCode { get; set; }
        [StringLength(10)]
        public string Laterality { get; set; }
        [StringLength(10)]
        public string Gender { get; set; }
        [Range(0, int.MaxValue)]
        public int? BirthYear { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? EnrollmentDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsValidated { get; set; }
        public bool? IsRejected { get; set; }
        public bool? IsTesting { get; set; }
        public bool? IsDismissed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RandomizedId == null && AlternativeRandomizedId == null)
                yield return new ValidationResult("RandomizedId or AlternativeRandomizedId field is required.");
        }

    }
}
