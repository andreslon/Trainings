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
	public partial class Aspnet_Application
	{
		private string _LoweredApplicationName;
		public virtual string LoweredApplicationName
		{
			get
			{
				return this._LoweredApplicationName;
			}
			set
			{
				this._LoweredApplicationName = value;
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
		
		private string _ApplicationName;
		public virtual string ApplicationName
		{
			get
			{
				return this._ApplicationName;
			}
			set
			{
				this._ApplicationName = value;
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
		
		private IList<Aspnet_Role> _AspnetRoles = new List<Aspnet_Role>();
		public virtual IList<Aspnet_Role> Aspnet_Roles
		{
			get
			{
				return this._AspnetRoles;
			}
		}
		
		private IList<Aspnet_Membership> _AspnetMemberships = new List<Aspnet_Membership>();
		public virtual IList<Aspnet_Membership> Aspnet_Memberships
		{
			get
			{
				return this._AspnetMemberships;
			}
		}

        private IList<Aspnet_User> _AspnetUsers = new List<Aspnet_User>();
        public virtual IList<Aspnet_User> Aspnet_Users
        {
            get
            {
                return this._AspnetUsers;
            }
        }
    }
}
#pragma warning restore 1591