using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Full
{
    public class StudyFullDto : StudyBaseDto
    {
        public StudyFullDto()
            : this(null)
        {

        }
        public StudyFullDto(PACS_Trial entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                OtherDrugs = entity.OtherDrugs;
                Arm = entity.TrialArm;
                DiseaseType = entity.DiseaseType;
                Phase = entity.TrialPhase;
                ProtocolTitle = entity.ProtocolTitle;
                SubjectSeg = entity.SubjectSeg;
                TherapeuticClass = entity.TherapeuticClass;
                Locations = entity.TrialLocations;
                SubjectAlternativeIdMask = entity.SubjectAlternativeIDMask;
                SubjectIdMask = entity.SubjectIDMask;
                SubjectNameCodeMask = entity.SubjectNameCodeMask;

                IsCompletedPublic = entity.IsCompletedPublic;
                IsEligibilityCloningEnabled = entity.IsEligibilityCloningEnabled;
                IsEligibilityIdUsed = entity.IsEligibilityIDUsed;
                IsSubjectBirthYearRequired = entity.IsSubjectBirthYearRequired;
                IsSubjectGenderRequired = entity.IsSubjectGenderRequired;
                IsSubjectNameCodeRequired = entity.IsSubjectNameCodeRequired;
                IsTestingPhase = entity.IsTestingPhase;
                IsValidated = entity.IsValidated;
                ShouldEligibleLateralityBeDetermined = entity.ShouldEligibleLateralityBeDetermined;
                AlwaysVerifyMultipleGrades = entity.AlwaysVerifyMultipleGrades;
                NeedCertification = entity.NeedCertification;

                FirstDataExportDate = entity.FirstDataExportDate;
                FirstSubjectEnrollDate = entity.FirstSubjectEnrollDate;
                LastDataExportDate = entity.LastDataExportDate;
                LastSubjectEnrollDate = entity.LastSubjectEnrollDate;
                LastSubjectVisitDate = entity.LastSubjectVisitDate;
            }
        }
        public override PACS_Trial ToEntity(PACS_Trial entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            entity.OtherDrugs = OtherDrugs;
            entity.ProtocolTitle = ProtocolTitle;
            entity.TrialPhase = Phase;
            entity.TrialArm = Arm;
            entity.DiseaseType = DiseaseType;
            entity.SubjectSeg = SubjectSeg;
            entity.TrialLocations = Locations;
            entity.SubjectIDMask = SubjectIdMask;
            entity.SubjectAlternativeIDMask = SubjectAlternativeIdMask;
            entity.SubjectNameCodeMask = SubjectNameCodeMask;
            entity.TherapeuticClass = TherapeuticClass;

            entity.IsValidated = IsValidated.GetValueOrDefault();
            entity.IsSubjectNameCodeRequired = IsSubjectNameCodeRequired.GetValueOrDefault();
            entity.IsEligibilityIDUsed = IsEligibilityIdUsed.GetValueOrDefault();
            entity.IsCompletedPublic = IsCompletedPublic.GetValueOrDefault();
            entity.IsTestingPhase = IsTestingPhase.GetValueOrDefault();
            entity.IsEligibilityCloningEnabled = IsEligibilityCloningEnabled.GetValueOrDefault();
            entity.ShouldEligibleLateralityBeDetermined = ShouldEligibleLateralityBeDetermined.GetValueOrDefault();
            entity.IsSubjectGenderRequired = IsSubjectGenderRequired.GetValueOrDefault();
            entity.IsSubjectBirthYearRequired = IsSubjectBirthYearRequired.GetValueOrDefault();
            entity.AlwaysVerifyMultipleGrades = AlwaysVerifyMultipleGrades.GetValueOrDefault();
            entity.NeedCertification = NeedCertification.GetValueOrDefault();

            entity.FirstSubjectEnrollDate = FirstSubjectEnrollDate;
            entity.LastSubjectEnrollDate = LastSubjectEnrollDate;
            entity.LastSubjectVisitDate = LastSubjectVisitDate;
            entity.FirstDataExportDate = FirstDataExportDate;
            entity.LastDataExportDate = LastDataExportDate;

            return entity;
        }
        public string OtherDrugs { get; set; }
        public string ProtocolTitle { get; set; }
        [StringLength(256)]
        public string Phase { get; set; }
        [StringLength(256)]
        public string Arm { get; set; }
        [StringLength(256)]
        public string DiseaseType { get; set; }
        [StringLength(256)]
        public string SubjectSeg { get; set; }
        [StringLength(256)]
        public string TherapeuticClass { get; set; }
        public string Locations { get; set; }
        [StringLength(64)]
        public string SubjectIdMask { get; set; }
        [StringLength(64)]
        public string SubjectAlternativeIdMask { get; set; }
        [StringLength(64)]
        public string SubjectNameCodeMask { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? FirstSubjectEnrollDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastSubjectEnrollDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastSubjectVisitDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? FirstDataExportDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastDataExportDate { get; set; }
        public bool? IsValidated { get; set; }
        public bool? IsSubjectNameCodeRequired { get; set; }
        public bool? IsEligibilityIdUsed { get; set; }
        public bool? IsCompletedPublic { get; set; }
        public bool? IsTestingPhase { get; set; }
        public bool? IsEligibilityCloningEnabled { get; set; }
        public bool? ShouldEligibleLateralityBeDetermined { get; set; }
        public bool? IsSubjectGenderRequired { get; set; }
        public bool? IsSubjectBirthYearRequired { get; set; }
        public bool? AlwaysVerifyMultipleGrades { get; set; }
        public bool? NeedCertification { get; set; }
    }
}
