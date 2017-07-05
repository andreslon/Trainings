using Excelsior.Domain.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class VisitMatrixRepository : IVisitMatrixRepository
    {
        public DataModel Context { get; set; }

        public VisitMatrixRepository(DataModel context)
        {
            Context = context;
        }

        public IQueryable<PACS_Subject> GetSubjects(long? siteId, string userId, string search, long? procedureId, long? stepId)
        {
            //var u = db.CONTACT_Users.Include(c => c.Role).SingleOrDefault(item => item.AspUserID.ToString() == userId);

            var site = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.PACS_Sites.Single(x => x.SiteID == siteId);
            });

            IQueryable<PACS_Subject> subjects = null;

            if (stepId.HasValue)
            {
                var query = Context.WF_Sequences.Where(x => x.IsActive && x.WFTempStep.WFStepListID == stepId.Value);

                if (procedureId.HasValue)
                {
                    subjects = query.Where(x => x.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == procedureId.Value
                        && x.PACSTimePoint.PACSSubject.IsActive 
                        && x.PACSTimePoint.PACSSubject.SiteID == siteId).Select(x => x.PACSTimePoint.PACSSubject).Distinct();
                }
                else
                {
                    subjects = query.Where(x => x.PACSTimePoint.PACSSubject.IsActive 
                        && x.PACSTimePoint.PACSSubject.SiteID == siteId).Select(x => x.PACSTimePoint.PACSSubject).Distinct();
                }
            }
            else
            {
                subjects = Context.PACS_Subjects
                    .Where(x => x.SiteID == siteId && x.IsActive);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                subjects = subjects.Where(x => x.AlternativeRandomizedSubjectID.Contains(search)
                    || x.RandomizedSubjectID.Contains(search)
                    || x.NameCode.Contains(search));
            }

            if (!site.PACSTrial.IsTestingPhase)
            {
                subjects = subjects.Where(x => !x.IsTestingSubject);
            }

            return subjects.Select(x => x);
        }

        public IQueryable<PACS_TimePointsList> GetTimePoints(long? siteId)
        {
            var site = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.PACS_Sites.Single(x => x.SiteID == siteId);
            });

            //Get expected procedures
            return Context.PACS_TimePointsLists
                .Where(x => x.TrialID == site.TrialID).OrderBy(x => x.TimePointsSeq);
        }

        public KeyValuePair<string, WF_Sequence> GetTimePointStatus(IEnumerable<WF_Sequence> seriesList, long timePointsListId, long? procedureId, long? stepId)
        {
            //Get expected procedures
            var proceduresCount = 0;
            var proceduresQuery = Context.PACS_TPProcLists
                .Where(x => x.TimePointsListID == timePointsListId);

            if (procedureId.HasValue)
            {
                proceduresCount = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return proceduresQuery.Count(x => x.ImgProcedureID == procedureId.Value);
                });
            }
            else
            {
                proceduresCount = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return proceduresQuery.Count();
                });
            }

            //Get submitted series
            var series = seriesList.Where(x => x.IsActive && x.PACSTimePoint.TimePointsListID == timePointsListId);
            var seriesCount = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return series.Count();
            });

            var seriesQuery = series;
            if (procedureId.HasValue)
            {
                seriesQuery = series.Where(x => x.PACSTPProcList.ImgProcedureID == procedureId.Value);
            }

            //Calculate status
            if (proceduresCount == 0)
            {
                return new KeyValuePair<string, WF_Sequence>("NA", null);
            }
            else if (stepId.HasValue)
            {
                var stepQuery = seriesQuery.Where(x => x.WFTempStep.WFStepListID == stepId.Value);
                var sqCount = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return stepQuery.Count();
                });
                if (sqCount > 0)
                {
                    var serie = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return stepQuery.FirstOrDefault();
                    });
                    if (serie != null)
                    {
                        if (sqCount == 1)
                            return new KeyValuePair<string, WF_Sequence>(serie.WFTempStep.WFStepList.WFStepListDes, serie);

                        return new KeyValuePair<string, WF_Sequence>(serie.WFTempStep.WFStepList.WFStepListDes, null);
                    }
                }
            }
            else if (proceduresCount == 1 && seriesCount == 1)
            {
                var serie = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return seriesQuery.FirstOrDefault();
                });
                if (serie != null)
                {
                    // #108 Changes to visit matrix series status and terminology
                    string wfStepDes = serie.WFTempStep.WFStepList.WFStepListDes;
                    if (wfStepDes == "Completed")
                        return new KeyValuePair<string, WF_Sequence>(wfStepDes, serie);

                    return new KeyValuePair<string, WF_Sequence>("In Progress", null);
                }
            }
            else
            {
                var completedCount = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return seriesQuery.Select(x => x.WFTempStep.WFStepList.WFStepListDes).Count(x => x == "Completed");
                });
                if (completedCount == proceduresCount)
                    return new KeyValuePair<string, WF_Sequence>("Completed", null);

                var isInProgress = seriesQuery.Any(x => true);
                if (isInProgress)
                    return new KeyValuePair<string, WF_Sequence>("In Progress", null);
            }

            return new KeyValuePair<string, WF_Sequence>("Empty", null);
        }

        public IQueryable<CERT_ImgProcedureList> GetTimePointProcedures(long timePointsListId)
        {
            //Get expected procedures
            return Context.PACS_TPProcLists
                .Where(x => x.TimePointsListID == timePointsListId).Select(x => x.CERTImgProcedureList).OrderBy(x => x.ImgProcedureName);
        }

        public IQueryable<CERT_ImgProcedureList> GetSiteProcedures(long? siteId)
        {
            var site = Context.PACS_Sites.Single(x => x.SiteID == siteId);

            //Get expected procedures
            var query = Context.PACS_TPProcLists
                .Where(x => x.PACSTimePointsList.TrialID == site.TrialID && x.CERTImgProcedureList != null)                
                .Select(x => x.CERTImgProcedureList)
                .Distinct();

            return query;
        }        

        public WF_Sequence GetSeries(long subjectId, long timePointsListId, long procedureId, long? stepId)
        {
            var query = Context.WF_Sequences
                .Where(x => x.IsActive
                    && x.PACSTimePoint.SubjectID == subjectId
                    && x.PACSTimePoint.TimePointsListID == timePointsListId
                    && x.PACSTPProcList.ImgProcedureID == procedureId);

            if (stepId.HasValue)
            {
                query = query.Where(x => x.WFTempStep.WFStepListID == stepId);
            }

            return DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return query.FirstOrDefault();
            });
        }

        public IQueryable<WF_Sequence> GetSeries(long subjectId, long timePointsListId)
        {
            return Context.WF_Sequences.Where(x => x.IsActive
                && x.PACSTimePoint.SubjectID == subjectId
                && x.PACSTimePoint.TimePointsListID == timePointsListId);
        }

        public IQueryable<WF_Sequence> GetSeries(long subjectId)
        {
            return Context.WF_Sequences.Where(x => x.IsActive
                && x.PACSTimePoint.SubjectID == subjectId);
        }

        public string GetFrameLocation(long subjectId, long timePointListId, long procedureId)
        {
            var serie = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.WF_Sequences
                .FirstOrDefault(x => x.IsActive
                    && x.PACSTimePoint.SubjectID == subjectId
                    && x.PACSTimePoint.TimePointsListID == timePointListId
                    && x.PACSTPProcList.ImgProcedureID == procedureId);
            });
              
            if (serie != null)
            {
                var rawData = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return serie.PACS_RawData.FirstOrDefault();
                });
                if (rawData != null)
                {
                    var frame = DataHelpers.RetryPolicy.ExecuteAction(() =>
                    {
                        return rawData.PACS_DicomFrames.FirstOrDefault();
                    });
                    if (frame != null)
                    {
                        return frame.FrameImageLocation;
                    }
                }
            }

            return null;
        }
    }
}