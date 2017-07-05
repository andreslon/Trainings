using System.Linq;
using System.Xml.Serialization;
using Telerik.OpenAccess;

namespace Excelsior.Domain
{
    public partial class PACS_Trial : IInstanceCallbacks
    {
        [XmlIgnore]
        [Transient]
        public int TotalSubjects
        {
            get
            {
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    if(IsTestingPhase)
                        return dm.PACS_Subjects.Count(item => item.PACSSite.TrialID == this.TrialID && item.IsActive);
                    else
                        return dm.PACS_Subjects.Count(item => item.PACSSite.TrialID == this.TrialID && item.IsActive && !item.IsTestingSubject && !item.PACSSite.IsTestingSite);
                }
                return 0;
            }
            set
            {
            }
        }

        [XmlIgnore]
        [Transient]
        public int TotalSubjectGroups
        {
            get
            {
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    return dm.PACS_SubjectGroups.Count(item => item.TrialID == this.TrialID);
                }
                return 0;
            }
            set
            {
            }
        }

        [XmlIgnore]
        [Transient]
        public int TotalSubjectCohorts
        {
            get
            {
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    return PACS_SubjectCohorts.Count(item => item.TrialID == this.TrialID);
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