using System.Linq;
using System.Xml.Serialization;
using Telerik.OpenAccess;

namespace Excelsior.Domain
{
    public partial class PACS_Site
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
                    if (PACSTrial != null && PACSTrial.IsTestingPhase)
                        return dm.PACS_Subjects.Count(item => item.SiteID == this.SiteID && item.IsActive);
                    else
                        return dm.PACS_Subjects.Count(item => item.SiteID == this.SiteID && item.IsActive && !item.IsTestingSubject);
                }
                return 0;
            }
            set
            {
            }
        }

        [XmlIgnore]
        [Transient]
        public string DisplayName
        {
            get
            {
                if (CONTACTAffiliation != null)
                    return string.Format("{0} ({1})", RandomizedSiteID, CONTACTAffiliation.AffiliationName);
                else
                    return RandomizedSiteID;
            }
            set { }
        }
    }
}