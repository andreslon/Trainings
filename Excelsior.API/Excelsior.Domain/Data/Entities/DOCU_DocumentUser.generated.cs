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
	public partial class DOCU_DocumentUser
	{
		private long _DocumentUserID;
		public virtual long DocumentUserID
		{
			get
			{
				return this._DocumentUserID;
			}
			set
			{
				this._DocumentUserID = value;
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
