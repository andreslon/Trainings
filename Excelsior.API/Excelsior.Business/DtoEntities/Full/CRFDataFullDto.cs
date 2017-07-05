using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{
    public class CrfDataFullDto : CrfDataBaseDto
    {
        public CrfDataFullDto()
            : this(null)
        {
        }
        public CrfDataFullDto(CRF_Datum entity, object sender = null)
            : base(entity, sender)
        {
            Results = new List<CrfDataResultFullDto>();

            if (entity != null)
            {
                if (!(sender is CrfDataResultBaseDto) && entity.CRF_DataResults.Count > 0)
                {
                    Results = new List<CrfDataResultFullDto>();
                    foreach (var item in entity.CRF_DataResults)
                    {
                        Results.Add(new CrfDataResultFullDto(item, this));
                    }
                }
            }
        }
        public override CRF_Datum ToEntity(CRF_Datum entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            return entity;
        }

        public SeriesFullDto Series { get; set; }
        public List<CrfDataResultFullDto> Results { get; set; }
    }
}