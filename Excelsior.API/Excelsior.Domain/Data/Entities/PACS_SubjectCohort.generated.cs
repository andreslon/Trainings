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
	public partial class PACS_SubjectCohort
	{
		private long _SubjectCohortID;
		public virtual long SubjectCohortID
		{
			get
			{
				return this._SubjectCohortID;
			}
			set
			{
				this._SubjectCohortID = value;
			}
		}
		
		private long? _TrialID;
		public virtual long? TrialID
		{
			get
			{
				return this._TrialID;
			}
			set
			{
				this._TrialID = value;
			}
		}
		
		private string _CohortName;
		public virtual string CohortName
		{
			get
			{
				return this._CohortName;
			}
			set
			{
				this._CohortName = value;
			}
		}
		
		private bool _IsActive;
		public virtual bool IsActive
		{
			get
			{
				return this._IsActive;
			}
			set
			{
				this._IsActive = value;
			}
		}
		
		private PACS_Trial _PACSTrial;
		public virtual PACS_Trial PACSTrial
		{
			get
			{
				return this._PACSTrial;
			}
			set
			{
				this._PACSTrial = value;
			}
		}
		
		private IList<PACS_Subject> _PACSSubjects = new List<PACS_Subject>();
		public virtual IList<PACS_Subject> PACS_Subjects
		{
			get
			{
				return this._PACSSubjects;
			}
		}
		
	}
}
#pragma warning restore 1591