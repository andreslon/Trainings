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
	public partial class CONTACT_UserAffiliation
	{
		private long _UserAffiliationID;
		public virtual long UserAffiliationID
		{
			get
			{
				return this._UserAffiliationID;
			}
			set
			{
				this._UserAffiliationID = value;
			}
		}
		
		private long? _AffiliationID;
		public virtual long? AffiliationID
		{
			get
			{
				return this._AffiliationID;
			}
			set
			{
				this._AffiliationID = value;
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
		
		private CONTACT_Affiliation _CONTACTAffiliation;
		public virtual CONTACT_Affiliation CONTACTAffiliation
		{
			get
			{
				return this._CONTACTAffiliation;
			}
			set
			{
				this._CONTACTAffiliation = value;
			}
		}
		
	}
}
#pragma warning restore 1591
