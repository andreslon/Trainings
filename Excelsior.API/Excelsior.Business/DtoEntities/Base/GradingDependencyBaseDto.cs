using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class GradingDependencyBaseDto
    {
        public GradingDependencyBaseDto()
            : this(null)
        {
        }
        public GradingDependencyBaseDto(GRD_Dependency entity, object sender = null)
        {
            if (entity != null)
            {

                Id = entity.GDependencyID;
                IsActionEnable = entity.ActionEnable;
                SourceAnswerID = entity.GSourceAnswerID;
                TargetAnswerID = entity.GTargetAnswerID;
                TargetQuestionID = entity.GTargetQuestionID;
            }
        }
        public virtual GRD_Dependency ToEntity(GRD_Dependency entity = null)
        {
            if (entity == null)
            {
                entity = new GRD_Dependency();
            }

            entity.GDependencyID = Id.GetValueOrDefault();
            entity.ActionEnable = IsActionEnable.GetValueOrDefault();
            entity.GSourceAnswerID = SourceAnswerID;
            entity.GTargetAnswerID = TargetAnswerID;
            entity.GTargetQuestionID = TargetQuestionID;

            return entity;
        }

        public long? Id { get; set; }
        public bool? IsActionEnable { get; set; }
        [Range(0, long.MaxValue)]
        public long? SourceAnswerID { get; set; }
        [Range(0, long.MaxValue)]
        public long? TargetAnswerID { get; set; }
        [Range(0, long.MaxValue)]
        public long? TargetQuestionID { get; set; }
    }
}
