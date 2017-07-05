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
	public partial class PACS_ProcessedDatum
	{
		private long _ProcessedDataID;
		public virtual long ProcessedDataID
		{
			get
			{
				return this._ProcessedDataID;
			}
			set
			{
				this._ProcessedDataID = value;
			}
		}
		
		private long? _RawDataID;
		public virtual long? RawDataID
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
		
		private string _ProcessedDataLabel;
		public virtual string ProcessedDataLabel
		{
			get
			{
				return this._ProcessedDataLabel;
			}
			set
			{
				this._ProcessedDataLabel = value;
			}
		}
		
		private string _ProcessDataXMLString;
		public virtual string ProcessDataXMLString
		{
			get
			{
				return this._ProcessDataXMLString;
			}
			set
			{
				this._ProcessDataXMLString = value;
			}
		}

        private DateTime? _DateCreated;
        public virtual DateTime? DateCreated
        {
            get
            {
                return this._DateCreated;
            }
            set
            {
                this._DateCreated = value;
            }
        }

        private DateTime? _DateModified;
        public virtual DateTime? DateModified
        {
            get
            {
                return this._DateModified;
            }
            set
            {
                this._DateModified = value;
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
		
		private long? _DicomFrameID;
		public virtual long? DicomFrameID
		{
			get
			{
				return this._DicomFrameID;
			}
			set
			{
				this._DicomFrameID = value;
			}
		}
		
		private PACS_RawDatum _PACSRawDatum;
		public virtual PACS_RawDatum PACSRawDatum
		{
			get
			{
				return this._PACSRawDatum;
			}
			set
			{
				this._PACSRawDatum = value;
			}
		}
		
		private PACS_DicomFrame _PACSDicomFrame;
		public virtual PACS_DicomFrame PACSDicomFrame
		{
			get
			{
				return this._PACSDicomFrame;
			}
			set
			{
				this._PACSDicomFrame = value;
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
		
	}
}
#pragma warning restore 1591
