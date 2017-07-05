using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class GradingResultBaseDto
    {
        public GradingResultBaseDto()
            : this(null)
        {
        }
        public GradingResultBaseDto(GRD_ReportResult entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.GRDReportResultID;
                ReportId = entity.GReportID;
                GroupName = entity.GQuestionGroupName;
                QuestionString = entity.GQuestionString;
                QuestionDescription = entity.GQuestionDes;
                AnswerString = entity.GAnswersString;
                MeasurementId = entity.GAnswerMeasurement;
                Laterality = entity.Laterality;

                if (!(sender is MeasurementBaseDto) && entity.MEAMeasurement != null)
                {
                    Measurement = new MeasurementBaseDto(entity.MEAMeasurement);
                }
            }
        }
        public virtual GRD_ReportResult ToEntity(GRD_ReportResult entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new GRD_ReportResult();
            }

            entity.GRDReportResultID = Id;
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["reportid"])
                    entity.GReportID = ReportId;
                if (fieldvalidation["groupname"])
                    entity.GQuestionGroupName = GroupName;
                if (fieldvalidation["questionstring"])
                    entity.GQuestionString = QuestionString;
                if (fieldvalidation["questiondescription"])
                    entity.GQuestionDes = QuestionDescription;
                if (fieldvalidation["answerstring"])
                    entity.GAnswersString = AnswerString;
                if (fieldvalidation["measurementId"])
                    entity.GAnswerMeasurement = MeasurementId;
                if (fieldvalidation["laterality"])
                    entity.Laterality = Laterality;
            }
            return entity;
        }

        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? ReportId { get; set; }
        public string GroupName { get; set; }
        public string QuestionString { get; set; }
        public string QuestionDescription { get; set; }
        public string AnswerString { get; set; }
        [StringLength(10)]
        public string Laterality { get; set; }
        public long? MeasurementId { get; set; }

        public MeasurementBaseDto Measurement { get; set; }
    }
}
