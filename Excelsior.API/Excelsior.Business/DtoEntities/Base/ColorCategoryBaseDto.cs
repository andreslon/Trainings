using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class ColorCategoryBaseDto
    {
        public ColorCategoryBaseDto()
            : this(null)
        {
        }
        public ColorCategoryBaseDto(WF_CategoryFlag entity, object sender = null)
        {
            if (entity != null)
            {

                Name = entity.CategoryDes;
                Id = entity.CategoryFlagID;
                
            }
        }
        public virtual WF_CategoryFlag ToEntity(WF_CategoryFlag entity = null)
        {
            if (entity == null)
            {
                entity = new WF_CategoryFlag();
            }

            entity.CategoryDes = Name;
            entity.CategoryFlagID = Id.GetValueOrDefault(); 
            return entity;
        }
        [StringLength(64)]
        [Required]
        public string Name { get; set; }
        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
    }
}
