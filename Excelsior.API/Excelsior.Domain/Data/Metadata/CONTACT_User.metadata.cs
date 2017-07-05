using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CONTACT_User.CONTACT_UserMetadata))]
    public partial class CONTACT_User
    {
        internal sealed class CONTACT_UserMetadata
        {
            [Required(ErrorMessage = "Affiliation is required.")]
            public long? AffiliationID
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Role is required.")]
            public Guid? RoleId
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Last Name is required.")]
            public string LastName
            {
                get;
                set;
            }

            [Required(ErrorMessage = "Email is required.")]
            public string Email
            {
                get;
                set;
            }

            [Association("User_UserTrial_Association", "UserID", "UserID")]
            public IList<CONTACT_UserTrial> CONTACT_UserTrials
            {
                get;
                set;
            }

            [Association("User_AspUser_Association", "AspUserID", "UserId")]
            public Aspnet_User AspnetUser
            {
                get;
                set;
            }

            [Association("User_AspRole_Association", "RoleId", "RoleId")]
            public Aspnet_Role AspnetRole
            {
                get;
                set;
            }

            [Association("CONTACTAffiliation_Association", "AffiliationID", "AffiliationID")]
            public CONTACT_Affiliation CONTACTAffiliation
            {
                get;
                set;
            }

            [Association("CONTACTCountries_Association", "CountryID", "CountryID")]
            public CONTACT_Country CONTACTCountry
            {
                get;
                set;
            }
        }
    }
}