using System.ComponentModel.DataAnnotations;

using System;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CONTACT_EquipmentModel.CONTACT_EquipmentModelMetadata))]
    public partial class CONTACT_EquipmentModel
    {
        internal sealed class CONTACT_EquipmentModelMetadata
        {
            [Required(ErrorMessage = "Manufacturer is required.")]
            public string ManufacturerName
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Model is required.")]
            public string ManufacturerModel
            {
                get;
                set;
            }
        }
    }
}