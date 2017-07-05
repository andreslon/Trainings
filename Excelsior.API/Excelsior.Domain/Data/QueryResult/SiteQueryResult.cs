using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain
{
    public class SiteQueryResult
    {
        public PACS_Site site { get; set; }
        public CONTACT_Affiliation affiliation { get; set; }
        public CONTACT_Country country { get; set; }
    }
}
