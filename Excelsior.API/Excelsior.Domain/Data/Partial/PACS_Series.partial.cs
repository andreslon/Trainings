using System.Linq;
using System.Xml.Serialization;
using Telerik.OpenAccess;

namespace Excelsior.Domain
{
    public partial class PACS_Series : IInstanceCallbacks
    {
        private bool _HasReport;
        [XmlIgnore]
        [Transient]
        public bool HasReport
        {
            get
            {
                return _HasReport;
            }
            set
            {
                _HasReport = value;
            }
        }

        [XmlIgnore]
        [Transient]
        public int TotalMedia
        {
            get
            {
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    return dm.PACS_RawData.Count(item => item.IsActive && item.SeriesID == this.SeriesID);
                }
                return 0;
            }
            set
            {
            }
        }

        [XmlIgnore]
        [Transient]
        public int TotalUploads
        {
            get
            {
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    return dm.UPLD_UploadInfos.Count(item => item.IsActive && item.SeriesID == this.SeriesID);
                }
                return 0;
            }
            set
            {
            }
        }

        [XmlIgnore]
        [Transient]
        public int TotalActiveQueries
        {
            get
            {
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    return dm.QRY_Queries.Count(item => item.SeriesID == this.SeriesID && !item.IsResolved);
                }
                return 0;
            }
            set
            {
            }
        }

        #region IInstanceCallbacks Implementation

        public void PostLoad()
        {
        }

        public void PreRemove(IObjectScope objectScope)
        {
        }

        public void PreStore()
        {
            DataModel context = DataModel.GetContext(this) as DataModel;
            if (context != null && context.GetState(this) == ObjectState.Dirty)
            {
                var key = context.CreateObjectKey(this);
                //context.AddChangedAudit(key);
            }
        }

        #endregion
    }
}