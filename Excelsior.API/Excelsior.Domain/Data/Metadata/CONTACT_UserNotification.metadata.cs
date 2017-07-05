using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;

namespace Excelsior.Domain
{
    [MetadataTypeAttribute(typeof(CONTACT_UserNotification.CONTACT_UserNotificationMetadata))]
    public partial class CONTACT_UserNotification
    {
        internal sealed class CONTACT_UserNotificationMetadata
        {
            [Association("UserNotification_User_Association", "UserID", "UserID")]
            
            public CONTACT_User CONTACTUser
            {
                get;
                set;
            }

            [Association("UserNotification_Notification_Association", "NotificationID", "NotificationID")]
            
            public CONTACT_Notification CONTACTNotification
            {
                get;
                set;
            }
        }
    }
}