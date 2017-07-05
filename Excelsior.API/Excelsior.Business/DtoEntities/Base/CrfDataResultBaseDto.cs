using Excelsior.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Base
{
    public class CrfDataResultBaseDto
    {
        public CrfDataResultBaseDto()
            : this(null)
        {
        }
        public CrfDataResultBaseDto(CRF_DataResult entity, object sender = null)
        {
            Answers = new List<TemplateAnswerBaseDto>();

            if (entity != null)
            {
                Id = entity.CRFDataResultID;
                AnswerString = entity.AnswerString;
                IsActive = entity.IsActive;
                Laterality = entity.Laterality;
                CrfDataId = entity.CRFDataID;
                TemplateQuestionId = entity.CRFTemplateQuestionID;
                MeasurementId = entity.MeasurementID;

                if (!(sender is TemplateQuestionBaseDto) && entity.CRFTemplateQuestion != null)
                {
                    TemplateQuestion = new TemplateQuestionBaseDto(entity.CRFTemplateQuestion, this);
                }

                if (!(sender is TemplateAnswerBaseDto) && entity.CRF_TemplateAnswers.Count > 0)
                {
                    Answers = new List<TemplateAnswerBaseDto>();
                    foreach (var item in entity.CRF_TemplateAnswers.OrderBy(x => x.AnswerSeq))
                    {
                        Answers.Add(new TemplateAnswerBaseDto(item, this));
                    }
                }
            }
        }
        public virtual CRF_DataResult ToEntity(CRF_DataResult entity = null)
        {
            if (entity == null)
            {
                entity = new CRF_DataResult();
            }

            entity.CRFDataResultID = Id;
            entity.AnswerString = AnswerString;
            entity.IsActive = IsActive.GetValueOrDefault(true);
            entity.Laterality = Laterality;
            entity.CRFDataID = CrfDataId;
            entity.CRFTemplateQuestionID = TemplateQuestionId;
            entity.MeasurementID = MeasurementId;

            return entity;
        }

        public long Id { get; set; }
        public string AnswerString { get; set; }
        [StringLength(10)]
        public string Laterality { get; set; }
        public bool? IsActive { get; set; }
        public long? CrfDataId { get; set; }
        public long? TemplateQuestionId { get; set; }
        public long? MeasurementId { get; set; }
        public TemplateQuestionBaseDto TemplateQuestion { get; set; }
        public List<TemplateAnswerBaseDto> Answers { get; set; }
    }
}