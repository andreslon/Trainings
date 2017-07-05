using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Logic
{
    public class EquipmentHandler
    {
        public DataModel db { get; set; }
        public EquipmentHandler(DataModel context)
        {
            db = context;
        }

        public int GetTotalCertifiedEquipmentByProcedure(long trialID, long EquipmentID, long procedureID)
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return GetCertEquipments().Count(eq => eq.IsActive && eq.TrialID == trialID && eq.EquipmentID == EquipmentID && eq.ImgProcedureID == procedureID && eq.IsCertified);
            });
            return result;
        }

        private List<CERT_Equipment> GetCertEquipments(long? trialID = null)
        {
            var eq = db.CERT_Equipments;
            var si = db.PACS_Sites;
            if (trialID == null)
            {
                var d = eq.Join(si,
                    e => new { TrialID = e.TrialID, AffiliationID = e.CONTACTEquipment.AffiliationID }
                    , s => new { TrialID = s.TrialID, s.AffiliationID }, (e, s) =>
                        new
                        {
                            Equip = e,
                            Site = s
                        });
                var query = d.Where(n => n.Site.PACSTrial.IsTestingPhase ? true : !n.Site.IsTestingSite).Select(n => n.Equip);
                var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return query.ToList();
                });
                return result;
            }
            else
            {
                var trial = db.PACS_Trials.Single(t => t.TrialID == trialID);

                var cews = eq.Where(ce => ce.TrialID == trialID).Join(si.Where(s => s.TrialID == trialID),
                    e => e.CONTACTEquipment.AffiliationID
                    , s => s.AffiliationID, (e, s) =>
                        new
                        {
                            Equip = e,
                            Site = s
                        });

                IQueryable<CERT_Equipment> query;
                if (trial.IsTestingPhase)
                    query = cews.Select(n => n.Equip).Distinct();
                else
                    query = cews.Where(n => n.Site.IsTestingSite == false).Select(n => n.Equip).Distinct();

                var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return query.ToList();
                });
                return result;
            }
        }
    }
}
