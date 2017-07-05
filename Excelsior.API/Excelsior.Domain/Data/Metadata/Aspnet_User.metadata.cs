using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Excelsior.Domain
{
    [XmlInclude(typeof(Aspnet_Membership))]
    [KnownType(typeof(Aspnet_Membership))]
    [MetadataTypeAttribute(typeof(Aspnet_User.Aspnet_UserMetadata))]
    public partial class Aspnet_User
    {
        internal sealed class Aspnet_UserMetadata
        {
           [Required(ErrorMessage = "Name is required.")]
            public string UserName
            {
                get;
                set;
            }

            //[Association("AspUser_AspRole_Association", "UserId", "RoleId")]
            //public IList<Aspnet_Role> Aspnet_Roles
            //{
            //    get;
            //    set;
            //}
        }
    }
}