using System.ComponentModel.DataAnnotations;

using System;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CONTACT_Equipment.CONTACT_EquipmentMetadata))]
    public partial class CONTACT_Equipment
    {
        internal sealed class CONTACT_EquipmentMetadata
        {
            [Association("CONTACTAffiliation_Association", "AffiliationID", "AffiliationID")]
            public CONTACT_Affiliation CONTACTAffiliation
            {
                get;
                set;
            }

            [Association("CONTACTEquipmentModel_Association", "EquipmentModelID", "EquipmentModelID")]
            public CONTACT_EquipmentModel CONTACTEquipmentModel
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Site is required.")]
            public long? AffiliationID
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Model is required.")]
            public long? EquipmentModelID
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Serial Number is required.")]
            public string MainSerialNum
            {
                get;
                set;
            }
        }
    }
}