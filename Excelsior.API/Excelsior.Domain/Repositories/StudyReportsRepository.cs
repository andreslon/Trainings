using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class StudyReportsRepository : EntityBaseRepository<RPT_TrialReport>, IStudyReportsRepository
    {
        #region Constructor
        public StudyReportsRepository(DataModel context) : base(context)
        {
        }
        #endregion

        #region Functions
        public IQueryable<RPT_TrialReport> GetAll(string userId, long? studyId)
        {
            var query = FindBy(x => x.RPTReport.IsActive);
            //find the user
            CONTACT_User u = null;
            if (!string.IsNullOrEmpty(userId))
            {
                u = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.CONTACT_Users.SingleOrDefault(item => item.AspUserID.ToString() == userId);
                });
            }
            if (u != null)
            {
                var roles = new List<string> { };
                switch (u.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                        break;
                    default:
                        query = query.Where(x => x.RPT_TrialReportRoles.Any(y => y.RoleId == u.RoleId));
                        break;
                }
            }
            if (studyId.HasValue)
            {
                query = query.Where(x => x.TrialID == studyId.GetValueOrDefault());
            }
            return query;
        }
        #endregion
    }
}
