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
	public partial class CRF_Template
	{
		private long _CRFTemplateID;
		public virtual long CRFTemplateID
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

        private string _TemplateName;
        public virtual string TemplateName
        {
            get
            {
                return this._TemplateName;
            }
            set
            {
                this._TemplateName = value;
            }
        }

        private string _TemplateDes;
        public virtual string TemplateDes
        {
            get
            {
                return this._TemplateDes;
            }
            set
            {
                this._TemplateDes = value;
            }
        }

        private string _TemplateAbbrev;
        public virtual string TemplateAbbrev
        {
            get
            {
                return this._TemplateAbbrev;
            }
            set
            {
                this._TemplateAbbrev = value;
            }
        }

        private string _TemplateVersion;
        public virtual string TemplateVersion
        {
            get
            {
                return this._TemplateVersion;
            }
            set
            {
                this._TemplateVersion = value;
            }
        }

        private string _AssocProtocol;
        public virtual string AssocProtocol
        {
            get
            {
                return this._AssocProtocol;
            }
            set
            {
                this._AssocProtocol = value;
            }
        }

        private bool _IsLocked;
		public virtual bool IsLocked
        {
			get
			{
				return this._IsLocked;
			}
			set
			{
				this._IsLocked = value;
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

		private IList<CRF_Datum> _CRFData = new List<CRF_Datum>();
		public virtual IList<CRF_Datum> CRF_Data
        {
			get
			{
				return this._CRFData;
			}
		}

        private IList<CRF_TemplateGroup> _CRFTemplateGroups = new List<CRF_TemplateGroup>();
        public virtual IList<CRF_TemplateGroup> CRF_TemplateGroups
        {
            get
            {
                return this._CRFTemplateGroups;
            }
        }

        private IList<PACS_TPProcList> _PACSTPProcLists = new List<PACS_TPProcList>();
        public virtual IList<PACS_TPProcList> PACS_TPProcLists
        {
            get
            {
                return this._PACSTPProcLists;
            }
        }

        private IList<PACS_SeriesGroup> _PACSSeriesGroups = new List<PACS_SeriesGroup>();
        public virtual IList<PACS_SeriesGroup> PACS_SeriesGroups
        {
            get
            {
                return this._PACSSeriesGroups;
            }
        }
    }
}
#pragma warning restore 1591
