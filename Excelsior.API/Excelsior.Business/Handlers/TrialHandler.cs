using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Logic
{
    public class TrialHandler
    {
        public DataModel db { get; set; }
        public TrialHandler(DataModel context)
        {
            db = context;
        }
        public IQueryable<PACS_Trial> GetTrialsByUserId(TrialsRequestDto trial)
        {
            var u = db.CONTACT_Users.FirstOrDefault(item => item.IsActive && item.AspUserID.ToString() == trial.UserId);

            IQueryable<PACS_TrialKeyMetric> trialKey = null;

            var roles = new List<string> { };
            if (u.AspnetRole.LoweredRoleName == "administrator")
                trialKey = db.PACS_TrialKeyMetrics;
            else if (u.AspnetRole.LoweredRoleName == "project manager")
                trialKey = db.PACS_TrialKeyMetrics.Where(item => item.CONTACT_UserTrials.Any(ut => ut.IsActive && ut.UserID == u.UserID));
            else
                trialKey = db.PACS_TrialKeyMetrics.Where(item => item.IsActive && item.CONTACT_UserTrials.Any(ut => ut.IsActive && ut.UserID == u.UserID));

            if (trial.IsActive != null)
            {
                trialKey = trialKey.Where(item => item.IsActive == trial.IsActive);
            }

            if (trial.IsLocked != null)
            {
                trialKey = trialKey.Where(item => item.IsLocked == trial.IsLocked);
            }

            var lst = trialKey.Select(item => item);

            return lst;

        }

        public int GetTotalSubjects(long TrialID, bool IsTestingPhase) {
            if (db != null)
            {
                if (IsTestingPhase)
                {
                    return DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return db.PACS_Subjects.Count(item => item.PACSSite.TrialID == TrialID && item.IsActive);
                    });
                }
                else
                {
                    return DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return db.PACS_Subjects.Count(item => item.IsActive && !item.IsTestingSubject
                            && item.PACSSite.TrialID == TrialID
                            && !item.PACSSite.IsTestingSite);
                    }); 
                }

            }
            return 0;
        }
    }
}
