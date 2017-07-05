using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class EquipmentModelBaseDto
    {
        public EquipmentModelBaseDto()
            : this(null)
        {

        }
        public EquipmentModelBaseDto(CONTACT_EquipmentModel entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.EquipmentModelID;
                ManufacturerName = entity.ManufacturerName;
                ManufacturerModel = entity.ManufacturerModel;
                EquipmentType = entity.EquipmentType;
            }
        }
        public virtual CONTACT_EquipmentModel ToEntity(CONTACT_EquipmentModel entity = null, string fields=null)
        {
            if (entity == null)
            {
                entity = new CONTACT_EquipmentModel();
            }

            entity.EquipmentModelID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["manufacturername"])
                    entity.ManufacturerName = ManufacturerName;
                if (fieldvalidation["manufacturermodel"])
                    entity.ManufacturerModel = ManufacturerModel;
                if (fieldvalidation["equipmenttype"])
                    entity.EquipmentType = EquipmentType;
            }
          
            return entity;
        }
        public long? Id { get; set; }
        [StringLength(256)]
        public string ManufacturerName { get; set; }
        [StringLength(256)]
        public string ManufacturerModel { get; set; }
        [StringLength(256)]
        public string EquipmentType { get; set; }

    }
}
