using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class GradingDependencyFullDto : GradingDependencyBaseDto
    {
        public GradingDependencyFullDto()
            : this(null)
        {   }
        public GradingDependencyFullDto(GRD_Dependency entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                
            }
        }
        public override GRD_Dependency ToEntity(GRD_Dependency entity = null)
        {
            entity = base.ToEntity(entity);
             

            return entity;
        } 
   }
}
