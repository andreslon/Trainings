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
	public partial class GRD_QuestionTag
	{
		private long _GQuestionTagID;
		public virtual long GQuestionTagID
		{
			get
			{
				return this._GQuestionTagID;
			}
			set
			{
				this._GQuestionTagID = value;
			}
		}
		
		private string _GQuestionTagString;
		public virtual string GQuestionTagString
		{
			get
			{
				return this._GQuestionTagString;
			}
			set
			{
				this._GQuestionTagString = value;
			}
		}
		
		private IList<GRD_GradingQuestion> _GRDGradingQuestions = new List<GRD_GradingQuestion>();
		public virtual IList<GRD_GradingQuestion> GRD_GradingQuestions
		{
			get
			{
				return this._GRDGradingQuestions;
			}
		}
		
	}
}
#pragma warning restore 1591