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
	public partial class PACS_SeriesGroup
	{
		private long _SeriesGroupID;
		public virtual long SeriesGroupID
		{
			get
			{
				return this._SeriesGroupID;
			}
			set
			{
				this._SeriesGroupID = value;
			}
		}
		
		private long? _TimePointsID;
		public virtual long? TimePointsID
		{
			get
			{
				return this._TimePointsID;
			}
			set
			{
				this._TimePointsID = value;
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

        private PACS_TimePoint _PACSTimePoint;
		public virtual PACS_TimePoint PACSTimePoint
		{
			get
			{
				return this._PACSTimePoint;
			}
			set
			{
				this._PACSTimePoint = value;
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
