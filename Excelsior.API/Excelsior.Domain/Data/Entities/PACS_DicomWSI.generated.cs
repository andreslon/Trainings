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
	public partial class PACS_DicomWSI : PACS_RawDatum
	{
		private long? _WSIImageWidth;
		public virtual long? WSIImageWidth
		{
			get
			{
				return this._WSIImageWidth;
			}
			set
			{
				this._WSIImageWidth = value;
			}
		}
		
		private long? _WSIImageHeight;
		public virtual long? WSIImageHeight
		{
			get
			{
				return this._WSIImageHeight;
			}
			set
			{
				this._WSIImageHeight = value;
			}
		}
		
		private double? _PixelSpacingX;
		public virtual double? PixelSpacingX
		{
			get
			{
				return this._PixelSpacingX;
			}
			set
			{
				this._PixelSpacingX = value;
			}
		}
		
		private double? _PixelSpacingY;
		public virtual double? PixelSpacingY
		{
			get
			{
				return this._PixelSpacingY;
			}
			set
			{
				this._PixelSpacingY = value;
			}
		}
		
		private int? _TileSizeY;
		public virtual int? TileSizeY
		{
			get
			{
				return this._TileSizeY;
			}
			set
			{
				this._TileSizeY = value;
			}
		}
		
		private int? _TileSizeX;
		public virtual int? TileSizeX
		{
			get
			{
				return this._TileSizeX;
			}
			set
			{
				this._TileSizeX = value;
			}
		}
		
		private short? _TileOverlap;
		public virtual short? TileOverlap
		{
			get
			{
				return this._TileOverlap;
			}
			set
			{
				this._TileOverlap = value;
			}
		}
		
		private string _TileFormat;
		public virtual string TileFormat
		{
			get
			{
				return this._TileFormat;
			}
			set
			{
				this._TileFormat = value;
			}
		}
		
	}
}
#pragma warning restore 1591
