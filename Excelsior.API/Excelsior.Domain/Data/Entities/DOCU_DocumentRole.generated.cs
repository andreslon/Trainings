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
	public partial class DOCU_DocumentRole
	{
		private long _DocumentRoleID;
		public virtual long DocumentRoleID
		{
			get
			{
				return this._DocumentRoleID;
			}
			set
			{
				this._DocumentRoleID = value;
			}
		}
		
		private Guid? _RoleId;
		public virtual Guid? RoleId
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
		
		private long? _DocumentID;
		public virtual long? DocumentID
		{
			get
			{
				return this._DocumentID;
			}
			set
			{
				this._DocumentID = value;
			}
		}
		
		private Aspnet_Role _AspnetRole;
		public virtual Aspnet_Role AspnetRole
        {
			get
			{
				return this._AspnetRole;
			}
			set
			{
				this._AspnetRole = value;
			}
		}
		
		private DOCU_Document _DOCUDocument;
		public virtual DOCU_Document DOCUDocument
		{
			get
			{
				return this._DOCUDocument;
			}
			set
			{
				this._DOCUDocument = value;
			}
		}
		
	}
}
#pragma warning restore 1591