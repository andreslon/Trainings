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
	public partial class PACS_TPProcList
	{
		private long _TPProcID;
		public virtual long TPProcID
		{
			get
			{
				return this._TPProcID;
			}
			set
			{
				this._TPProcID = value;
			}
		}
		
		private long? _TimePointsListID;
		public virtual long? TimePointsListID
		{
			get
			{
				return this._TimePointsListID;
			}
			set
			{
				this._TimePointsListID = value;
			}
		}
		
		private long? _ImgProcedureID;
		public virtual long? ImgProcedureID
		{
			get
			{
				return this._ImgProcedureID;
			}
			set
			{
				this._ImgProcedureID = value;
			}
		}
		
		private long? _WFTemplateID;
		public virtual long? WFTemplateID
		{
			get
			{
				return this._WFTemplateID;
			}
			set
			{
				this._WFTemplateID = value;
			}
		}
		
		private long? _GTemplateID;
		public virtual long? GTemplateID
		{
			get
			{
				return this._GTemplateID;
			}
			set
			{
				this._GTemplateID = value;
			}
		}

        private long? _CRFTemplateID;
        public virtual long? CRFTemplateID
        {
            get
            {
                return this._CRFTemplateID;
            }
            set
            {
                this._CRFTemplateID = value;
            }
        }

        private bool _IsGradeBothLaterality;
		public virtual bool IsGradeBothLaterality
		{
			get
			{
				return this._IsGradeBothLaterality;
			}
			set
			{
				this._IsGradeBothLaterality = value;
			}
		}
		
		private short? _PercentSeriesForReview;
		public virtual short? PercentSeriesForReview
		{
			get
			{
				return this._PercentSeriesForReview;
			}
			set
			{
				this._PercentSeriesForReview = value;
			}
		}
		
		private int? _CounterSeriesSigned;
		public virtual int? CounterSeriesSigned
		{
			get
			{
				return this._CounterSeriesSigned;
			}
			set
			{
				this._CounterSeriesSigned = value;
			}
		}
		
		private int? _CounterSeriesForReview;
		public virtual int? CounterSeriesForReview
		{
			get
			{
				return this._CounterSeriesForReview;
			}
			set
			{
				this._CounterSeriesForReview = value;
			}
		}

        private bool _IsAttachmentsEnabled;
        public virtual bool IsAttachmentsEnabled
        {
            get
            {
                return this._IsAttachmentsEnabled;
            }
            set
            {
                this._IsAttachmentsEnabled = value;
            }
        }

        private PACS_TimePointsList _PACSTimePointsList;
		public virtual PACS_TimePointsList PACSTimePointsList
		{
			get
			{
				return this._PACSTimePointsList;
			}
			set
			{
				this._PACSTimePointsList = value;
			}
		}
		
		private CERT_ImgProcedureList _CERTImgProcedureList;
		public virtual CERT_ImgProcedureList CERTImgProcedureList
		{
			get
			{
				return this._CERTImgProcedureList;
			}
			set
			{
				this._CERTImgProcedureList = value;
			}
		}
		
		private WF_Template _WFTemplate;
		public virtual WF_Template WFTemplate
		{
			get
			{
				return this._WFTemplate;
			}
			set
			{
				this._WFTemplate = value;
			}
		}
		
		private GRD_GradingTemplate _GRDGradingTemplate;
		public virtual GRD_GradingTemplate GRDGradingTemplate
		{
			get
			{
				return this._GRDGradingTemplate;
			}
			set
			{
				this._GRDGradingTemplate = value;
			}
		}

        private CRF_Template _CRFTemplate;
        public virtual CRF_Template CRFTemplate
        {
            get
            {
                return this._CRFTemplate;
            }
            set
            {
                this._CRFTemplate = value;
            }
        }

        private IList<PACS_Series> _PACSSeries = new List<PACS_Series>();
		public virtual IList<PACS_Series> PACS_Series
		{
			get
			{
				return this._PACSSeries;
			}
		}
		
	}
}
#pragma warning restore 1591