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
	public partial class CRF_TemplateQuestion
    {
		private long _CRFTemplateQuestionID;
		public virtual long CRFTemplateQuestionID
        {
			get
			{
				return this._CRFTemplateQuestionID;
			}
			set
			{
				this._CRFTemplateQuestionID = value;
			}
		}

        private long? _CRFTemplateGroupID;
        public virtual long? CRFTemplateGroupID
        {
            get
            {
                return this._CRFTemplateGroupID;
            }
            set
            {
                this._CRFTemplateGroupID = value;
            }
        }

        private long? _CRFAnswerTypeID;
        public virtual long? CRFAnswerTypeID
        {
            get
            {
                return this._CRFAnswerTypeID;
            }
            set
            {
                this._CRFAnswerTypeID = value;
            }
        }

        private long? _CRFAnswerValidationID;
        public virtual long? CRFAnswerValidationID
        {
            get
            {
                return this._CRFAnswerValidationID;
            }
            set
            {
                this._CRFAnswerValidationID = value;
            }
        }

        private string _QuestionText;
        public virtual string QuestionText
        {
            get
            {
                return this._QuestionText;
            }
            set
            {
                this._QuestionText = value;
            }
        }

        private string _CDashVariable;
        public virtual string CDashVariable
        {
            get
            {
                return this._CDashVariable;
            }
            set
            {
                this._CDashVariable = value;
            }
        }

        private string _SDTMVariable;
        public virtual string SDTMVariable
        {
            get
            {
                return this._SDTMVariable;
            }
            set
            {
                this._SDTMVariable = value;
            }
        }

        private string _QuestionDes;
        public virtual string QuestionDes
        {
            get
            {
                return this._QuestionDes;
            }
            set
            {
                this._QuestionDes = value;
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

        private int _QuestionSeq;
        public virtual int QuestionSeq
        {
            get
            {
                return this._QuestionSeq;
            }
            set
            {
                this._QuestionSeq = value;
            }
        }

        private bool _IsLaterality;
        public virtual bool IsLaterality
        {
            get
            {
                return this._IsLaterality;
            }
            set
            {
                this._IsLaterality = value;
            }
        }

        private CRF_TemplateGroup _CRFTemplateGroup;
		public virtual CRF_TemplateGroup CRFTemplateGroup
        {
			get
			{
				return this._CRFTemplateGroup;
			}
			set
			{
				this._CRFTemplateGroup = value;
			}
		}

        private CRF_AnswerType _CRFAnswerType;
        public virtual CRF_AnswerType CRFAnswerType
        {
            get
            {
                return this._CRFAnswerType;
            }
            set
            {
                this._CRFAnswerType = value;
            }
        }

        private CRF_AnswerValidation _CRFAnswerValidation;
        public virtual CRF_AnswerValidation CRFAnswerValidation
        {
            get
            {
                return this._CRFAnswerValidation;
            }
            set
            {
                this._CRFAnswerValidation = value;
            }
        }

        private IList<CRF_TemplateAnswer> _CRFTemplateAnswers = new List<CRF_TemplateAnswer>();
		public virtual IList<CRF_TemplateAnswer> CRF_TemplateAnswers
        {
			get
			{
				return this._CRFTemplateAnswers;
			}
		}

        private IList<CRF_TemplateDependency> _CRFTemplateDependencies = new List<CRF_TemplateDependency>();
        public virtual IList<CRF_TemplateDependency> CRF_TemplateDependencies
        {
            get
            {
                return this._CRFTemplateDependencies;
            }
        }

        private IList<CRF_TemplateDependencySource> _CRFTemplateDependencySources = new List<CRF_TemplateDependencySource>();
        public virtual IList<CRF_TemplateDependencySource> CRF_TemplateDependencySources
        {
            get
            {
                return this._CRFTemplateDependencySources;
            }
        }

        private IList<CRF_DataResult> _CRFDataResults = new List<CRF_DataResult>();
        public virtual IList<CRF_DataResult> CRF_DataResults
        {
            get
            {
                return this._CRFDataResults;
            }
        }

        private IList<CRF_TemplateQuestionTag> _CRFTemplateQuestionTags = new List<CRF_TemplateQuestionTag>();
        public virtual IList<CRF_TemplateQuestionTag> CRF_TemplateQuestionTags
        {
            get
            {
                return this._CRFTemplateQuestionTags;
            }
        }
    }
}
#pragma warning restore 1591