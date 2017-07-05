using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class ScheduleBaseDto
    {
        public ScheduleBaseDto()
            : this(null)
        {
        }
        public ScheduleBaseDto(PACS_TPProcList entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.TPProcID;
                TimePointId = entity.TimePointsListID;
                ProcedureId = entity.ImgProcedureID;
                WFTemplateId = entity.WFTemplateID;
                GTemplateId = entity.GTemplateID;
                CRFTemplateId = entity.CRFTemplateID;
                IsGradeBothLaterality = entity.IsGradeBothLaterality;
                PercentSeriesForReview = entity.PercentSeriesForReview;
                CounterSeriesForReview = entity.CounterSeriesForReview;
                CounterSeriesSigned = entity.CounterSeriesSigned;
                IsAttachmentsEnabled = entity.IsAttachmentsEnabled;

                if (entity.GRDGradingTemplate != null)
                {
                    GTemplateName = entity.GRDGradingTemplate.GTemplateName;
                }
                if (entity.CRFTemplate != null)
                {
                    CRFTemplateName = entity.CRFTemplate.TemplateName;
                }
                if (entity.WFTemplate != null)
                {
                    WFTemplateName = entity.WFTemplate.WFTemplateName;
                }

                if (!(sender is ProcedureBaseDto) && entity.CERTImgProcedureList != null)
                {
                    Procedure = new ProcedureFullDto(entity.CERTImgProcedureList, this);
                }
                if (!(sender is TimePointBaseDto) && entity.PACSTimePointsList != null)
                {
                    TimePoint = new TimePointFullDto(entity.PACSTimePointsList, this);
                }
            }
        }
        public virtual PACS_TPProcList ToEntity(PACS_TPProcList entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new PACS_TPProcList();
            }

            entity.TPProcID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["timepointid"])
                    entity.TimePointsListID = TimePointId;
                if (fieldvalidation["procedureid"])
                    entity.ImgProcedureID = ProcedureId;
                if (fieldvalidation["wftemplateid"])
                    entity.WFTemplateID = WFTemplateId;
                if (fieldvalidation["gtemplateid"])
                    entity.GTemplateID = GTemplateId;
                if (fieldvalidation["crftemplateid"])
                    entity.CRFTemplateID = CRFTemplateId;
                if (fieldvalidation["isgradebothlaterality"])
                    entity.IsGradeBothLaterality = IsGradeBothLaterality.GetValueOrDefault();
                if (fieldvalidation["percentseriesforreview"])
                    entity.PercentSeriesForReview = PercentSeriesForReview.GetValueOrDefault();
                if (fieldvalidation["counterseriesforreview"])
                    entity.CounterSeriesForReview = CounterSeriesForReview;
                if (fieldvalidation["counterseriessigned"])
                    entity.CounterSeriesSigned = CounterSeriesSigned;
                if (fieldvalidation["isAttachmentsEnabled"])
                {
                    entity.IsAttachmentsEnabled = IsAttachmentsEnabled;
                }

            }


            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? TimePointId { get; set; }
        [Range(0, long.MaxValue)]
        public long? ProcedureId { get; set; }
        [Range(0, long.MaxValue)]
        public long? WFTemplateId { get; set; }
        [Range(0, long.MaxValue)]
        public long? GTemplateId { get; set; }
        [Range(0, long.MaxValue)]
        public long? CRFTemplateId { get; set; }
        public bool? IsGradeBothLaterality { get; set; }
        [Range(0, short.MaxValue)]
        public short? PercentSeriesForReview { get; set; }
        [Range(0, int.MaxValue)]
        public int? CounterSeriesForReview { get; set; }
        [Range(0, int.MaxValue)]
        public int? CounterSeriesSigned { get; set; }
        public string GTemplateName { get; set; }
        public string CRFTemplateName { get; set; }
        public string WFTemplateName { get; set; }
        public bool IsAttachmentsEnabled { get; set; }

        public ProcedureFullDto Procedure { get; set; }
        public TimePointFullDto TimePoint { get; set; }
        public SeriesBaseDto Series { get; set; }
    }
}
