using System.Linq;
using System.Xml.Serialization;
using Telerik.OpenAccess;

namespace Excelsior.Domain
{
    public partial class GRD_QuestionGroup : IInstanceCallbacks
    {
        [XmlIgnore]
        [Transient]
        public int TotalQuestions
        {
            get
            {
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    return dm.GRD_TempQuestions.Count(item => item.GQuestionGroupID == this.GQuestionGroupID);
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