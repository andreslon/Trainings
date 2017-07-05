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
	public partial class PACS_RawDatum
	{
        private long _RawDataID;
        public virtual long RawDataID
        {
            get
            {
                return this._RawDataID;
            }
            set
            {
                this._RawDataID = value;
            }
        }

        private long _RawDataIndex;
        public virtual long RawDataIndex
        {
            get
            {
                return this._RawDataIndex;
            }
            set
            {
                this._RawDataIndex = value;
            }
        }

        private string _ThumbImageLocation;
		public virtual string ThumbImageLocation
		{
			get
			{
				return this._ThumbImageLocation;
			}
			set
			{
				this._ThumbImageLocation = value;
			}
		}
		
		private long? _SeriesID;
		public virtual long? SeriesID
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

        private long? _CertUserID;
        public virtual long? CertUserID
        {
            get
            {
                return this._CertUserID;
            }
            set
            {
                this._CertUserID = value;
            }
        }

        private long? _CertEquipmentID;
        public virtual long? CertEquipmentID
        {
            get
            {
                return this._CertEquipmentID;
            }
            set
            {
                this._CertEquipmentID = value;
            }
        }

        private long? _DataTypeID;
		public virtual long? DataTypeID
		{
			get
			{
				return this._DataTypeID;
			}
			set
			{
				this._DataTypeID = value;
			}
		}
		
		private string _DCMInstanceUID;
		public virtual string DCMInstanceUID
		{
			get
			{
				return this._DCMInstanceUID;
			}
			set
			{
				this._DCMInstanceUID = value;
			}
		}
		
		private string _DCMFileLocation;
		public virtual string DCMFileLocation
		{
			get
			{
				return this._DCMFileLocation;
			}
			set
			{
				this._DCMFileLocation = value;
			}
		}
		
		private string _LastError;
		public virtual string LastError
		{
			get
			{
				return this._LastError;
			}
			set
			{
				this._LastError = value;
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
		
		private long? _StatusID;
		public virtual long? StatusID
		{
			get
			{
				return this._StatusID;
			}
			set
			{
				this._StatusID = value;
			}
		}
		
		private bool _HasError;
		public virtual bool HasError
		{
			get
			{
				return this._HasError;
			}
			set
			{
				this._HasError = value;
			}
		}
		
		private string _Laterality;
		public virtual string Laterality
		{
			get
			{
				return this._Laterality;
			}
			set
			{
				this._Laterality = value;
			}
		}

        private string _OriginalFileName;
        public virtual string OriginalFileName
        {
            get
            {
                return this._OriginalFileName;
            }
            set
            {
                this._OriginalFileName = value;
            }
        }

        private PACS_Series _PACSSeries;
		public virtual PACS_Series PACSSeries
		{
			get
			{
				return this._PACSSeries;
			}
			set
			{
				this._PACSSeries = value;
			}
		}

        private CERT_User _CERTUser;
        public virtual CERT_User CERTUser
        {
            get
            {
                return this._CERTUser;
            }
            set
            {
                this._CERTUser = value;
            }
        }

        private CERT_Equipment _CERTEquipment;
        public virtual CERT_Equipment CERTEquipment
        {
            get
            {
                return this._CERTEquipment;
            }
            set
            {
                this._CERTEquipment = value;
            }
        }

        private PACS_DataType _PACSDataType;
		public virtual PACS_DataType PACSDataType
		{
			get
			{
				return this._PACSDataType;
			}
			set
			{
				this._PACSDataType = value;
			}
		}
		
		private PACS_RawDataStatus _PACSRawDataStatus;
		public virtual PACS_RawDataStatus PACSRawDataStatus
		{
			get
			{
				return this._PACSRawDataStatus;
			}
			set
			{
				this._PACSRawDataStatus = value;
			}
		}
		
		private IList<PACS_DicomFrame> _PACSDicomFrames = new List<PACS_DicomFrame>();
		public virtual IList<PACS_DicomFrame> PACS_DicomFrames
		{
			get
			{
				return this._PACSDicomFrames;
			}
		}
		
		private IList<MEA_Measurement> _MEAMeasurements = new List<MEA_Measurement>();
		public virtual IList<MEA_Measurement> MEA_Measurements
		{
			get
			{
				return this._MEAMeasurements;
			}
		}
		
		private IList<PACS_ProcessedDatum> _PACSProcessedData = new List<PACS_ProcessedDatum>();
		public virtual IList<PACS_ProcessedDatum> PACS_ProcessedData
		{
			get
			{
				return this._PACSProcessedData;
			}
		}
		
		private IList<PACS_DicomOPT> _PACSDicomOPTs = new List<PACS_DicomOPT>();
		public virtual IList<PACS_DicomOPT> PACS_DicomOPTs
		{
			get
			{
				return this._PACSDicomOPTs;
			}
		}
		
	}
}
#pragma warning restore 1591