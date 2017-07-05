using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{
    public class GradingAnswerFullDto : GradingAnswerBaseDto
    {
        public GradingAnswerFullDto()
            : this(null)
        {
            Dependencies = new List<GradingDependencyFullDto>();
        }
        public GradingAnswerFullDto(GRD_GradingAnswer entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            { 
            }
        } 
        public override GRD_GradingAnswer ToEntity(GRD_GradingAnswer entity = null)
        {
            entity = base.ToEntity(entity);

            return entity;
        }

        public List<GradingDependencyFullDto> Dependencies { get; set; }
    }
}
