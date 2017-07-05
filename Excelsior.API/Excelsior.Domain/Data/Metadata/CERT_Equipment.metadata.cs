using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CERT_Equipment.CERT_EquipmentMetadata))]
    public partial class CERT_Equipment
    {
        internal sealed class CERT_EquipmentMetadata
        {
            [Association("CERT_UploadInfo_Association", "CertEquipmentID", "CertEquipmentID")]
            
            public CERT_UploadInfo CERT_UploadInfos
            {
                get;
                set;
            }

            [Association("CERTImgProcedureList_Association", "ImgProcedureID", "ImgProcedureID")]
            
            public CERT_ImgProcedureList CERTImgProcedureList
            {
                get;
                set;
            }

            [Association("CONTACTEquipment_Association", "EquipmentID", "EquipmentID")]
            
            public CONTACT_Equipment CONTACTEquipment
            {
                get;
                set;
            }

            [Association("PACSTrial_Association", "TrialID", "TrialID")]
            
            public PACS_Trial PACSTrial
            {
                get;
                set;
            }

            [Association("CertifiedBy_Association", "CertifiedByID", "UserID")]
            
            public CONTACT_User CertifiedBy
            {
                get;
                set;
            }

            [Association("Queries_Association", "CertEquipmentID", "CertEquipmentID")]
            
            public IList<QRY_Query> QRY_Queries
            {
                get;
                set;
            }
        }
    }
}