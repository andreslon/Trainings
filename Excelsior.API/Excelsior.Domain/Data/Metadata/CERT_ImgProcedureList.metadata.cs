using System.ComponentModel.DataAnnotations;

using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CERT_ImgProcedureList.CERT_ImgProcedureListMetadata))]
    public partial class CERT_ImgProcedureList
    {
        internal sealed class CERT_ImgProcedureListMetadata
        {
            [Required(ErrorMessage = "Name is required.")]
            public string ImgProcedureName { get; set; }

            [Association("ImgProcedureList_DataType_Association", "DataTypeID", "DataTypeID")]
            
            public PACS_DataType PACSDataType
            {
                get;
                set;
            }

            //[Association("PACS_TPProcLists_Association", "ImgProcedureID", "ImgProcedureID")]
            //
            //public IList<PACS_TPProcList> PACS_TPProcLists
            //{
            //    get;
            //    set;
            //}
        }
    }
}