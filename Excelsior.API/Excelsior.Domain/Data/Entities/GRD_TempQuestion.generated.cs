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
	public partial class GRD_TempQuestion
	{
		private long _GTempQuestionID;
		public virtual long GTempQuestionID
		{
			get
			{
				return this._GTempQuestionID;
			}
			set
			{
				this._GTempQuestionID = value;
			}
		}
		
		private long? _GQuestionID;
		public virtual long? GQuestionID
		{
			get
			{
				return this._GQuestionID;
			}
			set
			{
				this._GQuestionID = value;
			}
		}
		
		private long? _GQuestionGroupID;
		public virtual long? GQuestionGroupID
		{
			get
			{
				return this._GQuestionGroupID;
			}
			set
			{
				this._GQuestionGroupID = value;
			}
		}
		
		private int? _GTempQuestionSeqInGroup;
		public virtual int? GTempQuestionSeqInGroup
		{
			get
			{
				return this._GTempQuestionSeqInGroup;
			}
			set
			{
				this._GTempQuestionSeqInGroup = value;
			}
		}
		
		private GRD_QuestionGroup _GRDQuestionGroup;
		public virtual GRD_QuestionGroup GRDQuestionGroup
		{
			get
			{
				return this._GRDQuestionGroup;
			}
			set
			{
				this._GRDQuestionGroup = value;
			}
		}
		
		private GRD_GradingQuestion _GRDGradingQuestion;
		public virtual GRD_GradingQuestion GRDGradingQuestion
		{
			get
			{
				return this._GRDGradingQuestion;
			}
			set
			{
				this._GRDGradingQuestion = value;
			}
		}
		
	}
}
#pragma warning restore 1591
