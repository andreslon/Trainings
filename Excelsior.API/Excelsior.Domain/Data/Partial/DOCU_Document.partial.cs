using Telerik.OpenAccess;
using System.Linq;

namespace Excelsior.Domain
{
    public partial class DOCU_Document : IInstanceCallbacks
    {
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