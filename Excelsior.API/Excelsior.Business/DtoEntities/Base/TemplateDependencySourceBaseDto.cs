using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class TemplateDependencySourceBaseDto
    {
        public TemplateDependencySourceBaseDto()
            : this(null)
        {

        }
        public TemplateDependencySourceBaseDto(CRF_TemplateDependencySource entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.CRFTemplateDependencySourceID;
                DependencyID = entity.CRFTemplateDependencyID;
                SourceAnswerId = entity.SourceAnswerID;
                SourceQuestionId = entity.SourceQuestionID;
            }
        }
        public virtual CRF_TemplateDependencySource ToEntity(CRF_TemplateDependencySource entity = null)
        {
            if (entity == null)
            {
                entity = new CRF_TemplateDependencySource();
            }

            entity.CRFTemplateDependencySourceID = Id.GetValueOrDefault();
            entity.CRFTemplateDependencyID = DependencyID;
            entity.SourceAnswerID = SourceAnswerId;
            entity.SourceQuestionID = SourceQuestionId;
            return entity;
        }

        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? DependencyID { get; set; }
        [Range(0, long.MaxValue)]
        public long? SourceAnswerId { get; set; }
        [Range(0, long.MaxValue)]
        public long? SourceQuestionId { get; set; }
    }
}
