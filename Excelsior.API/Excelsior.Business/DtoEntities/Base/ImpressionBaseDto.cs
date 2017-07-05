using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class ImpressionBaseDto
    {
        public ImpressionBaseDto()
            : this(null)
        {

        }
        public ImpressionBaseDto(GRD_Impression entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.ImpressionID;
                Description = entity.ImpressionDes;
                Color = entity.ImpressionColor;
            }
        }
        public virtual GRD_Impression ToEntity(GRD_Impression entity = null)
        {
            if (entity == null)
            {
                entity = new GRD_Impression();
            }

            entity.ImpressionID = Id.GetValueOrDefault();
            entity.ImpressionDes = Description;
            entity.ImpressionColor = Color;

            return entity;
        }

        public long? Id { get; set; }
        [StringLength(64)]
        [Required]
        public string Color { get; set; }
        [StringLength(128)]
        public string Description { get; set; }
    }
}