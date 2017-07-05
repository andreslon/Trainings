using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class StudiesRepository : EntityBaseRepository<PACS_Trial>, IStudiesRepository
    {
        #region Constructor

        public StudiesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<PACS_Trial> GetAll(string userId, bool? isActive, bool? isLocked, string search)
        {
            var query = GetAll();

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
                    case "project manager":
                        query = query.Where(x => x.CONTACT_UserTrials.Any(ut => ut.IsActive && ut.UserID == u.UserID));
                        break;
                    default:
                        query = query.Where(x => x.IsActive && x.CONTACT_UserTrials.Any(ut => ut.IsActive && ut.UserID == u.UserID));
                        break;
                }
            }

            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive);
            }

            if (isLocked.HasValue)
            {
                query = query.Where(x => x.IsLocked == isLocked);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                //trials = trials.Where(x => x.TrialName.Contains(search)
                //    || x.TrialAlias.Contains(search)
                //    || x.PrimaryDrugs.Contains(search)
                //    || (x.CFGAnimalSpecy == null ? false: x.CFGAnimalSpecy.AnimalSpeciesName.Contains(search)
                //        || x.CFGAnimalSpecy.AnimalSpeciesDisplayName.Contains(search)));

                query = from trial in query
                         join specy in Context.CFG_AnimalSpecies on trial.AnimalSpeciesID equals specy.AnimalSpeciesID into species
                         from specy in species.DefaultIfEmpty()
                         where trial.TrialName.Contains(search)
                            || trial.TrialAlias.Contains(search)
                            || trial.PrimaryDrugs.Contains(search)
                            || (specy != null ? specy.AnimalSpeciesName.Contains(search) || specy.AnimalSpeciesDisplayName.Contains(search) : false)
                         select trial;
            }

            return query;
        }

        public override void Delete(PACS_Trial entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<PACS_Trial, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        public int GetTotalSubjects(PACS_Trial entity)
        {
            if (entity.IsTestingPhase)
            {
                return DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.PACS_Subjects.Count(x => x.PACSSite.TrialID == entity.TrialID
                        && x.IsActive == true);
                });
            }
            else
            {
                return DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.PACS_Subjects.Count(x => x.PACSSite.TrialID == entity.TrialID
                        && x.IsActive == true && x.IsTestingSubject == false && x.PACSSite.IsTestingSite == false);
                });
            }
        }

        public int GetTotalQueriesPending(PACS_Trial entity)
        {
            var result = 0;
            if (entity != null)
            {
                var query = from q in Context.QRY_Queries
                            join qstatus in Context.QRY_Status on q.StatusID equals qstatus.StatusID
                            //serie
                            join serie in Context.PACS_Series on q.SeriesID equals serie.SeriesID into series
                            from serie in series.DefaultIfEmpty()
                            join visit in Context.PACS_TimePoints on serie.TimePointsID equals visit.TimePointsID into visits
                            from visit in visits.DefaultIfEmpty()
                            join subject in Context.PACS_Subjects on visit.SubjectID equals subject.SubjectID into subjects
                            from subject in subjects.DefaultIfEmpty()
                            join site in Context.PACS_Sites on subject.SiteID equals site.SiteID into sites
                            from site in sites.DefaultIfEmpty()
                                //ceruser
                            join certuser in Context.CERT_Users on q.CertUserID equals certuser.CertUserID into certusers
                            from certuser in certusers.DefaultIfEmpty()
                            join usertrial in Context.CONTACT_UserTrials on certuser.UserTrialID equals usertrial.UserTrialID into usertrials
                            from usertrial in usertrials.DefaultIfEmpty()
                            join cuuser in Context.CONTACT_Users on usertrial.UserID equals cuuser.UserID into cuusers
                            from cuuser in cuusers.DefaultIfEmpty()
                                //certequipment
                            join certequipment in Context.CERT_Equipments on q.CertEquipmentID equals certequipment.CertEquipmentID into certequipments
                            from certequipment in certequipments.DefaultIfEmpty()
                            join ceequipment in Context.CONTACT_Equipments on certequipment.EquipmentID equals ceequipment.EquipmentID into ceequipments
                            from ceequipment in ceequipments.DefaultIfEmpty()
                            select new
                            {
                                q = q,
                                qstatus = qstatus,
                                serie = serie,
                                visit = visit,
                                subject = subject,
                                site = site,
                                certuser = certuser,
                                usertrial = usertrial,
                                cuuser = cuuser,
                                certequipment = certequipment,
                                ceequipment = ceequipment,
                            };

                var andFilters = new List<string>();
                var parameters = new Dictionary<string, object>();

                var f = @"q.TrialID == @studyId";
                andFilters.Add(f);
                parameters.Add("@studyId", entity.TrialID);

                f = @"q.IsActive && !q.IsResolved";
                andFilters.Add(f);

                var filter = CombinePredicates(andFilters, "&&");
                result = query.Where(filter, parameters).Count();
            }

            return result;
        }

        public int GetTotalQueriesFlagged(PACS_Trial entity, CONTACT_User user)
        {
            var result = 0;
            if (entity != null)
            {
                var query = from q in Context.QRY_Queries
                            join qstatus in Context.QRY_Status on q.StatusID equals qstatus.StatusID
                            join sender in Context.CONTACT_Users on q.SenderID equals sender.UserID
                            //serie
                            join serie in Context.PACS_Series on q.SeriesID equals serie.SeriesID into series
                            from serie in series.DefaultIfEmpty()
                            join visit in Context.PACS_TimePoints on serie.TimePointsID equals visit.TimePointsID into visits
                            from visit in visits.DefaultIfEmpty()
                            join subject in Context.PACS_Subjects on visit.SubjectID equals subject.SubjectID into subjects
                            from subject in subjects.DefaultIfEmpty()
                            join site in Context.PACS_Sites on subject.SiteID equals site.SiteID into sites
                            from site in sites.DefaultIfEmpty()
                                //ceruser
                            join certuser in Context.CERT_Users on q.CertUserID equals certuser.CertUserID into certusers
                            from certuser in certusers.DefaultIfEmpty()
                            join usertrial in Context.CONTACT_UserTrials on certuser.UserTrialID equals usertrial.UserTrialID into usertrials
                            from usertrial in usertrials.DefaultIfEmpty()
                            join cuuser in Context.CONTACT_Users on usertrial.UserID equals cuuser.UserID into cuusers
                            from cuuser in cuusers.DefaultIfEmpty()
                                //certequipment
                            join certequipment in Context.CERT_Equipments on q.CertEquipmentID equals certequipment.CertEquipmentID into certequipments
                            from certequipment in certequipments.DefaultIfEmpty()
                            join ceequipment in Context.CONTACT_Equipments on certequipment.EquipmentID equals ceequipment.EquipmentID into ceequipments
                            from ceequipment in ceequipments.DefaultIfEmpty()
                            select new
                            {
                                q = q,
                                qstatus = qstatus,
                                sender = sender,
                                serie = serie,
                                visit = visit,
                                subject = subject,
                                site = site,
                                certuser = certuser,
                                usertrial = usertrial,
                                cuuser = cuuser,
                                certequipment = certequipment,
                                ceequipment = ceequipment,
                            };

                var andFilters = new List<string>();
                var parameters = new Dictionary<string, object>();

                var f = @"q.TrialID == @studyId";
                andFilters.Add(f);
                parameters.Add("@studyId", entity.TrialID);

                f = @"q.IsActive && !q.IsResolved";
                andFilters.Add(f);

                parameters.Add("@uaffiliationId", user.AffiliationID);
                switch (user.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                    case "project manager":
                    case "manager":
                    ////f = @"qstatus.StatusName == ""Pending Resolution""";
                    ////break;
                    case "super user":
                    case "data quality evaluator":
                        //TO DO: Filter of query creator
                        f = @"qstatus.StatusName == ""Pending Resolution"" && sender.AffiliationID == @uaffiliationId";
                        break;
                    case "site coordinator":
                    case "ophthalmic technician":
                        //TO DO: Filter affiliation of target
                        f = @"qstatus.StatusName == ""Pending Response"" && ((site != null ? site.AffiliationID == @uaffiliationId : false)
                            || (ceequipment != null ? ceequipment.AffiliationID == @uaffiliationId : false)
                            || (cuuser != null ? cuuser.AffiliationID == @uaffiliationId : false))";
                        break;
                    default:
                        f = "false";
                        break;
                }
                andFilters.Add(f);

                var filter = CombinePredicates(andFilters, "&&");
                result = query.Where(filter, parameters).Count();
            }

            return result;
        }

        public IQueryable<CERT_ImgProcedureList> GetProcedures(long id)
        {
            return Context.CERT_ImgProcedureLists.Where(x => x.PACS_Trials.Any(y => y.TrialID == id));
        }

        public CERT_ImgProcedureList AddProcedure(PACS_Trial entity, CERT_ImgProcedureList procedure)
        {
            if (!entity.CERT_ImgProcedureLists.Contains(procedure))
                entity.CERT_ImgProcedureLists.Add(procedure);

            return procedure;
        }

        #endregion
    }
}