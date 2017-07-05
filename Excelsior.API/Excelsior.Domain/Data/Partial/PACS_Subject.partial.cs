using System.Xml.Serialization;
using Telerik.OpenAccess;
using System.Linq;

namespace Excelsior.Domain
{
    public partial class PACS_Subject : IInstanceCallbacks
    {
        [XmlIgnore]
        [Transient]
        public int TotalActiveQueries
        {
            get
            {
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    return dm.QRY_Queries.Count(item => !item.IsResolved && item.PACSSeries != null && item.PACSSeries.PACSTimePoint.SubjectID == this.SubjectID);
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