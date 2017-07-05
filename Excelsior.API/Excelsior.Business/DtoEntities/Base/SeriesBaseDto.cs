using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class SeriesBaseDto
    {
        public SeriesBaseDto()
            : this(null)
        {
        }
        public SeriesBaseDto(WF_Sequence entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.SeriesID;
                LateralityReceived = entity.LateralityReceived;
                SeriesGroupId = entity.SeriesGroupID;
                LastStepCompletionDate = entity.LastStepCompletionDate;
                StudyDate = entity.StudyDate;
                DirectorReviewComplete = entity.DirectorReviewComplete;
                PhotographerId = entity.PhotographerID;
                EquipmentId = entity.EquipmentID;
                LastExportDate = entity.LastExportDateTime;
                DicomInstanceUId = entity.SeriesDCMInstanceUID;

                IsActive = entity.IsActive;
                IsDataQualityAdequate = entity.IsDataQualityAdequate;
                IsQCSeries = entity.IsQCSeries;
                IsValidated = entity.IsValidated;
                WorflowTemplateStepId = entity.WFTempStepID;
                ColorCategoryId = entity.CategoryFlagID;
                AssignedToId = entity.AssignedToID;
                ScheduleId = entity.TPProcListID;

                MediaTypeId = entity.PACSTPProcList.CERTImgProcedureList.PACSDataType.DataTypeID;
                MediaTypeName = entity.PACSTPProcList.CERTImgProcedureList.PACSDataType.DataType;

                SubjectId = entity.PACSTimePoint?.SubjectID;

                TimePointName = entity.PACSTimePoint?.PACSTimePointsList?.TimePointsDescription;
                ProcedureName = entity.PACSTPProcList?.CERTImgProcedureList?.ImgProcedureName;
                SiteName = entity.PACSTimePoint?.PACSSubject?.PACSSite?.DisplayName;
                SubjectRandomizedId = entity.PACSTimePoint?.PACSSubject?.RandomizedSubjectID;
                SubjectAlternativeRandomizedId = entity.PACSTimePoint?.PACSSubject?.AlternativeRandomizedSubjectID;
                SubjectNameCode = entity.PACSTimePoint?.PACSSubject?.NameCode;
                IsTestingSubject = entity.PACSTimePoint?.PACSSubject?.IsTestingSubject;
                SubjectLaterality = entity.PACSTimePoint?.PACSSubject?.Laterality;
                ColorCategoryName = entity.WFCategoryFlag?.CategoryDes;
                WorkflowStepName = entity.WFTempStep?.WFStepList?.WFStepListDes;
                TechnicianName = (entity.CONTACTUser?.LastName + (string.IsNullOrEmpty(entity.CONTACTUser?.FirstName) ? "" : ", " + entity.CONTACTUser?.FirstName));
                EquipmentName = (entity.CONTACTEquipment?.CONTACTEquipmentModel == null) ? string.Empty : (string.Format("{0} - {1} ({2})", entity.CONTACTEquipment?.CONTACTEquipmentModel?.ManufacturerModel, entity.CONTACTEquipment?.CONTACTEquipmentModel?.EquipmentType, entity.CONTACTEquipment?.MainSerialNum));
                EquipmentManufacturer = (entity.CONTACTEquipment?.CONTACTEquipmentModel == null) ? string.Empty : entity.CONTACTEquipment?.CONTACTEquipmentModel?.ManufacturerName;
                AssignedToName = (entity.AssignedTo?.LastName + (string.IsNullOrEmpty(entity.AssignedTo?.FirstName) ? "" : ", " + entity.AssignedTo?.FirstName));

                if (!(sender is ScheduleBaseDto) && entity.PACSTPProcList != null)
                {
                    Schedule = new ScheduleFullDto(entity.PACSTPProcList, this);
                }
            }
        }
        public virtual WF_Sequence ToEntity(WF_Sequence entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new WF_Sequence();
            }
             
            entity.SeriesID = Id.GetValueOrDefault();

            using (var fieldvalidation = new FieldValidation(fields))
            {                 
                if (fieldvalidation["lateralityreceived"])
                    entity.LateralityReceived = LateralityReceived;
                if (fieldvalidation["seriesgroupid"])
                    entity.SeriesGroupID = SeriesGroupId;
                if (fieldvalidation["laststepcompletionDate"])
                    entity.LastStepCompletionDate = LastStepCompletionDate;
                if (fieldvalidation["studydate"])
                    entity.StudyDate = StudyDate;
                if (fieldvalidation["directorreviewcomplete"])
                    entity.DirectorReviewComplete = DirectorReviewComplete;
                if (fieldvalidation["photographerid"])
                    entity.PhotographerID = PhotographerId;
                if (fieldvalidation["equipmentid"])
                    entity.EquipmentID = EquipmentId;
                if (fieldvalidation["lastexportdate"])
                    entity.LastExportDateTime = LastExportDate;
                if (fieldvalidation["dicominstanceuid"])
                    entity.SeriesDCMInstanceUID = DicomInstanceUId;

                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive.GetValueOrDefault(true);
                if (fieldvalidation["isdataqualityadequate"])
                    entity.IsDataQualityAdequate = IsDataQualityAdequate.GetValueOrDefault(true);
                if (fieldvalidation["isqcseries"])
                    entity.IsQCSeries = IsQCSeries.GetValueOrDefault();
                if (fieldvalidation["isvalidated"])
                    entity.IsValidated = IsValidated.GetValueOrDefault(true);
                if (fieldvalidation["worflowtemplatestepid"])
                    entity.WFTempStepID = WorflowTemplateStepId;
                if (fieldvalidation["colorcategoryid"])
                    entity.CategoryFlagID = ColorCategoryId;
                if (fieldvalidation["assignedtoid"])
                    entity.AssignedToID = AssignedToId;
                if (fieldvalidation["scheduleid"])
                    entity.TPProcListID = ScheduleId;
            }            

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [StringLength(64)]
        public string DicomInstanceUId { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? StudyDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastStepCompletionDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? LastExportDate { get; set; }
        [Range(0, long.MaxValue)]
        public bool? IsActive { get; set; }
        public bool? DirectorReviewComplete { get; set; }
        public bool? IsDataQualityAdequate { get; set; }
        public bool? IsQCSeries { get; set; }
        public bool? IsValidated { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesGroupId { get; set; }
        [StringLength(10)]
        public string LateralityReceived { get; set; }
        [Range(0, long.MaxValue)]
        public long? PhotographerId { get; set; }
        [Required]
        [Range(0, long.MaxValue)]
        public long? EquipmentId { get; set; }
        [Required]
        [Range(0, long.MaxValue)]
        public long? ScheduleId { get; set; }
        [Range(0, long.MaxValue)]
        public long? AssignedToId { get; set; }
        [Range(0, long.MaxValue)]
        public long? WorflowTemplateStepId { get; set; }
        [Range(0, long.MaxValue)]
        public long? ColorCategoryId { get; set; }

        [Range(0, long.MaxValue)]
        public long? MediaTypeId { get; set; }
        public string MediaTypeName { get; set; }
        [Range(0, long.MaxValue)]
        public long? SubjectId { get; set; }
        public string ColorCategoryName { get; set; }
        public string SiteName { get; set; }
        public string SubjectRandomizedId { get; set; }
        public string SubjectAlternativeRandomizedId { get; set; }
        public string SubjectNameCode { get; set; }
        public string SubjectLaterality { get; set; }
        public string TimePointName { get; set; }
        public string ProcedureName { get; set; }
        public string TechnicianName { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentManufacturer { get; set; }
        public string WorkflowStepName { get; set; }
        public string AssignedToName { get; set; }
        public bool? IsTechnicianCerified { get; set; }
        public bool? IsEquipmentCerified { get; set; }
        public bool? IsTestingSubject { get; set; }
        public string SegmentationStatus { get; set; }
        public int? TotalComments { get; set; }
        public int? TotalUploads { get; set; }
        public int? TotalMedia { get; set; }
        public int TotalQueriesPending { get; set; }
        public int TotalQueriesFlagged { get; set; }

        public ScheduleFullDto Schedule { get; set; }
    }
}
