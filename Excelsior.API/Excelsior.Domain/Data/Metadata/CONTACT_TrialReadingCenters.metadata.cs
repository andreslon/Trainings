using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CONTACT_TrialReadingCenter.CONTACT_TrialReadingCentersMetadata))]
    public partial class CONTACT_TrialReadingCenter
    {
        internal sealed class CONTACT_TrialReadingCentersMetadata
        {
            [Association("CONTACTAffiliation_Association", "AffiliationID", "AffiliationID")]
            
            public CONTACT_Affiliation CONTACTAffiliation
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

            [Association("CERTImgProcedureList_Association", "ImgProcedureID", "ImgProcedureID")]
            
            public CERT_ImgProcedureList CERTImgProcedureList
            {
                get;
                set;
            }
        }
    }
}