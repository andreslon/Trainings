using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Base
{
    public class MeaFreehandBaseDto
    {
        public MeaFreehandBaseDto()
            : this(null)
        {

        }
        public MeaFreehandBaseDto(MEA_Freehand entity, object sender = null)
        {            
            if (entity != null)
            {
                Id = entity.MeasurementID;
                Color = entity.Color;
                Tag = entity.Tag;
                Xml = entity.MeasurementXML;
            }
        }
        public virtual MEA_Freehand ToEntity(MEA_Freehand entity = null, string fields = null)
        {
            if (entity != null)
            {
                using (var fieldvalidation = new FieldValidation(fields))
                {
                    if (fieldvalidation["color"])
                        entity.Color = Color;
                    if (fieldvalidation["tag"])
                        entity.Tag = Tag;
                    if (fieldvalidation["xml"])
                        entity.MeasurementXML = Xml;
                }
            }
            return entity;
        }

        [Range(0, long.MaxValue)]
        public long Id { get; set; }
        [StringLength(10)]
        public string Color { get; set; }
        public string Tag { get; set; }
        public string Xml { get; set; }
    }
}
