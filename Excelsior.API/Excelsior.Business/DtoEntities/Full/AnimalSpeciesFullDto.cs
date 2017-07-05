using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AnimalSpeciesFullDto : AnimalSpeciesBaseDto
    {
        public AnimalSpeciesFullDto()
            : this(null)
        {

        }
        public AnimalSpeciesFullDto(CFG_AnimalSpecy entity, object sender = null)
            : base(entity, sender)
        {

        }
        public override CFG_AnimalSpecy ToEntity(CFG_AnimalSpecy entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);

            return entity;
        }
    }
} 
