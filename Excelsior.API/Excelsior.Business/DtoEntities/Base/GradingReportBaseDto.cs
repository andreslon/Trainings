using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class GradingReportBaseDto
    {
        public GradingReportBaseDto()
            : this(null)
        {
        }
        public GradingReportBaseDto(GRD_Report entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.GReportID;
                TemplateId = entity.GTemplateID;
                SeriesId = entity.SeriesID;
                TemplateName = entity.GRDGradingTemplate?.GTemplateName;
                TimePointDescription = entity.PACSSeries?.PACSTimePoint?.PACSTimePointsList?.TimePointsDescription;
                TimePointId = entity.PACSSeries?.PACSTimePoint?.PACSTimePointsList?.TimePointsListID;

                PerformedDate = entity.PerformedDate;
                PerformedById = entity.PerformedBy;

                IsActive = entity.IsActive;
                IsSigned = entity.IsSigned;
                IsPrimary = entity.IsPrimaryResult;

                if (entity.CONTACTUser != null)
                {
                    PerformedBy = new UserFullDto(entity.CONTACTUser, this);
                }
            }
        }
        public virtual GRD_Report ToEntity(GRD_Report entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new GRD_Report();
            }

            entity.GReportID = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["templateid"])
                    entity.GTemplateID = TemplateId;
                if (fieldvalidation["seriesId"])
                    entity.SeriesID = SeriesId;
                if (fieldvalidation["performeddate"])
                    entity.PerformedDate = PerformedDate;
                if (fieldvalidation["performedbyId"])
                    entity.PerformedBy = PerformedById;
                if (fieldvalidation["isactive"])
                    entity.IsActive = IsActive;
                if (fieldvalidation["issigned"])
                    entity.IsSigned = IsSigned;
                if (fieldvalidation["isprimaryresult"])
                    entity.IsPrimaryResult = IsPrimary;
            }

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? TemplateId { get; set; }
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }
        [Range(0, long.MaxValue)]
        public long? PerformedById { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? PerformedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsSigned { get; set; }
        public bool IsPrimary { get; set; }

        public string TemplateName { get; set; }
        public string TimePointDescription { get; set; }
        public long? TimePointId { get; set; }

        public UserFullDto PerformedBy { get; set; }
    }
}
