using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(PACS_SeriesComment.PACS_SeriesCommentMetadata))]
    public partial class PACS_SeriesComment
    {
        internal class PACS_SeriesCommentMetadata
        {
            [Required(ErrorMessage = "Comment is required.")]
            public string CommentText
            {
                get;
                set;
            }

            [Required(ErrorMessage = "User is required.")]
            public long? UserID
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Series is required.")]
            public long? SeriesID
            {
                get;
                set;
            }

            [Association("User_Association", "UserID", "UserID")]
            
            public CONTACT_User CONTACTUser
            {
                get;
                set;
            }
        }
    }
}