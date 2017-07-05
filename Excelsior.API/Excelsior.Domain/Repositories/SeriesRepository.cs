using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace Excelsior.Domain.Repositories
{
    public class SeriesRepository : EntityBaseRepository<WF_Sequence>, ISeriesRepository
    {
        #region Constructor

        public SeriesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        protected override string ApplySortField(string item)
        {
            var split = item.Trim().Split(' ');
            var c = split.Count();
            var field = split[0];
            var dir = "asc";
            if (c > 1)
            {
                dir = split[1];
            }
            switch (field)
            {
                case "laststepcompletiondate":
                    return string.Format("series.LastStepCompletionDate {0}", dir);
                case "colorcategoryname":
                    return string.Format("categoryflag.CategoryDes {0}", dir);
                case "timepointname":
                    return string.Format("timepoint.TimePointsDescription {0}", dir);
                case "procedurename":
                    return string.Format("timepoint.TimePointsDescription {0}", dir);
                case "sitename":
                    return string.Format("site.RandomizedSiteID {0}, affiliation.AffiliationName {0}", dir);
                case "technicianname":
                    return string.Format("tech.LastName {0}, tech.FirstName {0}", dir);
                case "equipmentname":
                    return string.Format("emodel.ManufacturerModel {0}, emodel.EquipmentType {0}, equipment.MainSerialNum {0}", dir);
                case "assignedtoname":
                    return string.Format("assignedto.LastName {0}, assignedto.FirstName {0}", dir);
                case "subjectrandomizedid":
                    return string.Format("subject.RandomizedSubjectID {0}", dir);
                case "subjectalternativerandomizedid":
                    return string.Format("subject.AlternativeRandomizedSubjectID {0}", dir);
                case "subjectnamecode":
                    return string.Format("subject.NameCode {0}", dir);
                case "workflowstepname":
                    return string.Format("wfstep.WFStepListDes {0}", dir);
                default:
                    return "";
            }
        }

        public IQueryable<WF_Sequence> GetAll(string aspUserId, long studyId, string step, long? categoryId, string dataType, long? timePointListId, long? procedureId, long? subjectId, long? siteId, string assignedTo, long? seriesGroupId, long? subjectGroupId, long? subjectCohortId, string search, string sort)
        {
            var trial = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.PACS_Trials.Single(t => t.TrialID == studyId);
            });

            var entities = GetAll();
            var query = from series in entities
                        join categoryflag in Context.WF_CategoryFlags on series.CategoryFlagID equals categoryflag.CategoryFlagID into categories
                        from categoryflag in categories.DefaultIfEmpty()
                        join schedule in Context.PACS_TPProcLists on series.TPProcListID equals schedule.TPProcID
                        join procedure in Context.CERT_ImgProcedureLists on schedule.ImgProcedureID equals procedure.ImgProcedureID
                        join timepoint in Context.PACS_TimePointsLists on schedule.TimePointsListID equals timepoint.TimePointsListID
                        join visit in Context.PACS_TimePoints on series.TimePointsID equals visit.TimePointsID
                        join subject in Context.PACS_Subjects on visit.SubjectID equals subject.SubjectID
                        join site in Context.PACS_Sites on subject.SiteID equals site.SiteID
                        join affiliation in Context.CONTACT_Affiliations on site.AffiliationID equals affiliation.AffiliationID
                        join country in Context.CONTACT_Countries on affiliation.CountryID equals country.CountryID into countries
                        from country in countries.DefaultIfEmpty()
                        join equipment in Context.CONTACT_Equipments on series.EquipmentID equals equipment.EquipmentID
                        join emodel in Context.CONTACT_EquipmentModels on equipment.EquipmentModelID equals emodel.EquipmentModelID
                        join tech in Context.CONTACT_Users on series.PhotographerID equals tech.UserID
                        join assignedto in Context.CONTACT_Users on series.AssignedToID equals assignedto.UserID into assignees
                        from assignedto in assignees.DefaultIfEmpty()
                        join scohort in Context.PACS_SubjectCohorts on subject.SubjectCohortID equals scohort.SubjectCohortID into cohorts
                        from scohort in cohorts.DefaultIfEmpty()
                        join sgroup in Context.PACS_SubjectGroups on subject.SubjectGroupID equals sgroup.SubjectGroupID into groups
                        from sgroup in groups.DefaultIfEmpty()
                        join wftempstep in Context.WF_TempSteps on series.WFTempStepID equals wftempstep.WFTempStepID
                        join wfstep in Context.WF_StepLists on wftempstep.WFStepListID equals wfstep.WFStepListID
                        select new
                        {
                            series = series,
                            categoryflag = categoryflag,
                            schedule = schedule,
                            procedure = procedure,
                            timepoint = timepoint,
                            visit = visit,
                            subject = subject,
                            site = site,
                            affiliation = affiliation,
                            country = country,
                            equipment = equipment,
                            emodel = emodel,
                            tech = tech,
                            assignedto = assignedto,
                            scohort = scohort,
                            sgroup = sgroup,
                            wftempstep = wftempstep,
                            wfstep = wfstep
                        };

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            var f = @"series.IsActive == true && subject.IsActive == true && site.TrialID == @trialId";
            andFilters.Add(f);
            parameters.Add("@trialId", studyId);

            if (!string.IsNullOrWhiteSpace(step))
            {
                parameters.Add("@step", step.ToLower());
                var fi = @"wfstep.WFStepListDes.ToLower() == @step";
                andFilters.Add(fi);
                switch (step.ToLower())
                {
                    case "upload":
                    case "check-in":
                    case "grade":
                        var fii = @"procedure.PACSDataType.DataType != @oe
                            && procedure.PACSDataType.DataType != @ecrf 
                            && procedure.PACSDataType.DataType != @discrete";
                        andFilters.Add(fii);
                        parameters.Add("@oe", "OE");
                        parameters.Add("@ecrf", "eCRF");
                        parameters.Add("@discrete", "DISCRETE");
                        break;
                    case "verify":
                    case "completed":
                        break;
                    default:
                        return null;
                }
            }

            if (siteId.HasValue)
            {
                var fi = @"site.SiteID == @siteId";
                andFilters.Add(fi);
                parameters.Add("@siteId", siteId);
            }
            if (timePointListId.HasValue)
            {
                var fi = @"visit.TimePointsListID == @timePointListId";
                andFilters.Add(fi);
                parameters.Add("@timePointListId", timePointListId);
            }
            if (procedureId.HasValue)
            {
                var fi = @"schedule.ImgProcedureID == @procedureId";
                andFilters.Add(fi);
                parameters.Add("@procedureId", procedureId);
            }
            if (subjectId.HasValue)
            {
                var fi = @"visit.SubjectID == @subjectId";
                andFilters.Add(fi);
                parameters.Add("@subjectId", subjectId);
            }
            if (categoryId.HasValue)
            {
                if (categoryId == 0)
                {
                    var fi = @"series.CategoryFlagID == null";
                    andFilters.Add(fi);
                }
                else
                {
                    var fi = @"series.CategoryFlagID == @categoryId";
                    andFilters.Add(fi);
                    parameters.Add("@categoryId", categoryId);
                }
            }
            if (!string.IsNullOrEmpty(dataType))
            {
                var fi = @"procedure.PACSDataType.DataType == @dataType";
                andFilters.Add(fi);
                parameters.Add("@dataType", dataType);
            }
            if (seriesGroupId.HasValue)
            {
                var fi = @"series.SeriesGroupID == @seriesGroupId";
                andFilters.Add(fi);
                parameters.Add("@seriesGroupId", seriesGroupId);
            }
            if (subjectGroupId.HasValue)
            {
                var fi = @"subject.SubjectGroupID == @subjectGroupId";
                andFilters.Add(fi);
                parameters.Add("@subjectGroupId", subjectGroupId);
            }
            if (subjectCohortId.HasValue)
            {
                var fi = @"subject.SubjectCohortID == @subjectCohortId";
                andFilters.Add(fi);
                parameters.Add("@subjectCohortId", subjectCohortId);
            }

            var u = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.CONTACT_Users.SingleOrDefault(item => item.AspUserID.ToString() == aspUserId);
            });
            switch (u.AspnetRole.LoweredRoleName)
            {
                case "ophthalmic technician":
                case "site coordinator":
                    var fi = "site.AffiliationID == @affiliationId";
                    andFilters.Add(fi);
                    parameters.Add("@affiliationId", u.AffiliationID);
                    break;
                case "data quality evaluator":
                case "grader":
                case "reviewer":
                case "super user":
                    var rcps = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return Context.CONTACT_TrialReadingCenters.Where(item => item.TrialID == studyId
                            && item.AffiliationID == u.AffiliationID).Select(item => item.ImgProcedureID).Distinct().ToList();
                    });

                    if (rcps != null && rcps.Count() > 0)
                    {
                        f = "@rcprocedures.Contains(procedure.ImgProcedureID.ToString())";
                        andFilters.Add(f);
                        var sProcedures = string.Join("[{0}]", rcps);
                        parameters.Add("@rcprocedures", sProcedures);
                    }
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                switch (assignedTo.ToLower())
                {
                    case "me":
                        var f1 = @"series.AssignedToID == @assignedToId";
                        andFilters.Add(f1);
                        parameters.Add("@assignedToId", u.UserID);
                        break;
                    case "any":
                        var f2 = @"series.AssignedToID != null";
                        andFilters.Add(f2);
                        break;
                    case "none":
                        var f3 = @"series.AssignedToID == null";
                        andFilters.Add(f3);
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);

                if (!seriesGroupId.HasValue)
                {
                    var fi = @"(series.SeriesGroupID != null ? series.SeriesGroupID.Value.ToString().Contains(@search) : false)";
                    orFilters.Add(fi);
                }
                if (!timePointListId.HasValue)
                {
                    var fi = @"timepoint.TimePointsDescription.Contains(@search)";
                    orFilters.Add(fi);
                }
                if (!procedureId.HasValue)
                {
                    var fi = @"procedure.ImgProcedureName.Contains(@search)
                        || procedure.ImgProcedureDescription.Contains(@search)";
                    orFilters.Add(fi);
                }
                if (!subjectId.HasValue)
                {
                    var fi = @"subject.RandomizedSubjectID.Contains(@search)
                        || subject.AlternativeRandomizedSubjectID.Contains(@search)
                        || subject.NameCode.Contains(@search)";
                    orFilters.Add(fi);

                    if (!siteId.HasValue)
                    {
                        var fii = @"site.RandomizedSiteID.Contains(@search)
                        || affiliation.AffiliationName.Contains(@search)
                        || (country != null ? country.CountryName.Contains(@search) : false)";
                        orFilters.Add(fii);
                    }

                    if (!subjectCohortId.HasValue)
                    {
                        var fii = @"(scohort != null ? scohort.CohortName.Contains(@search) : false)";
                        orFilters.Add(fii);
                    }
                    if (!subjectGroupId.HasValue)
                    {
                        var fii = @"(sgroup != null ? sgroup.GroupName.Contains(@search)
                        || sgroup.GroupDescription.Contains(@search) : false)";
                        orFilters.Add(fii);
                    }
                }
                if (string.IsNullOrWhiteSpace(assignedTo) || (!string.IsNullOrWhiteSpace(assignedTo) && assignedTo.ToLower() == "any"))
                {
                    var fi = @"(assignedto != null ? assignedto.LastName.Contains(@search)
                        || assignedto.FirstName.Contains(@search)
                        || assignedto.LoweredUserName.Contains(@search)
                        || assignedto.Email.Contains(@search) : false)";
                    orFilters.Add(fi);
                }

                var f1 = @"equipment.MainSerialNum.Contains(@search)
                    || equipment.SeconarySerialNum.Contains(@search)
                    || equipment.SoftwareVersion.Contains(@search)
                    || equipment.Notes.Contains(@search)
                    || emodel.EquipmentType.Contains(@search)
                    || emodel.ManufacturerModel.Contains(@search)
                    || emodel.ManufacturerName.Contains(@search)
                    || tech.LastName.Contains(@search)
                    || tech.FirstName.Contains(@search)
                    || tech.LoweredUserName.Contains(@search)
                    || tech.Email.Contains(@search)";
                orFilters.Add(f1);
                var filter = CombinePredicates(orFilters, "||");
                if (andFilters.Count > 0)
                {
                    var andFilter = CombinePredicates(andFilters, "&&");
                    filter = CombinePredicates(new string[] { andFilter, filter }, "&&");
                }
                query = query.Where(filter, parameters);
            }
            else
            {
                if (andFilters.Count > 0)
                {
                    var filter = CombinePredicates(andFilters, "&&");
                    query = query.Where(filter, parameters);
                }
            }

            if (!trial.IsTestingPhase)
                query = query.Where(seq => seq.subject.IsTestingSubject == false && seq.visit.PACSSubject.PACSSite.IsTestingSite == false);

            IQueryable result;
            if (!string.IsNullOrWhiteSpace(sort))
            {
                result = ApplySortExpression(sort, query);
            }
            else
            {
                query = query.OrderBy(x => x.series.LastStepCompletionDate);
                result = query as IQueryable;
            }

            return result.Select("series").Cast<WF_Sequence>();
        }

        public override void Delete(WF_Sequence entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<WF_Sequence, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        public string GetSegmentationStatus(long seriesId)
        {
            var status = string.Empty;
            var rawdata = Context.PACS_RawData.Where(item => item.IsActive && item.SeriesID == seriesId && item.PACSDataType.DataType == "OPT");

            var total = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return rawdata.Count();
            });

            var complete = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return rawdata.Count(item => item.PACS_ProcessedData.Any(pd => pd.ProcessedDataLabel == "Layers"));
            });


            if (total > 0)
            {
                if (complete == total)
                {
                    status = "complete";
                }
                else
                {
                    var isPartial = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return rawdata.Any(item => item.PACS_ProcessedData.Any(pd => pd.ProcessedDataLabel == "Layers" || pd.ProcessedDataLabel == "Analysis File"));
                    });
                    if (isPartial)
                    {
                        status = "partial";
                    }
                }
            }
            return status;
        }

        public int GetTotalCertifiedUsers(long? studyId, long? userId, long? procedureId)
        {
            var count = 0;
            if (studyId.HasValue && userId.HasValue && procedureId.HasValue)
            {
                var users = GetCertUsers(false, studyId);
                count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return users.Count(us => us.CONTACTUserTrial.UserID == userId && us.ImgProcedureID == procedureId && us.IsCertified);
                });
            }

            return count;
        }

        private List<CERT_User> GetCertUsers(bool includeInactive, long? studyId = null)
        {
            var cUsers = Context.CERT_Users;
            if (studyId == null)
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
                var trial = Context.PACS_Trials.Single(t => t.TrialID == studyId);

                var cuws = Context.CERT_Users.Where(cu => cu.CONTACTUserTrial.TrialID == studyId).Join(Context.PACS_Sites.Where(s => s.TrialID == studyId),
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

        public int GetTotalCertifiedEquipment(long? studyId, long? equipmentId, long? procedureId)
        {
            var count = 0;
            if (studyId.HasValue && equipmentId.HasValue && procedureId.HasValue)
            {
                count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return GetCertEquipments().Count(eq => eq.IsActive && eq.TrialID == studyId && eq.EquipmentID == equipmentId && eq.ImgProcedureID == procedureId && eq.IsCertified);
                });
            }

            return count;
        }

        private List<CERT_Equipment> GetCertEquipments(long? studyId = null)
        {
            var eq = Context.CERT_Equipments;
            var si = Context.PACS_Sites;
            if (studyId == null)
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
                var trial = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return Context.PACS_Trials.Single(t => t.TrialID == studyId);
                });

                var cews = eq.Where(ce => ce.TrialID == studyId).Join(si.Where(s => s.TrialID == studyId),
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

        public IQueryable<PACS_SeriesComment> GetComments(long id)
        {
            return Context.PACS_SeriesComments.Where(x => x.SeriesID == id);
        }

        public int GetTotalComments(long? seriesId)
        {
            var result = 0;
            if (seriesId.HasValue)
            {
                result = Context.PACS_SeriesComments.Count(x => x.SeriesID == seriesId);
            }
            return result;
        }

        public int GetTotalUploads(long? seriesId)
        {
            var result = 0;
            if (seriesId.HasValue)
            {
                result = Context.UPLD_UploadInfos.Count(x => x.IsActive && x.SeriesID == seriesId);
            }
            return result;
        }

        public int GetTotalMedia(long? seriesId)
        {
            var result = 0;
            if (seriesId.HasValue)
            {
                result = Context.PACS_RawData.Count(x => x.IsActive && x.SeriesID == seriesId);
            }
            return result;
        }

        public int GetTotalQueriesPending(WF_Sequence entity)
        {
            var result = 0;
            if (entity != null)
            {
                var affiliationId = entity.PACSTimePoint.PACSSubject.PACSSite.AffiliationID;
                result = Context.QRY_Queries.Count(x => x.IsActive && !x.IsResolved && x.SeriesID == entity.SeriesID);
            }
            return result;
        }

        public int GetTotalQueriesFlagged(WF_Sequence entity, CONTACT_User user)
        {
            var result = 0;
            if (entity != null)
            {
                var affiliationId = entity.PACSTimePoint.PACSSubject.PACSSite.AffiliationID;
                var query = Context.QRY_Queries.Where(x => x.IsActive && !x.IsResolved && x.SeriesID == entity.SeriesID);

                var uaffiliationId = user.AffiliationID;
                switch (user.AspnetRole.LoweredRoleName)
                {
                    case "administrator":
                    case "project manager":
                    case "study manager":
                    case "super user":
                    case "data quality evaluator":
                        query = query.Where(x => x.QRYStatus.StatusName == "Pending Resolution" && x.Sender.AffiliationID == uaffiliationId);
                        break;
                    case "site coordinator":
                    case "ophthalmic technician":
                        query = query.Where(x => x.QRYStatus.StatusName == "Pending Response");
                        break;
                    default:
                        query = query.Where(x => false);
                        break;
                }
                result = query.Count();
            }
            return result;
        }

        public IQueryable<UPLD_UploadInfo> GetUploads(long id)
        {
            return Context.UPLD_UploadInfos.Where(x => x.SeriesID == id);
        }

        public IQueryable<PACS_RawDatum> GetMedia(long id)
        {
            return Context.PACS_RawData.Where(x => x.SeriesID == id);
        }

        public PACS_RawDatum AddMedia(PACS_Series entity, PACS_RawDatum media)
        {
            if (media.RawDataID <= 0)
            {
                Context.Add(media);
                media.SeriesID = entity.SeriesID;
                media.PACSSeries = entity;
            }
            else
            {
                Context.AttachCopy(media);
            }

            return media;
        }

        public PACS_SeriesComment AddComment(long id, string userId, string value)
        {
            var u = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.CONTACT_Users.SingleOrDefault(item => item.AspUserID.ToString() == userId);
            });

            var comment = new PACS_SeriesComment();
            comment.CommentText = value;
            comment.CreatedDate = DateTime.UtcNow;
            comment.SeriesID = id;
            comment.UserID = u.UserID;
            Context.Add(comment);
            return comment;
        }

        public IQueryable<AUDIT_Record> GetWorkflowAuditRecords(long id)
        {
            return Context.AUDIT_Records.Where(x => x.SeriesID == id && x.WFTempStepID != null).OrderByDescending(x => x.PerformedDateTime);
        }
    }
}
