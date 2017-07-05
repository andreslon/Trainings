using System;
using System.Linq;
using Telerik.OpenAccess;

namespace Excelsior.Domain
{
    public partial class CERT_User : IInstanceCallbacks
    {
        public DateTime? LastSubmissionDate
        {
            get
            {
                if (this.CONTACTUserTrial == null)
                    return null;
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    var ls = dm.PACS_Series.OrderByDescending(s => s.StudyDate).FirstOrDefault(s => s.IsActive && s.PACSTimePoint.PACSSubject.PACSSite.TrialID == this.CONTACTUserTrial.TrialID && s.PhotographerID == this.CONTACTUserTrial.UserID && s.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == this.ImgProcedureID);
                    if (ls != null)
                        return ls.StudyDate;
                }
                return null;
            }
            set { }
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