using Excelsior.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class AuthScopeRepository : EntityBaseRepository<AUTH_Scope>, IAuthScopeRepository
    {
        #region Constructor
        public AuthScopeRepository(DataModel context) : base(context)
        {
        }
        #endregion

        #region Functions
        
        public IQueryable<AUTH_Scope> FindClient(string scopeId)
        {
            var IddG = Guid.Parse(scopeId);
            var scopes = FindBy(x => x.ScopeId == IddG);
            return scopes.Select(x => x);

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
            var cUsers = Context.CERT_Users;
            if (trialID == null)
            {
                cUsers = cUsers.Join(Context.PACS_Sites,
                    t => new { TrialID = t.CONTACTUserTrial.TrialID, AffiliationID = t.CONTACTUserTrial.CONTACTUser.AffiliationID }, s => new { TrialID = s.TrialID, AffiliationID = s.AffiliationID }, (t, s) =>
                    new
                    {
                        Tech = t,
                        Site = s
                    }).Where(n => (n.Site.PACSTrial.IsTestingPhase ? true : !n.Site.IsTestingSite)).Select(n => n.Tech);
            }
            else
            {
                var trial = Context.PACS_Trials.Single(t => t.TrialID == trialID);

                var cuws = Context.CERT_Users.Where(cu => cu.CONTACTUserTrial.TrialID == trialID).Join(Context.PACS_Sites.Where(s => s.TrialID == trialID),
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
        #endregion
    }
}
