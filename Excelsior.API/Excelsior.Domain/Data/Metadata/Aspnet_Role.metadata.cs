using System.ComponentModel.DataAnnotations;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(Aspnet_Role.Aspnet_RoleMetadata))]
    public partial class Aspnet_Role
    {
        internal sealed class Aspnet_RoleMetadata
        {
            [Required(ErrorMessage = "Name is required.")]
            public string RoleName
            {
                get;
                set;
            }
        }
    }
}