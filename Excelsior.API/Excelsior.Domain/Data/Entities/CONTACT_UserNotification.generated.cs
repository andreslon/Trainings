#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Excelsior.Domain;

namespace Excelsior.Domain	
{
	public partial class CONTACT_UserNotification
	{
		private long _UserNotificationID;
		public virtual long UserNotificationID
		{
			get
			{
				return this._UserNotificationID;
			}
			set
			{
				this._UserNotificationID = value;
			}
		}
		
		private long? _UserID;
		public virtual long? UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				this._UserID = value;
			}
		}
		
		private long? _NotificationID;
		public virtual long? NotificationID
		{
			get
			{
				return this._NotificationID;
			}
			set
			{
				this._NotificationID = value;
			}
		}
		
		private CONTACT_User _CONTACTUser;
		public virtual CONTACT_User CONTACTUser
		{
			get
			{
				return this._CONTACTUser;
			}
			set
			{
				this._CONTACTUser = value;
			}
		}
		
		private CONTACT_Notification _CONTACTNotification;
		public virtual CONTACT_Notification CONTACTNotification
		{
			get
			{
				return this._CONTACTNotification;
			}
			set
			{
				this._CONTACTNotification = value;
			}
		}
		
	}
}
#pragma warning restore 1591