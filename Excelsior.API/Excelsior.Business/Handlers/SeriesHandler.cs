using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Logic
{
    public class SeriesHandler
    {
        public DataModel db { get; set; }

        public SeriesHandler(DataModel context)
        {
            db = context;
        }

        public IQueryable<WF_Sequence> GetPool(SeriesRequestDto seriesDto)
        {
            var trial = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.PACS_Trials.Single(t => t.TrialID == seriesDto.StudyId);
            });

            var result = db.WF_Sequences.Where(x => x.IsActive && x.PACSTimePoint.PACSSubject.IsActive
                && x.PACSTimePoint.PACSSubject.PACSSite.TrialID == seriesDto.StudyId);

            if (!string.IsNullOrWhiteSpace(seriesDto.Step))
            {
                result = result.Where(x => x.WFTempStep.WFStepList.WFStepListDes.ToLower() == seriesDto.Step.ToLower());
                switch (seriesDto.Step.ToLower())
                {
                    case "upload":
                    case "check-in":
                    case "grade":
                        result = result.Where(x => x.PACSTPProcList.CERTImgProcedureList.PACSDataType.DataType != "OE" &&
                            x.PACSTPProcList.CERTImgProcedureList.PACSDataType.DataType != "eCRF" &&
                            x.PACSTPProcList.CERTImgProcedureList.PACSDataType.DataType != "DISCRETE");
                        break;
                    case "verify":
                    case "completed":
                        break;
                    default:
                        return null;
                }
            }

            if (seriesDto.TimePointListId != null)
                result = result.Where(x => x.PACSTimePoint.TimePointsListID == seriesDto.TimePointListId);
            if (seriesDto.ProcedureId != null)
                result = result.Where(x => x.PACSTPProcList.ImgProcedureID == seriesDto.ProcedureId);
            if (seriesDto.SubjectId != null)
                result = result.Where(x => x.PACSTimePoint.SubjectID == seriesDto.SubjectId);

            var u = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.CONTACT_Users.FirstOrDefault(item => item.IsActive && item.AspUserID.ToString() == seriesDto.UserId);
            });

            if (seriesDto.Filter != null)
            {
                var filtersList = seriesDto.Filter.Split(',');
                foreach (var filters in filtersList)
                {
                    var filter = filters.Split(':');
                    switch (filter[0])
                    {
                        case "CategoryFlagID":
                            {
                                if (filter[1] == "null")
                                    result = result.Where(seq => seq.CategoryFlagID == null);
                                else
                                {
                                    result = result.Where(seq => seq.CategoryFlagID.HasValue);
                                    result = result.Where(seq => seq.CategoryFlagID.ToString() == filter[1]);
                                }
                            }
                            break;
                        case "DataType":
                            {
                                result = result.Where(seq => seq.PACSTPProcList.CERTImgProcedureList.PACSDataType.DataType == filter[1]);
                            }
                            break;
                        case "AssignedTo":
                            {
                                switch (filter[1])
                                {
                                    case "me":
                                        {
                                            result = result.Where(seq => seq.AssignedToID.HasValue && seq.AssignedToID.Value == u.UserID);
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(seriesDto.Search))
            {
                result = result.Where(x => ((x.SeriesGroupID == null) ? "" : x.SeriesGroupID.ToString()).Contains(seriesDto.Search)
                    || x.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID.Contains(seriesDto.Search)
                    || x.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.AffiliationName.Contains(seriesDto.Search)
                    || x.PACSTimePoint.PACSSubject.RandomizedSubjectID.Contains(seriesDto.Search)
                    || x.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID.Contains(seriesDto.Search)
                    || x.PACSTimePoint.PACSSubject.NameCode.Contains(seriesDto.Search)
                    || x.PACSTimePoint.PACSTimePointsList.TimePointsDescription.Contains(seriesDto.Search)
                    || x.PACSTPProcList.CERTImgProcedureList.ImgProcedureName.Contains(seriesDto.Search)
                    || ((x.AssignedTo == null) ? false : (x.AssignedTo.FirstName.Contains(seriesDto.Search) 
                        || x.AssignedTo.LastName.Contains(seriesDto.Search) || x.AssignedTo.LoweredUserName.Contains(seriesDto.Search)))
                    );
            }

            if (!trial.IsTestingPhase)
                result = result.Where(seq => !seq.PACSTimePoint.PACSSubject.IsTestingSubject && !seq.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);

            switch (u.AspnetRole.LoweredRoleName)
            {
                case "ophthalmic technician":
                case "site coordinator":
                case "data quality evaluator":
                case "grader":
                case "reviewer":
                case "super user":
                    var rcps = db.CONTACT_TrialReadingCenters.Where(item => item.TrialID == seriesDto.StudyId && item.AffiliationID == u.AffiliationID).Select(item => item.ImgProcedureID).Distinct().ToList();
                    if (rcps != null && rcps.Count() > 0)
                    {
                        result = result.Where(seq => rcps.Contains(seq.PACSTPProcList.CERTImgProcedureList.ImgProcedureID)).AsQueryable();
                        return result;
                    }
                    return result;
                default:
                    return result;
            }
        }

        public WF_Sequence GetSerieByID(long seriesID)
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.WF_Sequences.FirstOrDefault(x => x.SeriesID == seriesID);
            });
            return result;
        }

        public bool AssignSerie(CommonRequestDto commonDto, string userId)
        {
            var hasChanges = true;
            List<WF_Sequence> series = GetAffectedSeries(commonDto);

            var u = db.CONTACT_Users.FirstOrDefault(item => item.IsActive && item.AspUserID.ToString() == userId);
            var warning = false;
            foreach (var s in series)
            {
                if (s.AssignedToID != null)
                {
                    warning = true;
                    continue;
                }

                s.AssignedToID = u.UserID;
                hasChanges = true;

                //Audit
                var record = new AUDIT_Record()
                {
                    PerformedDateTime = DateTime.UtcNow,
                    SoftwareVersion = "1.0.0.0"
                };
                record.UserID = u.UserID;
                record.AuditActionID = db.AUDIT_Actions.SingleOrDefault(aa =>
                    aa.AuditActionName.ToLower() == "seriesassigned").AuditActionID;
                record.TrialID = commonDto.CommonId;
                record.SeriesID = s.SeriesID;
                record.WFTempStepID = s.WFTempStepID;
                db.Add(record);
                hasChanges = true;
            }

            if (hasChanges)
            {
                DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    db.SaveChanges();
                });
            }


            return !warning;
        }

        public bool UnassignSerie(CommonRequestDto commonDto, string userId)
        {
            var hasChanges = false;
            List<WF_Sequence> series = GetAffectedSeries(commonDto);

            var u = db.CONTACT_Users.FirstOrDefault(item => item.IsActive && item.AspUserID.ToString() == userId);
            foreach (var s in series)
            {
                hasChanges = true;
                s.AssignedToID = null;

                //Audit
                var record = new AUDIT_Record()
                {
                    PerformedDateTime = DateTime.UtcNow,
                    SoftwareVersion = "1.0.0.0"
                };
                record.UserID = u.UserID;
                record.AuditActionID = db.AUDIT_Actions.SingleOrDefault(aa =>
                    aa.AuditActionName.ToLower() == "seriesunassigned").AuditActionID;
                record.TrialID = commonDto.CommonId;
                record.SeriesID = s.SeriesID;
                record.WFTempStepID = s.WFTempStepID;
                db.Add(record);
                hasChanges = true;
            }

            if (hasChanges)
            {
                DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    db.SaveChanges();
                });
            }

            return hasChanges;
        }

        private List<WF_Sequence> GetAffectedSeries(CommonRequestDto commonDto)
        {
            var series = new List<WF_Sequence>();
            foreach (var sID in commonDto.CommonList)
            {
                var chgSeries = db.WF_Sequences.Where(x => commonDto.CommonList.Contains(x.SeriesID));
                foreach (var item in chgSeries)
                {
                    if (item.PACSSeriesGroup?.SeriesGroupID != null)
                    {
                        var seriesInGroup = db.WF_Sequences.Where(ws => ws.PACSSeriesGroup.SeriesGroupID == item.PACSSeriesGroup.SeriesGroupID);
                        foreach (var gs in seriesInGroup)
                        {
                            if (!series.Contains(gs))
                            {
                                series.Add(gs);
                            }
                        }
                    }
                    else
                    {
                        if (!series.Contains(item))
                        {
                            series.Add(item);
                        }
                    }
                }
            }

            return series;
        }

        public IQueryable<WF_CategoryFlag> GetWFCategoryFlags()
        {
            return db.WF_CategoryFlags;
        }

        public IQueryable<WF_Sequence> SetSeriesCategory(CommonRequestDto commonDto, string userId)
        {
            var hasChanges = false;

            var series = db.WF_Sequences.Where(x => commonDto.CommonList.Contains(x.SeriesID));

            foreach (var s in series)
            {
                //if multi-modality
                if (s != null)
                {
                    if (commonDto.CommonId <= 0)
                        s.CategoryFlagID = null;
                    else
                        s.CategoryFlagID = commonDto.CommonId;

                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    db.SaveChanges();
                });
            }

            return series;
        }

        public string GetSegmentationStatus(long seriesID)
        {
            //101616
            var status = string.Empty;
            var rawdata = db.PACS_RawData.Where(item => item.IsActive && item.SeriesID == seriesID && item.PACSDataType.DataType == "OPT");

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
    }
}
