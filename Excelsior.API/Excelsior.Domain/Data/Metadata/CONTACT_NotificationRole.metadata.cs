using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CONTACT_NotificationRole.CONTACT_NotificationRoleMetadata))]
    public partial class CONTACT_NotificationRole
    {
        internal sealed class CONTACT_NotificationRoleMetadata
        {
            [Association("NotificationRole_Notification_Association", "NotificationID", "NotificationID")]
            
            public CONTACT_Notification CONTACTNotification
            {
                get;
                set;
            }

            [Association("NotificationRole_Role_Association", "RoleId", "RoleId")]
            
            public Aspnet_Role AspnetRole
            {
                get;
                set;
            }
        }
    }
}