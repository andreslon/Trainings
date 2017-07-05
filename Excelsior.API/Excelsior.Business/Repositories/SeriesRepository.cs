using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Request.v0;
using Excelsior.Business.DtoEntities.Responses;
using Excelsior.Business.Helpers;
using Excelsior.Business.Logic;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Repositories
{
    public class SeriesRepository
    {
        public DataModel db { get; set; }

        public SeriesRepository(DataModel context)
        {
            db = context;
        }

        public ResultInfo<IList<WFSequencesResponseDto>> GetPool(SeriesRequestDto serieDto)
        {
            var u = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return db.CONTACT_Users.FirstOrDefault(item => item.IsActive && item.AspUserID.ToString() == serieDto.UserId);
            });

            var result = new ResultInfo<IList<WFSequencesResponseDto>>();
            try
            {
                var seriesResult = new List<WFSequencesResponseDto>();
                var handler = new SeriesHandler(db);
                var series = handler.GetPool(serieDto);
                if (serieDto.Assigned != null)
                {
                    if (serieDto.Assigned.Value)
                    {
                        switch (u.AspnetRole.LoweredRoleName)
                        {
                            case "administrator":
                            case "project manager":
                            case "super user":
                                series = series.Where(seq => seq.AssignedToID != null).OrderBy(seq => seq.LastStepCompletionDate);
                                break;
                            default:
                                series = series.Where(seq => seq.AssignedToID == u.UserID).OrderBy(seq => seq.LastStepCompletionDate);
                                break;
                        }
                    }
                    else
                    {
                        series = series.Where(seq => seq.AssignedToID == null).OrderBy(seq => seq.LastStepCompletionDate);
                    }
                }

                var count = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return series.Count();
                });
                result.SetPager(count, serieDto.Page, serieDto.PageSize);
                var seriesPaged = GeneralHelper.GetPagedList(series.OrderBy(x=> x.LastStepCompletionDate), result.Pager);
                if (seriesPaged != null)
                {
                    foreach (var item in seriesPaged)
                    {
                        var dto = ConvertToDto(item);
                        dto.TotalUploads = GetTotalUploads(dto.SeriesID);
                        if (item != null && item.PACSTPProcList?.CERTImgProcedureList?.PACSDataType?.DataType == "OPT")
                            dto.SegmentationStatus = GetSegmentationStatus(item.SeriesID);
                        seriesResult.Add(dto);
                    }
                }

                result.Result = seriesResult;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public int GetTotalUploads(long? seriesId)
        {
            var handler = new SeriesHandler(db);
            var result = 0;
            if (seriesId.HasValue)
            {
                result = handler.db.UPLD_UploadInfos.Count(x => x.IsActive && x.SeriesID == seriesId);
            }
            return result;
        }

        public ResultInfo<WFSequencesResponseDto> GetSerieByID(CommonRequestDto request)
        {
            var result = new ResultInfo<WFSequencesResponseDto>();
            try
            {
                var handler = new SeriesHandler(db);
                var entity = handler.GetSerieByID(request.CommonId);
                var dto = ConvertToDto(entity);
                if(entity != null && entity.PACSTPProcList?.CERTImgProcedureList?.PACSDataType?.DataType == "OPT") 
                    dto.SegmentationStatus = GetSegmentationStatus(dto.SeriesID);

                result.Result = dto;                
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        private WFSequencesResponseDto ConvertToDto(WF_Sequence item)
        {
            if (item == null)
                return null;

            var dto = new WFSequencesResponseDto()
            {
                SeriesID = item.SeriesID,
                CategoryDes = item.WFCategoryFlag?.CategoryDes,
                SeriesGroupID = item.SeriesGroupID,
                LastStepCompletionDate = item.LastStepCompletionDate,
                SiteName = item.PACSTimePoint?.PACSSubject?.PACSSite?.DisplayName,
                RandomizedSubjectID = item.PACSTimePoint?.PACSSubject?.RandomizedSubjectID,
                AlternativeRandomizedSubjectID = item.PACSTimePoint?.PACSSubject?.AlternativeRandomizedSubjectID,
                NameCode = item.PACSTimePoint?.PACSSubject?.NameCode,
                IsTestingSubject = item.PACSTimePoint?.PACSSubject?.IsTestingSubject,
                Laterality = item.PACSTimePoint?.PACSSubject?.Laterality,
                StudyDate = item.StudyDate,
                TimePointsDescription = item.PACSTimePoint?.PACSTimePointsList?.TimePointsDescription,
                ImgProcedureName = item.PACSTPProcList?.CERTImgProcedureList?.ImgProcedureName,
                ContactUserName = (item.CONTACTUser?.LastName + (string.IsNullOrEmpty(item.CONTACTUser?.FirstName) ? "" : ", " + item.CONTACTUser?.FirstName)),
                ContactEquipmentName = (item.CONTACTEquipment?.CONTACTEquipmentModel == null) ? string.Empty : (string.Format("{0} - {1} ({2})", item.CONTACTEquipment?.CONTACTEquipmentModel?.ManufacturerModel, item.CONTACTEquipment?.CONTACTEquipmentModel?.EquipmentType, item.CONTACTEquipment?.MainSerialNum)),
                WFStep = item.WFTempStep?.WFStepList?.WFStepListDes,
                AssignedToName = (item.AssignedTo?.LastName + (string.IsNullOrEmpty(item.AssignedTo?.FirstName) ? "" : ", " + item.AssignedTo?.FirstName)),
                AssignedToID = item.AssignedToID,
                IsDataQualityAdequate = item.IsDataQualityAdequate,
                IsTechnicianCerified = GetCertifiedUserValue(item.PACSTimePoint?.PACSSubject?.PACSSite?.PACSTrial?.TrialID, item.CONTACTUser?.UserID, item.PACSTPProcList?.ImgProcedureID),
                IsEquipmentCerified = GetCertifiedEquipmentValue(item.PACSTimePoint?.PACSSubject?.PACSSite?.PACSTrial?.TrialID, item.CONTACTEquipment?.EquipmentID, item.PACSTPProcList?.ImgProcedureID),
            };

            if(item.PACSTPProcList != null)
            {
                dto.TPProcList = new TPProcListResponseDto()
                {
                    TPProcID = item.PACSTPProcList.ImgProcedureID,
                    TimePointsListID = item.PACSTPProcList.TimePointsListID,
                    GTemplateID = item.PACSTPProcList.GTemplateID,
                    WFTemplateID = item.PACSTPProcList.WFTemplateID,
                    IsGradeBothLaterality = item.PACSTPProcList.IsGradeBothLaterality,
                    CounterSeriesForReview = item.PACSTPProcList.CounterSeriesForReview,
                    CounterSeriesSigned = item.PACSTPProcList.CounterSeriesSigned,
                    PercentSeriesForReview = item.PACSTPProcList.PercentSeriesForReview,
                    ImgProcedureID = item.PACSTPProcList.ImgProcedureID,
                };

                if(item.PACSTPProcList.CERTImgProcedureList != null)
                {
                    dto.TPProcList.ImgProcedure = new ImgProcedureResponseDto()
                    {
                        ImgProcedureID = item.PACSTPProcList.CERTImgProcedureList.ImgProcedureID,
                        ImgProcedureName = item.PACSTPProcList.CERTImgProcedureList.ImgProcedureName,
                        DICOMAcquisitionDeviceCodeMeaning = item.PACSTPProcList.CERTImgProcedureList.DICOMAcquisitionDeviceCodeMeaning,
                        DICOMAcquisitionDeviceCodeValue = item.PACSTPProcList.CERTImgProcedureList.DICOMAcquisitionDeviceCodeValue,
                        DICOMAcquisitionDeviceCodeSchemeDesignator = item.PACSTPProcList.CERTImgProcedureList.DICOMAcquisitionDeviceCodeSchemeDesignator,
                        DICOMAnatomicStructureCodeMeaning = item.PACSTPProcList.CERTImgProcedureList.DICOMAnatomicStructureCodeMeaning,
                        DICOMAnatomicStructureCodeSchemeDesignator = item.PACSTPProcList.CERTImgProcedureList.DICOMAnatomicStructureCodeSchemeDesignator,
                        DICOMAnatomicStructureCodeValue = item.PACSTPProcList.CERTImgProcedureList.DICOMAnatomicStructureCodeValue,
                        DataTypeID = item.PACSTPProcList.CERTImgProcedureList.DataTypeID,
                    };

                    if (item.PACSTPProcList.CERTImgProcedureList.PACSDataType != null)
                    {
                        dto.TPProcList.ImgProcedure.DataType = new DataTypeResponseDto()
                        {
                            DataTypeID = item.PACSTPProcList.CERTImgProcedureList.PACSDataType.DataTypeID,
                            DataType = item.PACSTPProcList.CERTImgProcedureList.PACSDataType.DataType,
                        };
                    }
                }

                if (item.PACSTimePoint != null)
                {
                    dto.TimePoint = new TimePointsResponseDto()
                    {
                        TimePointsID = item.PACSTimePoint.TimePointsID,
                    };
                    if (item.PACSTimePoint.PACSTimePointsList != null)
                    {
                        dto.TimePoint.TimePointsList = new TimePointsListResponseDto()
                        {
                            TimePointsListID = item.PACSTimePoint.PACSTimePointsList.TimePointsListID,
                            TimePointsDescription = item.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                            TimePointsSeq = item.PACSTimePoint.PACSTimePointsList.TimePointsSeq,
                            TrialID = item.PACSTimePoint.PACSTimePointsList.TrialID,
                            IsInitialTimePoint = item.PACSTimePoint.PACSTimePointsList.IsInitialTimePoint,
                            IsTerminalTimePoint = item.PACSTimePoint.PACSTimePointsList.IsTerminalTimePoint,
                            IsEligibilityTimePoint = item.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint,
                            ExpectedVisitEndDay = item.PACSTimePoint.PACSTimePointsList.ExpectedVisitEndDay,
                            ExpectedVisitStartDay = item.PACSTimePoint.PACSTimePointsList.ExpectedVisitStartDay,
                        };
                    }
                    if (item.PACSTimePoint.PACSSubject != null)
                    {
                        dto.TimePoint.Subject = new SubjectsResponseDto()
                        {
                            SubjectID = item.PACSTimePoint.PACSSubject.SubjectID,
                            RandomizedSubjectID = item.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                            AlternativeRandomizedSubjectID = item.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID,
                            NameCode = item.PACSTimePoint.PACSSubject.NameCode,
                            IsTestingSubject = item.PACSTimePoint.PACSSubject.IsTestingSubject,
                            Laterality = item.PACSTimePoint.PACSSubject.Laterality,
                            BirthYear = item.PACSTimePoint.PACSSubject.BirthYear,
                            Gender = item.PACSTimePoint.PACSSubject.Gender,
                            IsActive = item.PACSTimePoint.PACSSubject.IsActive,
                            IsDismissed = item.PACSTimePoint.PACSSubject.IsDismissed,
                            IsSubjectRejected = item.PACSTimePoint.PACSSubject.IsSubjectRejected,
                            IsValidated = item.PACSTimePoint.PACSSubject.IsValidated,
                            SubjectEnrollmentDate = item.PACSTimePoint.PACSSubject.SubjectEnrollmentDate,
                            SubjectCohortID = item.PACSTimePoint.PACSSubject.SubjectCohortID,
                            SubjectGroupID = item.PACSTimePoint.PACSSubject.SubjectGroupID,
                            SiteID = item.PACSTimePoint.PACSSubject.SiteID,
                        };
                        if (item.PACSTimePoint.PACSSubject.PACSSite != null)
                        {
                            dto.TimePoint.Subject.Site = new SitesResponseDto()
                            {
                                SiteID = item.PACSTimePoint.PACSSubject.PACSSite.SiteID,
                                RandomizedSiteID = item.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID,
                                IsActive = item.PACSTimePoint.PACSSubject.PACSSite.IsActive,
                                IsIRB = item.PACSTimePoint.PACSSubject.PACSSite.IsIRB,
                                IsTestingSite = item.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite,
                                PrincipalInvestigator = item.PACSTimePoint.PACSSubject.PACSSite.PrincipalInvestigator,
                                TrialID = item.PACSTimePoint.PACSSubject.PACSSite.TrialID,
                                AffiliationID = item.PACSTimePoint.PACSSubject.PACSSite.AffiliationID,
                            };
                            if (item.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation != null)
                            {
                                dto.TimePoint.Subject.Site.Affiliation = new AffiliationsResponseDto()
                                {
                                    AffiliationID = item.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.AffiliationID,
                                    AffiliationName = item.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.AffiliationName,
                                    IsActive = item.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.IsActive,
                                    Country = item.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.CONTACTCountry?.CountryName,
                                };
                            }
                        }
                    }
                }
            }
            return dto;
        }

        private string GetSegmentationStatus(long? seriesID)
        {
            if (!seriesID.HasValue)
                return string.Empty;

            SeriesHandler handler = new SeriesHandler(db);
            var result = handler.GetSegmentationStatus(seriesID.Value);
            return result;
        }

        private bool GetCertifiedUserValue(long? trialID, long? userID, long? procedureID)
        {
            if (trialID.HasValue)
            {
                var count = GetTotalCertifiedUsersByProcedure(trialID.Value, userID, procedureID);
                return count > 0;
            }
            return false;
        }

        private bool GetCertifiedEquipmentValue(long? trialID, long? equipmentID, long? procedureID)
        {
            if (trialID.HasValue)
            {
                var count = new EquipmentRepository(db).GetTotalCertifiedEquipmentByProcedure(trialID.Value, equipmentID, procedureID);
                return count > 0;
            }
            return false;
        }

        public bool AssignSerie(CommonRequestDto commonDto, string userId)
        {
            SeriesHandler handler = new SeriesHandler(db);
            var result = handler.AssignSerie(commonDto, userId);

            return result;
        }

        public bool UnassignSerie(CommonRequestDto commonDto, string userId)
        {
            SeriesHandler handler = new SeriesHandler(db);
            var result = handler.UnassignSerie(commonDto, userId);

            return result;
        }

        public ResultInfo<IList<WFCategoryFlagResponseDto>> GetWFCategoryFlags()
        {
            var result = new ResultInfo<IList<WFCategoryFlagResponseDto>>();
            try
            {
                var handler = new SeriesHandler(db);
                var cat = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return handler.GetWFCategoryFlags().ToList();
                });
                List<WFCategoryFlagResponseDto> catResult = new List<WFCategoryFlagResponseDto>();
                if (cat != null)
                {
                    foreach (var item in cat)
                    {
                        var dto = new WFCategoryFlagResponseDto()
                        {
                            CategoryDes = item.CategoryDes,
                            CategoryFlagID = item.CategoryFlagID,
                        };
                        catResult.Add(dto);
                    }
                }

                result.Result = catResult;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;

        }

        public ResultInfo<IList<WFSequencesResponseDto>> SetSeriesCategory(CommonRequestDto commonDto, string userId)
        {
            var result = new ResultInfo<IList<WFSequencesResponseDto>>();

            try
            {
                var handler = new SeriesHandler(db);
                var wfSequences = DataHelpers.RetryPolicy.ExecuteAction(() =>
                {
                    return handler.SetSeriesCategory(commonDto, userId).ToList();
                });

                var seqRta = new List<WFSequencesResponseDto>();
                foreach (var item in wfSequences)
                {
                    WFSequencesResponseDto dto = ConvertToDto(item);
                    if (item != null && item.PACSTPProcList?.CERTImgProcedureList?.PACSDataType?.DataType == "OPT")
                        dto.SegmentationStatus = GetSegmentationStatus(item.SeriesID);
                    seqRta.Add(dto);
                }

                result.Result = seqRta;
                result.IsSuccess = true;

            }
            catch (Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public int GetTotalCertifiedUsersByProcedure(long? trialID, long? userID, long? procedureID)
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