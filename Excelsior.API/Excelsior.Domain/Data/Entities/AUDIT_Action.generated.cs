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
	public partial class AUDIT_Action
	{
		private long _AuditActionID;
		public virtual long AuditActionID
		{
			get
			{
				return this._AuditActionID;
			}
			set
			{
				this._AuditActionID = value;
			}
		}
		
		private string _AuditActionDes;
		public virtual string AuditActionDes
		{
			get
			{
				return this._AuditActionDes;
			}
			set
			{
				this._AuditActionDes = value;
			}
		}
		
		private string _AuditActionName;
		public virtual string AuditActionName
		{
			get
			{
				return this._AuditActionName;
			}
			set
			{
				this._AuditActionName = value;
			}
		}
		
		//private IList<AUDIT_Record> _AUDITRecords = new List<AUDIT_Record>();
		//public virtual IList<AUDIT_Record> AUDIT_Records
		//{
		//	get
		//	{
		//		return this._AUDITRecords;
		//	}
		//}
		
	}
}
#pragma warning restore 1591
