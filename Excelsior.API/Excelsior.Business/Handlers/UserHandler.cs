using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Logic
{
    public class UserHandler
    {
        public DataModel db { get; set; }
        public UserHandler(DataModel context)
        {
            db = context;
        }
        public Aspnet_Membership GetUser(string userName)
        {
            var user = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.Aspnet_Memberships.FirstOrDefault(x => x.UserName == userName);
            });
            return user;
        }

        public AUTH_Client GetClient(string clientId)
        {
            var clientIddG = Guid.Parse(clientId);
            var client = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.AUTH_Clients.FirstOrDefault(x => x.ClientId == clientIddG);
            });
            return client;
        }

        public List<AUTH_Client> GetClients()
        {
            var clients = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.AUTH_Clients.ToList();
            });
            return clients;
        }

        public List<AUTH_Scope> GetScopes()
        {
            var scopes = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.AUTH_Scopes.ToList();
            });
            return scopes;
        }

        public AUTH_Scope GetScope(string scopeId)
        {
            var scopeIddG = Guid.Parse(scopeId);
            var scope = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.AUTH_Scopes.Where(x => x.ScopeId == scopeIddG).ToList();
            });
            return scope != null ? scope.FirstOrDefault() : null;
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
            var cUsers= db.CERT_Users;
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
