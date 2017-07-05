using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Base
{
    public class StencilsBaseDto
    {
        public StencilsBaseDto()
            : this(null)
        {

        }
        public StencilsBaseDto(MEA_Stencil entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.StencilID;
                StudyId = entity.TrialID;
                Tag = entity.Tag;
                Color = entity.Color;
            }
        }
        public virtual MEA_Stencil ToEntity(MEA_Stencil entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new MEA_Stencil();
            }

            entity.StencilID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["trialid"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["tag"])
                    entity.Tag = Tag;
                if (fieldvalidation["color"])
                    entity.Color = Color;
            }   
            return entity;
        }
        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Required]
        [StringLength(512)]
        public string Tag { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        [StringLength(512)]
        public string Color { get; set; }
 
    }
}
