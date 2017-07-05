using Excelsior.Business.Logic;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Repositories
{
    public class UserRepository
    {
        public DataModel db { get; set; }

        public UserRepository(DataModel context)
        {
            db = context;
        }

        public int GetTotalCertifiedUsersByProcedure(long trialID, long? userID, long? procedureID)
        {
            var count = 0;
            if (userID.HasValue && procedureID.HasValue)
            { 
                count =  GetTotalCertifiedUsersByProcedure(trialID, userID.Value, procedureID.Value);
            }

            return count;
        }


        public int GetTotalCertifiedUsersByProcedure(long trialID, long userID, long procedureID)
        {
            var users = GetCertUsers(false, trialID);
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return users.Count(us => us.CONTACTUserTrial.UserID == userID && us.ImgProcedureID == procedureID && us.IsCertified);
            });
            return result;
        }

        public List<CERT_User> GetCertUsers(bool includeInactive, long? trialID = null)
        {
            var cUsers = db.CERT_Users;
            if (trialID == null)
            {
                cUsers = cUsers.Join(db.PACS_Sites,
                    t => new { TrialID = t.CONTACTUserTrial.TrialID, AffiliationID = t.CONTACTUserTrial.CONTACTUser.AffiliationID }, s => new { TrialID = s.TrialID, AffiliationID = s.AffiliationID }, (t, s) =>
                    new
                    {
                        Tech = t,
                        Site = s
                    }).Where(n => (n.Site.PACSTrial.IsTestingPhase ? true : !n.Site.IsTestingSite)).Select(n => n.Tech);
            }
            else
            {
                var trial = db.PACS_Trials.Single(t => t.TrialID == trialID);

                var cuws = db.CERT_Users.Where(cu => cu.CONTACTUserTrial.TrialID == trialID).Join(db.PACS_Sites.Where(s => s.TrialID == trialID),
                    t => t.CONTACTUserTrial.CONTACTUser.AffiliationID, s => s.AffiliationID, (t, s) =>
                    new
                    {
                        Tech = t,
                        Site = s
                    });

                if (trial.IsTestingPhase)
                    cUsers = cuws.Select(n => n.Tech);
                else
                    cUsers = cuws.Where(n => !n.Site.IsTestingSite).Select(n => n.Tech);
            }

            if (!includeInactive)
                cUsers = cUsers.Where(n => n.IsActive);

            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return cUsers.ToList();
            });
            return result;
        }
    }
}