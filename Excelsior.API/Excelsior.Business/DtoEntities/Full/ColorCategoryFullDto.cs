using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class ColorCategoryFullDto : ColorCategoryBaseDto
    {
        public ColorCategoryFullDto()
            : this(null)
        {
       }
        public ColorCategoryFullDto(WF_CategoryFlag entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                
            }
        }
        public override WF_CategoryFlag ToEntity(WF_CategoryFlag entity = null)
        {
            entity = base.ToEntity(entity);
             

            return entity;
        }

      }
}
