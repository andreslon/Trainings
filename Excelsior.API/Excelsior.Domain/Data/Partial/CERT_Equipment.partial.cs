using System;
using System.Linq;
using Telerik.OpenAccess;

namespace Excelsior.Domain
{
    public partial class CERT_Equipment
    {
        public DateTime? LastSubmissionDate
        {
            get
            {
                var dm = DataModel.GetContext(this) as DataModel;
                if (dm != null)
                {
                    var ls = dm.PACS_Series.OrderByDescending(s => s.StudyDate).FirstOrDefault(s => s.IsActive && s.PACSTimePoint.PACSSubject.PACSSite.TrialID == this.TrialID && s.EquipmentID == this.EquipmentID && s.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == this.ImgProcedureID);
                    if (ls != null)
                        return ls.StudyDate;
                }
                return null;
            }
            set { }
        }
    }
}