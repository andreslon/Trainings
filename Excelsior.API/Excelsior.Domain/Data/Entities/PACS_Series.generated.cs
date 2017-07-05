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
	public partial class PACS_Series
	{
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
		
		private long _SeriesID;
		public virtual long SeriesID
		{
			get
			{
				return this._SeriesID;
			}
			set
			{
				this._SeriesID = value;
			}
		}
		
		private bool _IsValidated;
		public virtual bool IsValidated
		{
			get
			{
				return this._IsValidated;
			}
			set
			{
				this._IsValidated = value;
			}
		}
		
		private bool _IsQCSeries;
		public virtual bool IsQCSeries
		{
			get
			{
				return this._IsQCSeries;
			}
			set
			{
				this._IsQCSeries = value;
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
		
		private bool? _DirectorReviewComplete;
		public virtual bool? DirectorReviewComplete
		{
			get
			{
				return this._DirectorReviewComplete;
			}
			set
			{
				this._DirectorReviewComplete = value;
			}
		}
		
		private long? _TPProcListID;
		public virtual long? TPProcListID
		{
			get
			{
				return this._TPProcListID;
			}
			set
			{
				this._TPProcListID = value;
			}
		}
		
		private long? _PhotographerID;
		public virtual long? PhotographerID
		{
			get
			{
				return this._PhotographerID;
			}
			set
			{
				this._PhotographerID = value;
			}
		}
		
		private long? _EquipmentID;
		public virtual long? EquipmentID
		{
			get
			{
				return this._EquipmentID;
			}
			set
			{
				this._EquipmentID = value;
			}
		}
		
		private DateTime? _LastExportDateTime;
		public virtual DateTime? LastExportDateTime
		{
			get
			{
				return this._LastExportDateTime;
			}
			set
			{
				this._LastExportDateTime = value;
			}
		}
		
		private string _SeriesDCMInstanceUID;
		public virtual string SeriesDCMInstanceUID
		{
			get
			{
				return this._SeriesDCMInstanceUID;
			}
			set
			{
				this._SeriesDCMInstanceUID = value;
			}
		}
		
		private bool _IsDataQualityAdequate;
		public virtual bool IsDataQualityAdequate
		{
			get
			{
				return this._IsDataQualityAdequate;
			}
			set
			{
				this._IsDataQualityAdequate = value;
			}
		}
		
		private DateTime? _StudyDate;
		public virtual DateTime? StudyDate
		{
			get
			{
				return this._StudyDate;
			}
			set
			{
				this._StudyDate = value;
			}
		}
		
		private long? _SeriesGroupID;
		public virtual long? SeriesGroupID
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
        
        private string _LateralityReceived;
        public virtual string LateralityReceived
        {
            get
            {
                return this._LateralityReceived;
            }
            set
            {
                this._LateralityReceived = value;
            }
        }

        private PACS_TPProcList _PACSTPProcList;
		public virtual PACS_TPProcList PACSTPProcList
		{
			get
			{
				return this._PACSTPProcList;
			}
			set
			{
				this._PACSTPProcList = value;
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
		
		private CONTACT_Equipment _CONTACTEquipment;
		public virtual CONTACT_Equipment CONTACTEquipment
		{
			get
			{
				return this._CONTACTEquipment;
			}
			set
			{
				this._CONTACTEquipment = value;
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
		
		private PACS_SeriesGroup _PACSSeriesGroup;
		public virtual PACS_SeriesGroup PACSSeriesGroup
		{
			get
			{
				return this._PACSSeriesGroup;
			}
			set
			{
				this._PACSSeriesGroup = value;
			}
		}
		
		private IList<PACS_RawDatum> _PACSRawData = new List<PACS_RawDatum>();
		public virtual IList<PACS_RawDatum> PACS_RawData
		{
			get
			{
				return this._PACSRawData;
			}
		}
		
		private IList<UPLD_UploadInfo> _UPLDUploadInfos = new List<UPLD_UploadInfo>();
		public virtual IList<UPLD_UploadInfo> UPLD_UploadInfos
		{
			get
			{
				return this._UPLDUploadInfos;
			}
		}
		
		private IList<QRY_Query> _QRYQueries = new List<QRY_Query>();
		public virtual IList<QRY_Query> QRY_Queries
		{
			get
			{
				return this._QRYQueries;
			}
		}
		
		private IList<GRD_Report> _GRDReports = new List<GRD_Report>();
		public virtual IList<GRD_Report> GRD_Reports
		{
			get
			{
				return this._GRDReports;
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
		
		private IList<PACS_SeriesComment> _PACSSeriesComments = new List<PACS_SeriesComment>();
		public virtual IList<PACS_SeriesComment> PACS_SeriesComments
		{
			get
			{
				return this._PACSSeriesComments;
			}
		}

        private IList<PACS_SeriesAttachment> _PACSSeriesAttachments = new List<PACS_SeriesAttachment>();
        public virtual IList<PACS_SeriesAttachment> PACS_SeriesAttachments
        {
            get
            {
                return this._PACSSeriesAttachments;
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

        private IList<CONTACT_User> _PACSSeriesAssignees = new List<CONTACT_User>();
        public virtual IList<CONTACT_User> PACS_SeriesAssignees
        {
            get
            {
                return this._PACSSeriesAssignees;
            }
        }
    }
}
#pragma warning restore 1591