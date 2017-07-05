using System.ComponentModel.DataAnnotations;


namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(QRY_Message.QRY_MessageMetadata))]
    public partial class QRY_Message
    {
        internal sealed class QRY_MessageMetadata
        {
            [Association("Query_Association", "QueryID", "QueryID")]
            
            public QRY_Query QRYQuery
            {
                get;
                set;
            }
        }
    }
}