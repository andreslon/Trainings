using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class GradingTemplateQuestionBaseDto
    {
        public GradingTemplateQuestionBaseDto()
            : this(null)
        {
        }
        public GradingTemplateQuestionBaseDto(GRD_TempQuestion entity, object sender = null)
        {
            if (entity != null)
            {

                Id = entity.GTempQuestionID;
                Index = entity.GTempQuestionSeqInGroup;
            }
        }
        public virtual GRD_TempQuestion ToEntity(GRD_TempQuestion entity = null)
        {
            if (entity == null)
            {
                entity = new GRD_TempQuestion();
            }
            entity.GTempQuestionID = Id.GetValueOrDefault();
            entity.GTempQuestionSeqInGroup = Index.GetValueOrDefault();
            return entity;
        }
        public long? Id { get; set; }
        [Range(0, int.MaxValue)]
        public int? Index { get; set; }
    }
}
