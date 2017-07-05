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
	public partial class Aspnet_Role
	{
		private string _RoleName;
		public virtual string RoleName
		{
			get
			{
				return this._RoleName;
			}
			set
			{
				this._RoleName = value;
			}
		}
		
		private Guid _RoleId;
		public virtual Guid RoleId
		{
			get
			{
				return this._RoleId;
			}
			set
			{
				this._RoleId = value;
			}
		}
		
		private string _LoweredRoleName;
		public virtual string LoweredRoleName
		{
			get
			{
				return this._LoweredRoleName;
			}
			set
			{
				this._LoweredRoleName = value;
			}
		}
		
		private string _Description;
		public virtual string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				this._Description = value;
			}
		}
		
		private Guid _ApplicationId;
		public virtual Guid ApplicationId
		{
			get
			{
				return this._ApplicationId;
			}
			set
			{
				this._ApplicationId = value;
			}
		}
		
		private Aspnet_Application _AspnetApplication;
		public virtual Aspnet_Application AspnetApplication
		{
			get
			{
				return this._AspnetApplication;
			}
			set
			{
				this._AspnetApplication = value;
			}
		}
		
		private IList<CONTACT_User> _CONTACTUsers = new List<CONTACT_User>();
		public virtual IList<CONTACT_User> CONTACT_Users
		{
			get
			{
				return this._CONTACTUsers;
			}
		}
		
		private IList<CONTACT_NotificationRole> _CONTACTNotificationRoles = new List<CONTACT_NotificationRole>();
		public virtual IList<CONTACT_NotificationRole> CONTACT_NotificationRoles
		{
			get
			{
				return this._CONTACTNotificationRoles;
			}
		}
		
		private IList<RPT_TrialReportRole> _RPTTrialReportRoles = new List<RPT_TrialReportRole>();
		public virtual IList<RPT_TrialReportRole> RPT_TrialReportRoles
		{
			get
			{
				return this._RPTTrialReportRoles;
			}
		}

        private IList<DOCU_DocumentRole> _DOCUDocumentRoles = new List<DOCU_DocumentRole>();
        public virtual IList<DOCU_DocumentRole> DOCU_DocumentRoles
        {
            get
            {
                return this._DOCUDocumentRoles;
            }
        }
    }
}
#pragma warning restore 1591
