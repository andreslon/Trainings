using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class AnimalSpeciesBaseDto
    {
        public AnimalSpeciesBaseDto()
            : this(null)
        {

        }
        public AnimalSpeciesBaseDto(CFG_AnimalSpecy entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.AnimalSpeciesID;
                Name = entity.AnimalSpeciesName;
                DisplayName = entity.AnimalSpeciesDisplayName;
            }
        }
        public virtual CFG_AnimalSpecy ToEntity(CFG_AnimalSpecy entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new CFG_AnimalSpecy();
            }

            entity.AnimalSpeciesID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["name"])
                    entity.AnimalSpeciesName = Name;
                if (fieldvalidation["displayname"])
                    entity.AnimalSpeciesDisplayName = DisplayName;
            }

            return entity;
        }

        public long? Id { get; set; }
        [StringLength(256)]
        [Required]
        public string Name { get; set; }
        [StringLength(512)]
        public string DisplayName { get; set; }
    }
}
