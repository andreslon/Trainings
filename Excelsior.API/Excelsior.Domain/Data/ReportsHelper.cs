using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain
{
    #region Certificates

    public class DeviceCertificate
    {
        public string manufacturer { get; set; }
        public string deviceModel { get; set; }
        public string deviceSerial { get; set; }
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string trialName { get; set; }
        public string procedure { get; set; }
        public DateTime certDate { get; set; }
        public string certIssuer { get; set; }
        public string certIssuerAffiliationName { get; set; }
    }

    public class TechnicianCertificate
    {
        public string technicianName { get; set; }
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string trialName { get; set; }
        public string procedure { get; set; }
        public DateTime certDate { get; set; }
        public string certIssuer { get; set; }
        public string certIssuerAffiliationName { get; set; }
    }

    public class EquipmentCertificationReportData
    {
        public string trialName { get; set; }
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string procedure { get; set; }
        public CONTACT_Equipment equip { get; set; }
        public string equipmentType { get; set; }
        public string equipmentSerial { get; set; }
        public string certificationStatus { get; set; }
        public string activeQuery { get; set; }
        public string queryReplied { get; set; }
        public string lastMessageDate { get; set; }
    }

    public class SiteCertificationStatus
    {
        public string siteRandomizedID { get; set; }
        public string siteName { get; set; }
        public string procedure { get; set; }
        public string techorequipment { get; set; }
        public string statusReport { get; set; }
    }

    public class TechnicianCertificationReportData
    {
        public string trialName { get; set; }
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string procedure { get; set; }
        public CONTACT_User tech { get; set; }
        public string userEmail { get; set; }
        public string userLastName { get; set; }
        public string userFirstName { get; set; }
        public string userFormattedName
        {
            get
            {
                return string.IsNullOrEmpty(userLastName) ? userFirstName : (string.IsNullOrEmpty(userFirstName) ? userLastName : string.Format("{0}, {1}", userLastName, userFirstName));
            }
        }
        public string registrationStatus { get; set; }
        public string certificationStatus { get; set; }
        public string activeQuery { get; set; }
        public string queryReplied { get; set; }
        public string lastMessageDate { get; set; }
    }

    #endregion

    #region Billing

    #region SeriesCheckInCompleted
    public class SeriesCheckInCompleted
    {
        public string siteID { get; set; }
        public string subjectID { get; set; }
        public string subjectAltID { get; set; }
        public string subjectNameCode { get; set; }
        public string timePoint { get; set; }
        public string procedure { get; set; }
        public DateTime? studyDate { get; set; }
        public string eye { get; set; }
        public string dqe { get; set; }
        public string dqeOrg { get; set; }
        public DateTime dateCompleted { get; set; }
        public bool beforeStartDate { get; set; }
    }
    #endregion

    #region BillingInfo
    public class BillingInfo
    {
        public string billingType { get; set; }
        public int cumCount { get; set; }
        public int timeBandCount { get; set; }
    }
    #endregion

    #region CertUserDetail
    public class CertUserDetail
    {
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string userLastName { get; set; }
        public string userFirstName { get; set; }
        public string procedure { get; set; }
        public DateTime dateCertified { get; set; }
        public string certifiedBy { get; set; }
        public string certByOrg { get; set; }
        public string certificationStatus { get; set; }
    }
    public class CertEquipDetail
    {
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string EquipmentManufacturer { get; set; }
        public string EquipmentModel { get; set; }
        public string EquipmentSerialNum { get; set; }
        public string procedure { get; set; }
        public DateTime dateCertified { get; set; }
        public string certifiedBy { get; set; }
        public string certByOrg { get; set; }
        public string certificationStatus { get; set; }
    }

    #endregion

    #region CertificationCompleted
    public class CertUserEquipmentReportData
    {
        public string userEquip { get; set; }
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string userLastName { get; set; }
        public string userFirstName { get; set; }
        public string EquipmentManufacturer { get; set; }
        public string EquipmentModel { get; set; }
        public string EquipmentSerialNum { get; set; }
        public string procedure { get; set; }
        public DateTime dateCertified { get; set; }
        public string certifiedBy { get; set; }
        public string certiType { get; set; }
        public bool beforeStartDate { get; set; }
    }
    #endregion

    #region SeriesGradingCompleted
    public class SeriesGradingCompleted
    {
        public string siteID { get; set; }
        public string subjectID { get; set; }
        public string subjectAltID { get; set; }
        public string subjectNameCode { get; set; }
        public string timePoint { get; set; }
        public string procedure { get; set; }
        public DateTime? studyDate { get; set; }
        public string eye { get; set; }
        public string completedby { get; set; }
        public string gradeOrg { get; set; }
        public DateTime dateCompleted { get; set; }
        public bool beforeStartDate { get; set; }
    }
    #endregion

    #endregion

    #region DataBook

    #region DiscreteDataReport
    public class DiscreteDataSummary
    {
        public string laterality { get; set; }
        public string group { get; set; }
        public string animalID { get; set; }
        public string timePointDes { get; set; }
        public int? timePointSeq { get; set; }
        public double measurement { get; set; }
    }
    #endregion

    #region FALeakageReport
    public class CNVLeakageGradingList
    {
        public string group { get; set; }
        public string timePointDes { get; set; }
        public string gender { get; set; }
        public string cohort { get; set; }
        public string laterality { get; set; }
        public string animalID { get; set; }
        public string area { get; set; }
        public string result { get; set; }
    }
    #endregion

    #region OcularExamReport
    public class GroupInformationSummary
    {
        public string groupName { get; set; }
        public string treatmentLaterality { get; set; }
        public string groupDescription { get; set; }
        public string dosingInfo { get; set; }
        public string numberofSubjects { get; set; }
    }

    public class QASequence
    {
        public string questionGroup { get; set; }
        public int groupInSeq { get; set; }
        public string question { get; set; }
        public int questionInSeq { get; set; }
        public string answer { get; set; }
        public int answerInSeq { get; set; }
        public bool normal { get; set; }
    }

    public class OcularExamSummary
    {
        public string questionGroup { get; set; }
        public int qgInSeq { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public string qaString { get; set; }
        public int qaStringInSeq { get; set; }
        public string timePointDes { get; set; }
        public int timePointSeq { get; set; }
        public string group { get; set; }
        public string cohort { get; set; }
        public string gender { get; set; }
        public string laterality { get; set; }
        public string animalID { get; set; }
        public Color fontcolor { get; set; }
    }
    #endregion

    #region OEBarGraph
    public class OcularExamHistogram
    {
        public string question { get; set; }
        public string answer { get; set; }
        public string laterality { get; set; }
        public string timePointDes { get; set; }
        public int timePointSeq { get; set; }
    }
    #endregion

    #endregion

    #region Grading

    #region GradingReport
    public class GradingReportSeries
    {
        public string trialName { get; set; }
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string alternativeSubjectID { get; set; }
        public string randomizedSubjectID { get; set; }
        public string nameCode { get; set; }
        public string studyEye { get; set; }
        public string studyDate { get; set; }
        public string gradedBy { get; set; }
        public string reviewedBy { get; set; }
        public bool eligibilityTimepoint { get; set; }
        public string eligibility { get; set; }

    }
    public class GradingReportResults
    {
        public string laterality { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }

    #endregion

    #endregion

    #region GradingSummary

    #region EligibilityDetailReport
    public class EligDetailReport
    {
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string alternativeSubjectID { get; set; }
        public string randomizedSubjectID { get; set; }
        public string nameCode { get; set; }
        public string studyEye { get; set; }
        public string studyDate { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
    #endregion

    #region GrdConfidence
    public class GrdConfidenceScore
    {
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string confidenceScore { get; set; }
    }

    #endregion

    #region GrdSummary

    public class GrdSummarySubjectGroup
    {
        public long groupID { get; set; }
        public string groupName { get; set; }
    }

    public class GrdSummarySubjectCohort
    {
        public long cohortID { get; set; }
        public string cohortName { get; set; }
    }

    public class GrdSummarySubject
    {
        public long subjectID { get; set; }
        public string displayName { get; set; }
    }

    public class GrdCategoryAnswerCount
    {
        public string tPointList { get; set; }
        public string gAnswer { get; set; }
        public string resultID { get; set; }
    }
    public class GrdNumericalAnswer
    {
        public string tPointList { get; set; }
        public double avgValue { get; set; }
    }

    #endregion

    #region SubjEligibility

    public class subjEligibilityReport
    {
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string subjID { get; set; }
        public string subjNameCode { get; set; }
        public string laterality { get; set; }
        public string eligbility { get; set; }
        public string firstStudyDate { get; set; }
    }
    #endregion

    #endregion

    #region Imaging

    #region KeyFramesReport
    public class KeyFrame
    {
        public Int64 id { get; set; }
        public Int64 rdid { get; set; }
        public int? index { get; set; }
        public string timepoint { get; set; }
        public int? timepointSeq { get; set; }
        public string laterality { get; set; }
        public DateTime? studyDate { get; set; }
        public string imageUrl { get; set; }

        public KeyFrame(PACS_DicomFrame f, string msUrl)
        {
            this.id = f.DicomFrameID;
            this.rdid = f.PACSRawDatum.RawDataID;
            this.index = f.FrameIndex;
            this.timepoint = f.PACSRawDatum.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription;
            this.timepointSeq = f.PACSRawDatum.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsSeq;
            this.laterality = f.PACSRawDatum.Laterality;
            this.studyDate = f.PACSRawDatum.PACSSeries.StudyDate;

            UriBuilder uriBuilder = new UriBuilder(msUrl);
            uriBuilder.Path += string.Format(CultureInfo.InvariantCulture, "/{0}", f.FrameImageLocation);
            this.imageUrl = uriBuilder.Uri.AbsoluteUri;
        }
    }

    #endregion

    #endregion

    #region Performance

    #region QueryReport
    public class QueryReportItem
    {
        public string type { get; set; }
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string progress { get; set; }
        public long queryID { get; set; }
    }

    #endregion

    #region RCEligibilityPerformance
    public class RCEligibilityPerformanceMetrics
    {
        public string trialName { get; set; }
        public double? avgRCEligibilityTAT { get; set; }
        public double? targetRCEligibilityTAT { get; set; }
        public double? timelinessRCEligibility { get; set; }
        public double? avgDQEEligibilityTAT { get; set; }
        public double? targetDQEEligibilityTAT { get; set; }
        public double? timelinessDQEEligibility { get; set; }
        public double? avgGradingEligibilityTAT { get; set; }
        public double? targetGradingEligibilityTAT { get; set; }
        public double? timelinessGradingEligibility { get; set; }
        public double countEligibilityRC { get; set; }
        public double countEligibilityGrading { get; set; }
        public double countEligibilityDQE { get; set; }
        public double countEligibilityTimelinessRC { get; set; }
        public double countEligibilityTimelinessGrading { get; set; }
        public double countEligibilityTimelinessDQE { get; set; }
    }

    #endregion

    #region RCPerformance
    public class RCPerformanceMetrics
    {
        public string trialName { get; set; }
        public double? avgRCTAT { get; set; }
        public double? targetRCTAT { get; set; }
        public double? timelinessRC { get; set; }
        public double? avgDQETAT { get; set; }
        public double? targetDQETAT { get; set; }
        public double? timelinessDQE { get; set; }
        public double? avgGradingTAT { get; set; }
        public double? targetGradingTAT { get; set; }
        public double? timelinessGrading { get; set; }
        public double countRC { get; set; }
        public double countGrading { get; set; }
        public double countDQE { get; set; }
        public double countTimelinessRC { get; set; }
        public double countTimelinessGrading { get; set; }
        public double countTimelinessDQE { get; set; }
    }

    public class gradingBacklog
    {
        public string modality { get; set; }
        public string stage { get; set; }
        public string visit { get; set; }
        public long seriesID { get; set; }
    }

    #endregion

    #region SitePerformance
    public class SitePerformanceMetrics
    {
        public string trialName { get; set; }
        public double? siteUploadTAT { get; set; }
        public double? targetSiteUploadTAT { get; set; }
        public double? countUpload { get; set; }
        public double? countTimelinessUpload { get; set; }
        public double? timelinessUpload { get; set; }

        public double? queryTAT { get; set; }
        public double? targetQueryTAT { get; set; }
        public double? totalQueryResolved { get; set; }
        public double? countTimelinessquery { get; set; }
        public double? timelinessquery { get; set; }

        public double? totalQuery { get; set; }
        public double? totalSeries { get; set; }
        public double? queryRatio { get; set; }
        public double? targetQueryRatio { get; set; }

        public double? countSuboptimal { get; set; }
        public double? subOptimalRatio { get; set; }
        public double? targetSubOptimalRatio { get; set; }
    }

    #endregion

    #region TrialPerformanceReport
    public class TpfTrialRecruitmentMetrics
    {
        public int userCertified { get; set; }
        public int totalUsers { get; set; }
        public double userCertifiedRatio { get; set; }
        public int equipmentCertified { get; set; }
        public int totalEquipment { get; set; }
        public double equipmentCertifiedRatio { get; set; }
        public int subjectEnrolled { get; set; }
        public int? totalSubject { get; set; }
        public double subjectEnrollmentRatio { get; set; }
        public int dataSeriesCollected { get; set; }
        public int? totalDataSeries { get; set; }
        public double dataSeriesRatio { get; set; }
    }
    public class TpfRCPerformanceMetrics
    {
        public double? avgRCTAT { get; set; }
        public double? targetRCTAT { get; set; }
        public double? timelinessRC { get; set; }
        public double? avgDQETAT { get; set; }
        public double? targetDQETAT { get; set; }
        public double? timelinessDQE { get; set; }
        public double? avgGradingTAT { get; set; }
        public double? targetGradingTAT { get; set; }
        public double? timelinessGrading { get; set; }
        public double countRC { get; set; }
        public double countGrading { get; set; }
        public double countDQE { get; set; }
        public double countTimelinessRC { get; set; }
        public double countTimelinessGrading { get; set; }
        public double countTimelinessDQE { get; set; }

        public double? avgRCEligibilityTAT { get; set; }
        public double? targetRCEligibilityTAT { get; set; }
        public double? timelinessRCEligibility { get; set; }
        public double? avgDQEEligibilityTAT { get; set; }
        public double? targetDQEEligibilityTAT { get; set; }
        public double? timelinessDQEEligibility { get; set; }
        public double? avgGradingEligibilityTAT { get; set; }
        public double? targetGradingEligibilityTAT { get; set; }
        public double? timelinessGradingEligibility { get; set; }
        public double countEligibilityRC { get; set; }
        public double countEligibilityGrading { get; set; }
        public double countEligibilityDQE { get; set; }
        public double countEligibilityTimelinessRC { get; set; }
        public double countEligibilityTimelinessGrading { get; set; }
        public double countEligibilityTimelinessDQE { get; set; }
    }
    public class TpfSitePerformanceMetrics
    {
        public double? siteUploadTAT { get; set; }
        public double? targetSiteUploadTAT { get; set; }
        public double? countUpload { get; set; }
        public double? countTimelinessUpload { get; set; }
        public double? timelinessUpload { get; set; }

        public double? queryTAT { get; set; }
        public double? targetQueryTAT { get; set; }
        public double? totalQueryResolved { get; set; }
        public double? countTimelinessquery { get; set; }
        public double? timelinessquery { get; set; }

        public double? totalQuery { get; set; }
        public double? totalSeries { get; set; }
        public double? queryRatio { get; set; }
        public double? targetQueryRatio { get; set; }

        public double? countSuboptimal { get; set; }
        public double? subOptimalRatio { get; set; }
        public double? targetSubOptimalRatio { get; set; }
    }

    #endregion

    #region TrialRecruitmentProgress
    public class TrialRecruitmentMetrics
    {
        public string trialName { get; set; }
        public int userCertified { get; set; }
        public int totalUsers { get; set; }
        public double userCertifiedRatio { get; set; }
        public int equipmentCertified { get; set; }
        public int totalEquipment { get; set; }
        public double equipmentCertifiedRatio { get; set; }
        public int subjectEnrolled { get; set; }
        public int? totalSubject { get; set; }
        public double subjectEnrollmentRatio { get; set; }
        public int dataSeriesCollected { get; set; }
        public int? totalDataSeries { get; set; }
        public double dataSeriesRatio { get; set; }
    }

    #endregion

    #region Query

    #region QueryPrint
    public class QueryReportQuery
    {
        public string trialName { get; set; }
        public string queryType { get; set; }
        public string sender { get; set; }
        public string receipent { get; set; }
        public string receipentEmail { get; set; }
        public string subject { get; set; }
        public string dateInitiated { get; set; }
        public string dateResolved { get; set; }
    }

    public class QueryPrintQueryMessage
    {
        public string msgFrom { get; set; }
        public string msgDate { get; set; }
        public string msgBody { get; set; }
    }

    #endregion

    #endregion

    #region Statistics

    #region TrialUserList
    public class TrialUser
    {
        public string userOrgName { get; set; }
        public string useOrgID { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userRole { get; set; }
        public string userStatus { get; set; }
    }

    public class TrialEquipment
    {
        public string siteID { get; set; }
        public string siteName { get; set; }
        public string procedure { get; set; }
        public string equipmentType { get; set; }
        public string equipmentSerial { get; set; }
    }
    #endregion

    #region User
    public class UserStat
    {
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userRole { get; set; }
    }

    public class UserStatLogInOut
    {
        public DateTime logInTime { get; set; }
        public TimeSpan logInDuration { get; set; }
    }
    #endregion

    #endregion

    #endregion
}
