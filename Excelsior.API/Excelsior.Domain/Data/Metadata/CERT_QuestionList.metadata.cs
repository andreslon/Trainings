using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CERT_QuestionList.CERT_QuestionListMetadata))]
    public partial class CERT_QuestionList
    {
        internal sealed class CERT_QuestionListMetadata
        {

            [Required(ErrorMessage = "Procedure is required.")]
            public long? ImgProcedureID
            {
                get;
                set;
            }

            [Association("CERT_ImgProcedureList_Association", "ImgProcedureID", "ImgProcedureID")]
                        
            public CERT_ImgProcedureList CERTImgProcedureList
            {
                get;
                set;
            }
        }
    }
}