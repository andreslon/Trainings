#pragma warning disable 1591
using Excelsior.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace Excelsior.Domain
{
    public partial class DataModelBase : OpenAccessContext, IDataModelUnitOfWork
    {
        protected static string connectionString = (new Settings()).GetConnectionString("EyeKorConnection");

        protected static MetadataSource metadataSource = new DataModelFluentMetadataSource();

        public string Serialize(object obj)
        {
            string retval = null;
            if (obj != null)
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                StringBuilder sb = new StringBuilder();
                using (XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = false, NamespaceHandling = NamespaceHandling.OmitDuplicates }))
                {
                    new XmlSerializer(obj.GetType()).Serialize(writer, obj, ns);
                }
                retval = sb.ToString();
            }
            return retval;
        }

        public DataModelBase(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
            : base(connection, backendConfiguration, metadataSource)
        {
        }

        public IQueryable<PACS_Trial> PACS_Trials
        {
            get
            {
                return this.GetAll<PACS_Trial>();
            }
        }

        public IQueryable<PACS_TPProcList> PACS_TPProcLists
        {
            get
            {
                return this.GetAll<PACS_TPProcList>();
            }
        }

        public IQueryable<PACS_TimePointsList> PACS_TimePointsLists
        {
            get
            {
                return this.GetAll<PACS_TimePointsList>();
            }
        }

        public IQueryable<PACS_Site> PACS_Sites
        {
            get
            {
                return this.GetAll<PACS_Site>();
            }
        }

        public IQueryable<MEA_MeasurementType> MEA_MeasurementTypes
        {
            get
            {
                return this.GetAll<MEA_MeasurementType>();
            }
        }

        public IQueryable<CONTACT_User> CONTACT_Users
        {
            get
            {
                return this.GetAll<CONTACT_User>();
            }
        }

        public IQueryable<CONTACT_TrialSponsor> CONTACT_TrialSponsors
        {
            get
            {
                return this.GetAll<CONTACT_TrialSponsor>();
            }
        }

        public IQueryable<CONTACT_Equipment> CONTACT_Equipments
        {
            get
            {
                return this.GetAll<CONTACT_Equipment>();
            }
        }

        public IQueryable<CONTACT_EquipmentModel> CONTACT_EquipmentModels
        {
            get
            {
                return this.GetAll<CONTACT_EquipmentModel>();
            }
        }

        public IQueryable<CONTACT_Affiliation> CONTACT_Affiliations
        {
            get
            {
                return this.GetAll<CONTACT_Affiliation>();
            }
        }

        public IQueryable<CERT_ImgProcedureList> CERT_ImgProcedureLists
        {
            get
            {
                return this.GetAll<CERT_ImgProcedureList>();
            }
        }

        public IQueryable<Aspnet_User> Aspnet_Users
        {
            get
            {
                return this.GetAll<Aspnet_User>();
            }
        }

        public IQueryable<Aspnet_Membership> Aspnet_Memberships
        {
            get
            {
                return this.GetAll<Aspnet_Membership>();
            }
        }

        public IQueryable<CERT_Result> CERT_Results
        {
            get
            {
                return this.GetAll<CERT_Result>();
            }
        }

        public IQueryable<CERT_QuestionList> CERT_QuestionLists
        {
            get
            {
                return this.GetAll<CERT_QuestionList>();
            }
        }

        public IQueryable<Aspnet_Role> Aspnet_Roles
        {
            get
            {
                return this.GetAll<Aspnet_Role>();
            }
        }

        public IQueryable<Aspnet_Application> Aspnet_Applications
        {
            get
            {
                return this.GetAll<Aspnet_Application>();
            }
        }

        public IQueryable<WF_TempStep> WF_TempSteps
        {
            get
            {
                return this.GetAll<WF_TempStep>();
            }
        }

        public IQueryable<WF_Template> WF_Templates
        {
            get
            {
                return this.GetAll<WF_Template>();
            }
        }

        public IQueryable<WF_StepList> WF_StepLists
        {
            get
            {
                return this.GetAll<WF_StepList>();
            }
        }

        public IQueryable<PACS_Series> PACS_Series
        {
            get
            {
                return this.GetAll<PACS_Series>();
            }
        }

        public IQueryable<WF_Sequence> WF_Sequences
        {
            get
            {
                return this.GetAll<WF_Sequence>();
            }
        }

        public IQueryable<PACS_RawDatum> PACS_RawData
        {
            get
            {
                return this.GetAll<PACS_RawDatum>();
            }
        }

        public IQueryable<PACS_DicomEPDF> PACS_DicomEPDFs
        {
            get
            {
                return this.GetAll<PACS_DicomEPDF>();
            }
        }

        public IQueryable<PACS_DicomOP> PACS_DicomOPs
        {
            get
            {
                return this.GetAll<PACS_DicomOP>();
            }
        }

        public IQueryable<PACS_DicomOPT> PACS_DicomOPTs
        {
            get
            {
                return this.GetAll<PACS_DicomOPT>();
            }
        }

        public IQueryable<CONTACT_UserTrial> CONTACT_UserTrials
        {
            get
            {
                return this.GetAll<CONTACT_UserTrial>();
            }
        }

        public IQueryable<CERT_User> CERT_Users
        {
            get
            {
                return this.GetAll<CERT_User>();
            }
        }

        public IQueryable<CERT_Equipment> CERT_Equipments
        {
            get
            {
                return this.GetAll<CERT_Equipment>();
            }
        }

        public IQueryable<UPLD_UploadInfo> UPLD_UploadInfos
        {
            get
            {
                return this.GetAll<UPLD_UploadInfo>();
            }
        }

        public IQueryable<PACS_DataType> PACS_DataTypes
        {
            get
            {
                return this.GetAll<PACS_DataType>();
            }
        }

        public IQueryable<PACS_DicomFrame> PACS_DicomFrames
        {
            get
            {
                return this.GetAll<PACS_DicomFrame>();
            }
        }

        public IQueryable<GRD_ReportResult> GRD_ReportResults
        {
            get
            {
                return this.GetAll<GRD_ReportResult>();
            }
        }

        public IQueryable<GRD_QuestionTag> GRD_QuestionTags
        {
            get
            {
                return this.GetAll<GRD_QuestionTag>();
            }
        }

        public IQueryable<GRD_GradingQuestion> GRD_GradingQuestions
        {
            get
            {
                return this.GetAll<GRD_GradingQuestion>();
            }
        }

        public IQueryable<GRD_GradingAnswer> GRD_GradingAnswers
        {
            get
            {
                return this.GetAll<GRD_GradingAnswer>();
            }
        }

        public IQueryable<GRD_GradingTemplate> GRD_GradingTemplates
        {
            get
            {
                return this.GetAll<GRD_GradingTemplate>();
            }
        }

        public IQueryable<GRD_QuestionGroup> GRD_QuestionGroups
        {
            get
            {
                return this.GetAll<GRD_QuestionGroup>();
            }
        }

        public IQueryable<GRD_TempQuestion> GRD_TempQuestions
        {
            get
            {
                return this.GetAll<GRD_TempQuestion>();
            }
        }

        public IQueryable<GRD_Dependency> GRD_Dependencies
        {
            get
            {
                return this.GetAll<GRD_Dependency>();
            }
        }

        public IQueryable<QRY_Message> QRY_Messages
        {
            get
            {
                return this.GetAll<QRY_Message>();
            }
        }

        public IQueryable<MEA_MeasDataType> MEA_MeasDataTypes
        {
            get
            {
                return this.GetAll<MEA_MeasDataType>();
            }
        }

        public IQueryable<EXCELSIOR_SYSTEM> EXCELSIOR_SYSTEMs
        {
            get
            {
                return this.GetAll<EXCELSIOR_SYSTEM>();
            }
        }

        public IQueryable<DOCU_AuthorizationType> DOCU_AuthorizationTypes
        {
            get
            {
                return this.GetAll<DOCU_AuthorizationType>();
            }
        }

        public IQueryable<MEA_OCTLayer> MEA_OCTLayers
        {
            get
            {
                return this.GetAll<MEA_OCTLayer>();
            }
        }

        public IQueryable<MEA_OCTGrid> MEA_OCTGrids
        {
            get
            {
                return this.GetAll<MEA_OCTGrid>();
            }
        }

        public IQueryable<MEA_ETDRSGrid> MEA_ETDRSGrids
        {
            get
            {
                return this.GetAll<MEA_ETDRSGrid>();
            }
        }

        public IQueryable<MEA_Distance> MEA_Distances
        {
            get
            {
                return this.GetAll<MEA_Distance>();
            }
        }

        public IQueryable<MEA_Area> MEA_Areas
        {
            get
            {
                return this.GetAll<MEA_Area>();
            }
        }

        public IQueryable<MEA_Measurement> MEA_Measurements
        {
            get
            {
                return this.GetAll<MEA_Measurement>();
            }
        }

        public IQueryable<PACS_TrialKeyMetric> PACS_TrialKeyMetrics
        {
            get
            {
                return this.GetAll<PACS_TrialKeyMetric>();
            }
        }

        public IQueryable<CONTACT_Country> CONTACT_Countries
        {
            get
            {
                return this.GetAll<CONTACT_Country>();
            }
        }

        public IQueryable<PACS_ProcessedDatum> PACS_ProcessedData
        {
            get
            {
                return this.GetAll<PACS_ProcessedDatum>();
            }
        }

        public IQueryable<WF_CategoryFlag> WF_CategoryFlags
        {
            get
            {
                return this.GetAll<WF_CategoryFlag>();
            }
        }

        public IQueryable<CONTACT_TrialReadingCenter> CONTACT_TrialReadingCenters
        {
            get
            {
                return this.GetAll<CONTACT_TrialReadingCenter>();
            }
        }

        public IQueryable<AUDIT_Action> AUDIT_Actions
        {
            get
            {
                return this.GetAll<AUDIT_Action>();
            }
        }

        public IQueryable<AUDIT_CRUDAudit> AUDIT_CRUDAudits
        {
            get
            {
                return this.GetAll<AUDIT_CRUDAudit>();
            }
        }

        public IQueryable<QRY_Query> QRY_Queries
        {
            get
            {
                return this.GetAll<QRY_Query>();
            }
        }

        public IQueryable<QRY_Status> QRY_Status
        {
            get
            {
                return this.GetAll<QRY_Status>();
            }
        }

        public IQueryable<GRD_Report> GRD_Reports
        {
            get
            {
                return this.GetAll<GRD_Report>();
            }
        }

        public IQueryable<DOCU_DocumentVersion> DOCU_DocumentVersions
        {
            get
            {
                return this.GetAll<DOCU_DocumentVersion>();
            }
        }

        public IQueryable<DOCU_DocumentGroup> DOCU_DocumentGroups
        {
            get
            {
                return this.GetAll<DOCU_DocumentGroup>();
            }
        }

        public IQueryable<DOCU_Document> DOCU_Documents
        {
            get
            {
                return this.GetAll<DOCU_Document>();
            }
        }

        public IQueryable<DOCU_DocumentUser> DOCU_DocumentUsers
        {
            get
            {
                return this.GetAll<DOCU_DocumentUser>();
            }
        }

        public IQueryable<DOCU_DocumentRole> DOCU_DocumentRoles
        {
            get
            {
                return this.GetAll<DOCU_DocumentRole>();
            }
        }

        public IQueryable<PACS_Subject> PACS_Subjects
        {
            get
            {
                return this.GetAll<PACS_Subject>();
            }
        }

        public IQueryable<PACS_TimePoint> PACS_TimePoints
        {
            get
            {
                return this.GetAll<PACS_TimePoint>();
            }
        }

        public IQueryable<CERT_UploadInfo> CERT_UploadInfos
        {
            get
            {
                return this.GetAll<CERT_UploadInfo>();
            }
        }

        public IQueryable<AUDIT_Record> AUDIT_Records
        {
            get
            {
                return this.GetAll<AUDIT_Record>();
            }
        }

        public IQueryable<PACS_SeriesComment> PACS_SeriesComments
        {
            get
            {
                return this.GetAll<PACS_SeriesComment>();
            }
        }

        public IQueryable<PACS_SeriesAttachment> PACS_SeriesAttachments
        {
            get
            {
                return this.GetAll<PACS_SeriesAttachment>();
            }
        }

        public IQueryable<PACS_RawDataStatus> PACS_RawDataStatus
        {
            get
            {
                return this.GetAll<PACS_RawDataStatus>();
            }
        }

        public IQueryable<DOCU_DocumentVersionUser> DOCU_DocumentVersionUsers
        {
            get
            {
                return this.GetAll<DOCU_DocumentVersionUser>();
            }
        }

        public IQueryable<PACS_DicomWSI> PACS_DicomWSIs
        {
            get
            {
                return this.GetAll<PACS_DicomWSI>();
            }
        }

        public IQueryable<CONTACT_UserNotification> CONTACT_UserNotifications
        {
            get
            {
                return this.GetAll<CONTACT_UserNotification>();
            }
        }

        public IQueryable<CONTACT_Notification> CONTACT_Notifications
        {
            get
            {
                return this.GetAll<CONTACT_Notification>();
            }
        }

        public IQueryable<CONTACT_NotificationRole> CONTACT_NotificationRoles
        {
            get
            {
                return this.GetAll<CONTACT_NotificationRole>();
            }
        }

        public IQueryable<PACS_SubjectGroup> PACS_SubjectGroups
        {
            get
            {
                return this.GetAll<PACS_SubjectGroup>();
            }
        }

        public IQueryable<PACS_SubjectCohort> PACS_SubjectCohorts
        {
            get
            {
                return this.GetAll<PACS_SubjectCohort>();
            }
        }

        public IQueryable<CONTACT_UserAffiliation> CONTACT_UserAffiliations
        {
            get
            {
                return this.GetAll<CONTACT_UserAffiliation>();
            }
        }

        public IQueryable<MEA_Freehand> MEA_Freehands
        {
            get
            {
                return this.GetAll<MEA_Freehand>();
            }
        }

        public IQueryable<PACS_SeriesGroup> PACS_SeriesGroups
        {
            get
            {
                return this.GetAll<PACS_SeriesGroup>();
            }
        }

        public IQueryable<RPT_TrialReport> RPT_TrialReports
        {
            get
            {
                return this.GetAll<RPT_TrialReport>();
            }
        }

        public IQueryable<RPT_Report> RPT_Reports
        {
            get
            {
                return this.GetAll<RPT_Report>();
            }
        }

        public IQueryable<RPT_ReportCategory> RPT_ReportCategories
        {
            get
            {
                return this.GetAll<RPT_ReportCategory>();
            }
        }

        public IQueryable<RPT_TrialReportRole> RPT_TrialReportRoles
        {
            get
            {
                return this.GetAll<RPT_TrialReportRole>();
            }
        }

        public IQueryable<BGD_JobStatus> BGD_JobStatus
        {
            get
            {
                return this.GetAll<BGD_JobStatus>();
            }
        }

        public IQueryable<BGD_Job> BGD_Jobs
        {
            get
            {
                return this.GetAll<BGD_Job>();
            }
        }

        public IQueryable<MEA_DeltaVolume> MEA_DeltaVolumes
        {
            get
            {
                return this.GetAll<MEA_DeltaVolume>();
            }
        }

        public IQueryable<CFG_AnimalSpecy> CFG_AnimalSpecies
        {
            get
            {
                return this.GetAll<CFG_AnimalSpecy>();
            }
        }

        public IQueryable<GRD_Impression> GRD_Impressions
        {
            get
            {
                return this.GetAll<GRD_Impression>();
            }
        }

        public IQueryable<MEA_Stencil> MEA_Stencils
        {
            get
            {
                return this.GetAll<MEA_Stencil>();
            }
        }

        #region CRF

        public IQueryable<CRF_DataRELREC> CRF_DataRELRECs
        {
            get
            {
                return this.GetAll<CRF_DataRELREC>();
            }
        }

        public IQueryable<CRF_DataResult> CRF_DataResults
        {
            get
            {
                return this.GetAll<CRF_DataResult>();
            }
        }

        public IQueryable<CRF_Datum> CRF_Data
        {
            get
            {
                return this.GetAll<CRF_Datum>();
            }
        }

        public IQueryable<CRF_AnswerType> CRF_AnswerTypes
        {
            get
            {
                return this.GetAll<CRF_AnswerType>();
            }
        }

        public IQueryable<CRF_AnswerValidation> CRF_AnswerValidations
        {
            get
            {
                return this.GetAll<CRF_AnswerValidation>();
            }
        }

        public IQueryable<CRF_TemplateGroup> CRF_TemplateGroups
        {
            get
            {
                return this.GetAll<CRF_TemplateGroup>();
            }
        }

        public IQueryable<CRF_TemplateAnswer> CRF_TemplateAnswers
        {
            get
            {
                return this.GetAll<CRF_TemplateAnswer>();
            }
        }

        public IQueryable<CRF_TemplateDependency> CRF_TemplateDependencies
        {
            get
            {
                return this.GetAll<CRF_TemplateDependency>();
            }
        }

        public IQueryable<CRF_TemplateDependencySource> CRF_TemplateDependencySources
        {
            get
            {
                return this.GetAll<CRF_TemplateDependencySource>();
            }
        }

        public IQueryable<CRF_TemplateQuestion> CRF_TemplateQuestions
        {
            get
            {
                return this.GetAll<CRF_TemplateQuestion>();
            }
        }

        public IQueryable<CRF_TemplateQuestionTag> CRF_TemplateQuestionTags
        {
            get
            {
                return this.GetAll<CRF_TemplateQuestionTag>();
            }
        }

        public IQueryable<CRF_Template> CRF_Templates
        {
            get
            {
                return this.GetAll<CRF_Template>();
            }
        }

        #endregion

        #region AUTH

        public IQueryable<AUTH_Client> AUTH_Clients
        {
            get
            {
                return this.GetAll<AUTH_Client>();
            }
        }

        public IQueryable<AUTH_ClientScope> AUTH_ClientScopes
        {
            get
            {
                return this.GetAll<AUTH_ClientScope>();
            }
        }

        public IQueryable<AUTH_ClientSecret> AUTH_ClientSecrets
        {
            get
            {
                return this.GetAll<AUTH_ClientSecret>();
            }
        }

        public IQueryable<AUTH_Scope> AUTH_Scopes
        {
            get
            {
                return this.GetAll<AUTH_Scope>();
            }
        }

        public IQueryable<AUTH_ScopeClaim> AUTH_ScopeClaims
        {
            get
            {
                return this.GetAll<AUTH_ScopeClaim>();
            }
        }

        #endregion

        #region Reports

        #region Certificates

        #region EquipmentCertificate

        public DeviceCertificate GetDeviceCertificate(long CertID)
        {
            var dc = new DeviceCertificate();

            var certEquipment = this.CERT_Equipments.SingleOrDefault(cu => cu.CertEquipmentID == CertID);

            if (certEquipment != null)
            {
                dc.manufacturer = certEquipment.CONTACTEquipment.CONTACTEquipmentModel.ManufacturerName;
                dc.deviceModel = certEquipment.CONTACTEquipment.CONTACTEquipmentModel.ManufacturerModel;
                dc.deviceSerial = certEquipment.CONTACTEquipment.MainSerialNum;

                var site = this.PACS_Sites.SingleOrDefault(s => (s.TrialID == certEquipment.TrialID)
                    && (s.AffiliationID == certEquipment.CONTACTEquipment.CONTACTAffiliation.AffiliationID));
                if (site != null)
                {
                    dc.siteID = site.RandomizedSiteID;
                    dc.siteName = string.Format("Site {0}: {1}", site.RandomizedSiteID, site.CONTACTAffiliation.AffiliationName);
                }
                dc.trialName = this.PACS_Trials.Single(t => t.TrialID == certEquipment.TrialID).TrialName;
                dc.procedure = certEquipment.CERTImgProcedureList.ImgProcedureDescription;
                dc.certDate = (DateTime)certEquipment.DateofCertification;

                var cb = certEquipment.CertifiedBy;
                dc.certIssuer = (cb != null) ? cb.FirstName + " " + cb.LastName : "";
                dc.certIssuerAffiliationName = (cb != null) ? cb.CONTACTAffiliation.AffiliationName : "";
            }

            return dc;
        }

        #endregion

        #region TechnicianCertificate

        public TechnicianCertificate GetTechnicianCertificate(long CertID)
        {
            var tc = new TechnicianCertificate();

            var certUser = this.CERT_Users.SingleOrDefault(cu => cu.CertUserID == CertID);

            if (certUser != null)
            {
                tc.technicianName = (string)certUser.CONTACTUserTrial.CONTACTUser.FirstName + " " + certUser.CONTACTUserTrial.CONTACTUser.LastName;
                var site = this.PACS_Sites.SingleOrDefault(s => (s.TrialID == certUser.CONTACTUserTrial.TrialID)
                    && (s.AffiliationID == certUser.CONTACTUserTrial.CONTACTUser.CONTACTAffiliation.AffiliationID));
                if (site != null)
                {
                    tc.siteID = site.RandomizedSiteID;
                    tc.siteName = string.Format("Site {0}: {1}", site.RandomizedSiteID, site.CONTACTAffiliation.AffiliationName);
                }
                tc.trialName = this.PACS_Trials.Single(t => t.TrialID == certUser.CONTACTUserTrial.TrialID).TrialName;
                tc.procedure = certUser.CERTImgProcedureList.ImgProcedureDescription;
                tc.certDate = (DateTime)certUser.DateofCertification;

                var cb = certUser.CertifiedBy;
                tc.certIssuer = (cb != null) ? cb.FirstName + " " + cb.LastName : "";
                tc.certIssuerAffiliationName = (cb != null) ? cb.CONTACTAffiliation.AffiliationName : "";

            }

            return tc;
        }

        #endregion

        #region EquipmentCertificationStatus  
        public IQueryable<EquipmentCertificationReportData> GetEquipCertStatus(long trialID, Array siteFilter, string procFilter, string certFilter)
        {
            var certEquipment = this.CERT_Equipments.Where(ce => ce.TrialID == trialID && ce.IsActive);

            if (procFilter != "All")
            {
                certEquipment = certEquipment.Where(ce => ce.CERTImgProcedureList.ImgProcedureDescription == procFilter);
            }

            var siteIDSelected = new System.Collections.Generic.List<string>();
            foreach (var s in siteFilter)
                siteIDSelected.Add((string)s);

            var sites = this.PACS_Sites.Where(s => s.PACSTrial.TrialID == trialID && !s.IsTestingSite);

            if (!siteIDSelected.Any(s => s == "All"))
                sites = sites.Where(s => siteIDSelected.Contains(s.RandomizedSiteID));


            var joined = certEquipment.Join(sites, ce => ce.CONTACTEquipment.CONTACTAffiliation.AffiliationID, s => s.AffiliationID,
                              (ce, s) => new EquipmentCertificationReportData
                              {
                                  siteID = s.RandomizedSiteID,
                                  siteName = s.CONTACTAffiliation.AffiliationName,
                                  procedure = ce.CERTImgProcedureList.ImgProcedureDescription,
                                  equip = ce.CONTACTEquipment,
                                  equipmentType = ce.CONTACTEquipment.CONTACTEquipmentModel.EquipmentType,
                                  equipmentSerial = ce.CONTACTEquipment.MainSerialNum,
                                  certificationStatus = ce.IsCertified ?
                                    (ce.QRY_Queries.Any(q => q.IsActive) ? "Full" : "GF")
                                    : "No",
                                  activeQuery = ce.QRY_Queries.Any(q => q.IsActive && !q.IsResolved) ?
                                    "Yes" : "No",
                                  //queryReplied = ce.QRY_Queries.Any(q => q.IsActive) ?
                                  //  (ce.QRY_Queries.Last(q => q.IsActive).QRY_Messages.Any(q => q.IsActive) ?
                                  //      (ce.QRY_Queries.LastOrDefault(q => q.IsActive)?.QRYStatus?.StatusName == "Pending Resolution" ? "Yes" : "No")
                                  //      : "")
                                  //  : "",
                                  //lastMessageDate = ce.QRY_Queries.Any(q => q.IsActive) ?
                                  //  (ce.QRY_Queries.Last(q => q.IsActive).QRY_Messages.Any(q => q.IsActive) ? ce.QRY_Queries.Last(q => q.IsActive).QRY_Messages.Last(q => q.IsActive).DateCreated.Value.ToString("MM/dd/yyyy HH:mm") : "")
                                  //  : ""
                              });

            if (certFilter != "All")
            {
                joined = joined.Where(j => j.certificationStatus == certFilter);
            }


            return joined;
        }

        #endregion

        #region SiteCertificationStatus
        public System.Collections.Generic.List<PACS_Site> GetSites(long trialID)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            var result = this.PACS_Sites.Where(s => s.TrialID == trialID);

            if (!trial.IsTestingPhase)
                result = result.Where(s => !s.IsTestingSite);

            return result.OrderBy(s => s.RandomizedSiteID).ToList();
        }
        public System.Collections.Generic.List<SiteCertificationStatus> GetSiteCertificationStatus(long trialID, Array siteID, bool completedOnly, bool queryPending)
        {
            var status = new System.Collections.Generic.List<SiteCertificationStatus>();
            var sites = new System.Collections.Generic.List<PACS_Site>();

            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            //Get sites list
            if (siteID == null)
            {
                var sitesQ = this.PACS_Sites.Where(s => s.TrialID == trialID);
                if (!trial.IsTestingPhase)
                    sitesQ = sitesQ.Where(s => !s.IsTestingSite);

                sites = sitesQ.OrderBy(s => s.RandomizedSiteID).ToList();
            }
            else
            {
                foreach (var s in siteID)
                {
                    sites.Add(this.PACS_Sites.Single(m => m.SiteID == (long)s));
                }
            }

            //Get procedures
            var procIDs = this.PACS_TPProcLists.Where(tp => tp.PACSTimePointsList.PACSTrial.TrialID == trialID)
                .Select(tp => tp.ImgProcedureID).Distinct().ToList();


            //assign values
            foreach (var s in sites)
            {
                //need to deal with virtual site

                var status2 = new System.Collections.Generic.List<SiteCertificationStatus>();

                foreach (var p in procIDs)
                {
                    var users = this.CERT_Users.Where(u => u.CONTACTUserTrial.PACSTrial.TrialID == trialID
                            && (u.ImgProcedureID == p) && u.IsActive
                            && (u.CONTACTUserTrial.CONTACTUser.CONTACTAffiliation.AffiliationID == s.CONTACTAffiliation.AffiliationID
                              || this.CONTACT_UserAffiliations.Any(ua => ua.CONTACTUser.UserID == u.CONTACTUserTrial.CONTACTUser.UserID
                                  && ua.CONTACTAffiliation.AffiliationID == s.CONTACTAffiliation.AffiliationID))
                            );
                    var certifiedUsers = users.Where(u => u.IsCertified);

                    var equip = this.CERT_Equipments.Where(e => e.TrialID == trialID && e.ImgProcedureID == p
                        && e.IsActive
                        && e.CONTACTEquipment.CONTACTAffiliation.AffiliationID == s.CONTACTAffiliation.AffiliationID
                        );
                    var certifiedEquip = equip.Where(e => e.IsCertified);

                    status2.Add(new SiteCertificationStatus
                    {
                        siteRandomizedID = s.RandomizedSiteID,
                        siteName = s.CONTACTAffiliation.AffiliationName,
                        procedure = this.CERT_ImgProcedureLists.Single(l => l.ImgProcedureID == p).ImgProcedureName,
                        techorequipment = "Imaging Techinician",
                        statusReport = (users.Count() == 0) ? "NA" : ((certifiedUsers.Count() != 0) ? "Yes" : "No")
                    });

                    status2.Add(new SiteCertificationStatus
                    {
                        siteRandomizedID = s.RandomizedSiteID,
                        siteName = s.CONTACTAffiliation.AffiliationName,
                        procedure = this.CERT_ImgProcedureLists.Single(l => l.ImgProcedureID == p).ImgProcedureName,
                        techorequipment = "Equipment",
                        statusReport = (equip.Count() == 0) ? "NA" : ((certifiedEquip.Count() != 0) ? "Yes" : "No")
                    });
                }

                var allStatus = status2.All(sr => sr.statusReport == "NA") ? "NA" : (status2.Any(sr => sr.statusReport == "No") ? "No" : "Yes");

                var query = this.QRY_Queries.Where(q => q.IsActive && q.TrialID == trialID && !q.IsResolved
                    && ((q.CertEquipmentID != null && q.CERTEquipment.CONTACTEquipment.CONTACTAffiliation.AffiliationID == s.CONTACTAffiliation.AffiliationID)
                    || (q.CertUserID != null && (q.CERTUser.CONTACTUserTrial.CONTACTUser.CONTACTAffiliation.AffiliationID == s.CONTACTAffiliation.AffiliationID
                              || this.CONTACT_UserAffiliations.Any(ua => ua.CONTACTUser.UserID == q.CERTUser.CONTACTUserTrial.CONTACTUser.UserID
                                  && ua.CONTACTAffiliation.AffiliationID == s.CONTACTAffiliation.AffiliationID))
                    )));

                var queryCount = query.Count();
                var qActive = (queryCount != 0);

                if ((!queryPending || qActive) && (!completedOnly || (allStatus == "Yes")))
                {
                    status2.Add(new SiteCertificationStatus
                    {
                        siteRandomizedID = s.RandomizedSiteID,
                        siteName = s.CONTACTAffiliation.AffiliationName,
                        procedure = "",
                        techorequipment = "Query Pending?",
                        statusReport = (queryCount == 0) ? "-" : "Active"
                    });

                    status2.Add(new SiteCertificationStatus
                    {
                        siteRandomizedID = s.RandomizedSiteID,
                        siteName = s.CONTACTAffiliation.AffiliationName,
                        procedure = null,
                        techorequipment = "All",
                        statusReport = allStatus
                    });

                    status.AddRange(status2);
                }
            }

            return status;
        }

        #endregion

        #endregion

        #region Billing

        #region SeriesCheckinCompleted

        public IQueryable<SeriesCheckInCompleted> GetSeriesCheckInSummary(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            var auditRec = this.AUDIT_Records.Where(ar => ar.TrialID == trialID
                && (ar.WFTempStep.WFStepList.WFStepListDes == "Check-in")
                && (ar.AUDITAction.AuditActionName == "WorkflowStepCompleted")
                && ar.PACSSeries.IsActive && (ar.PerformedDateTime.Value.Date <= dtEnd.Date));

            if (!trial.IsTestingPhase)
                auditRec = auditRec.Where(ar => !ar.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject && !ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);

            var trialStateDate = trial.TrialStartDate;
            if (trialStateDate != null)
                auditRec = auditRec.Where(ar => (ar.PerformedDateTime.Value.Date >= trialStateDate));

            //var res = auditRec.ToList();
            var res = auditRec;
            var res2 = res.GroupBy(ar => ar.SeriesID, ar => ar, (key, elements) => elements.OrderBy(ar => ar.PerformedDateTime).Last());

            var result = res2.Select(ar => new SeriesCheckInCompleted
            {
                siteID = ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID,
                subjectID = ar.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                subjectAltID = ar.PACSSeries.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID,
                subjectNameCode = ar.PACSSeries.PACSTimePoint.PACSSubject.NameCode,
                timePoint = ar.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                procedure = ar.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureDescription,
                studyDate = ar.PACSSeries.StudyDate,
                eye = (ar.PACSSeries.PACSTPProcList.IsGradeBothLaterality) ? "OU" : "SE",
                dqe = ar.CONTACTUser.LoweredUserName,
                dqeOrg = ar.CONTACTUser.CONTACTAffiliation.AffiliationName,
                dateCompleted = (DateTime)ar.PerformedDateTime,
                beforeStartDate = (ar.PerformedDateTime.Value.Date < dtStart.Date)
            });

            return result;
        }
        public IQueryable<SeriesCheckInCompleted> GetSeriesCheckInCompleted(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var res = GetSeriesCheckInSummary(trialID, dtStart, dtEnd);
            return res.Where(r => r.dateCompleted >= dtStart.Date);
        }
        public IQueryable<SeriesCheckInCompleted> GetSeriesCreatedSummary(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            var auditRec = this.AUDIT_Records.Where(ar => ar.TrialID == trialID
                && (ar.AUDITAction.AuditActionName == "WorkflowStepCompleted")
                && ar.PACSSeries.IsActive && (ar.PerformedDateTime.Value.Date <= dtEnd.Date));

            if (!trial.IsTestingPhase)
                auditRec = auditRec.Where(ar => !ar.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject && !ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);

            var trialStartDate = trial.TrialStartDate;
            if (trialStartDate != null)
                auditRec = auditRec.Where(ar => (ar.PerformedDateTime.Value.Date >= trialStartDate));

            //var res = auditRec.ToList();
            var res = auditRec;
            var res2 = res.GroupBy(ar => ar.SeriesID, ar => ar, (key, elements) => elements.OrderBy(ar => ar.PerformedDateTime).First());

            //            var res2 = res.Where(ar => (ar.PerformedDateTime.Value.Date <= dtEnd.Date) && (ar.PerformedDateTime.Value.Date >= dtStart.Date)).ToList();

            var result = res2.Select(ar => new SeriesCheckInCompleted
            {
                siteID = ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID,
                subjectID = ar.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                subjectAltID = ar.PACSSeries.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID,
                subjectNameCode = ar.PACSSeries.PACSTimePoint.PACSSubject.NameCode,
                timePoint = ar.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                procedure = ar.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureDescription,
                studyDate = ar.PACSSeries.StudyDate,
                eye = (ar.PACSSeries.PACSTPProcList.IsGradeBothLaterality) ? "OU" : "SE",
                beforeStartDate = (ar.PerformedDateTime.Value.Date < dtStart.Date),
                dateCompleted = (DateTime)ar.PerformedDateTime
            });

            return result;
        }
        public IQueryable<SeriesCheckInCompleted> GetSeriesCreated(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var res = GetSeriesCreatedSummary(trialID, dtStart, dtEnd);
            return res.Where(r => r.dateCompleted >= dtStart.Date);
        }

        #endregion

        #region BillingInfo
        public IQueryable<BillingInfo> GetBillingCheckInInfo(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var auditRec = this.AUDIT_Records.Where(ar => ar.TrialID == trialID
                && (ar.WFTempStep.WFStepList.WFStepListDes == "Check-in")
                && (ar.AUDITAction.AuditActionName == "WorkflowStepCompleted")
                && ar.PACSSeries.IsActive
                && (!ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite));

            return null;
        }
        #endregion

        #region CertCompletedDetail
        public List<string> GetCertTypeCompleted()
        {
            return new System.Collections.Generic.List<string> { "All", "Full", "GF" };
        }
        public IQueryable<CertUserDetail> GetCertUserDetail(long trialID, string procFilter, string certFilter, DateTime? dtStart, DateTime dtEnd)
        {
            var certUser = this.CERT_Users.Where(cu => (cu.CONTACTUserTrial.TrialID == trialID)
                && cu.IsCertified
                && (cu.DateofCertification.Value.Date <= dtEnd.Date));

            if (dtStart != null)
                certUser = certUser.Where(cu => cu.DateofCertification.Value.Date >= dtStart.Value.Date);

            if (procFilter != "All")
                certUser = certUser.Where(cu => cu.CERTImgProcedureList.ImgProcedureDescription == procFilter);

            var sites = this.PACS_Sites.Where(s => s.PACSTrial.TrialID == trialID && !s.IsTestingSite);

            var joined = certUser.Join(sites, cu => cu.CONTACTUserTrial.CONTACTUser.CONTACTAffiliation.AffiliationID, s => s.AffiliationID,
                              (cu, s) => new CertUserDetail
                              {
                                  siteID = s.RandomizedSiteID,
                                  siteName = s.CONTACTAffiliation.AffiliationName,
                                  userLastName = cu.CONTACTUserTrial.CONTACTUser.LastName,
                                  userFirstName = cu.CONTACTUserTrial.CONTACTUser.FirstName,
                                  procedure = cu.CERTImgProcedureList.ImgProcedureDescription,
                                  dateCertified = (DateTime)cu.DateofCertification,
                                  certifiedBy = (cu.CertifiedBy != null) ? cu.CertifiedBy.LoweredUserName : "",
                                  certByOrg = (cu.CertifiedBy != null) ? cu.CertifiedBy.CONTACTAffiliation.AffiliationName : "",
                                  certificationStatus = (cu.QRY_Queries.Any(q => q.IsActive) ? "Full" : "GF")
                              });

            if (certFilter != "All")
                return joined.Where(j => j.certificationStatus == certFilter);

            return joined;
        }
        public IQueryable<CertEquipDetail> GetCertEquipDetail(long trialID, string procFilter, string certFilter, DateTime? dtStart, DateTime dtEnd)
        {
            var certEquipment = this.CERT_Equipments.Where(ce => (ce.TrialID == trialID)
                && (ce.IsActive)
                && ce.IsCertified
                && (ce.DateofCertification.Value.Date <= dtEnd.Date));

            if (dtStart != null)
                certEquipment = certEquipment.Where(ce => ce.DateofCertification.Value.Date >= dtStart.Value.Date);

            if (procFilter != "All")
                certEquipment = certEquipment.Where(j => j.CERTImgProcedureList.ImgProcedureDescription == procFilter);

            var sites = this.PACS_Sites.Where(s => s.PACSTrial.TrialID == trialID && !s.IsTestingSite);
            var joined = certEquipment.Join(sites, ce => ce.CONTACTEquipment.CONTACTAffiliation.AffiliationID, s => s.AffiliationID,
                  (ce, s) => new CertEquipDetail
                  {
                      siteID = s.RandomizedSiteID,
                      siteName = s.CONTACTAffiliation.AffiliationName,
                      EquipmentManufacturer = ce.CONTACTEquipment.CONTACTEquipmentModel.ManufacturerName,
                      EquipmentModel = ce.CONTACTEquipment.CONTACTEquipmentModel.ManufacturerModel,
                      EquipmentSerialNum = ce.CONTACTEquipment.MainSerialNum,
                      procedure = ce.CERTImgProcedureList.ImgProcedureDescription,
                      dateCertified = (DateTime)ce.DateofCertification,
                      certifiedBy = (ce.CertifiedBy != null) ? ce.CertifiedBy.LoweredUserName : "",
                      certByOrg = (ce.CertifiedBy != null) ? ce.CertifiedBy.CONTACTAffiliation.AffiliationName : "",
                      certificationStatus = (ce.QRY_Queries.Any(q => q.IsActive) ? "Full" : "GF")
                  });

            if (certFilter != "All")
                return joined.Where(j => j.certificationStatus == certFilter);

            return joined;
        }
        #endregion

        #region CertificationCompleted
        public PACS_Trial GetTrial(long trialID)
        {
            return this.PACS_Trials.Single(t => t.TrialID == trialID);
        }
        public IQueryable<CertUserEquipmentReportData> GetCertUser(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var certUser = this.CERT_Users.Where(cu => (cu.CONTACTUserTrial.TrialID == trialID)
                && (cu.IsActive)
                && (cu.DateofCertification.Value.Date <= dtEnd.Date));

            var sites = this.PACS_Sites.Where(s => s.PACSTrial.TrialID == trialID && !s.IsTestingSite);

            var joined = certUser.Join(sites, cu => cu.CONTACTUserTrial.CONTACTUser.CONTACTAffiliation.AffiliationID, s => s.AffiliationID,
                              (cu, s) => new CertUserEquipmentReportData
                              {
                                  userEquip = "User",
                                  siteID = s.RandomizedSiteID,
                                  siteName = s.CONTACTAffiliation.AffiliationName,
                                  userLastName = cu.CONTACTUserTrial.CONTACTUser.LastName,
                                  userFirstName = cu.CONTACTUserTrial.CONTACTUser.FirstName,
                                  procedure = cu.CERTImgProcedureList.ImgProcedureDescription,
                                  dateCertified = (DateTime)cu.DateofCertification,
                                  certifiedBy = (cu.CertifiedBy != null) ? cu.CertifiedBy.LoweredUserName : "",
                                  certiType = (cu.QRY_Queries.Any()) ? "Full" : "GF",
                                  beforeStartDate = (cu.DateofCertification.Value.Date < dtStart.Date)
                              });

            return joined;
        }
        public IQueryable<CertUserEquipmentReportData> GetCertUserFull(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var cUser = GetCertUser(trialID, dtStart, dtEnd);
            return cUser.Where(cu => cu.certiType == "Full" && !cu.beforeStartDate);
        }
        public IQueryable<CertUserEquipmentReportData> GetCertUserGrandfathered(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var cUser = GetCertUser(trialID, dtStart, dtEnd);
            return cUser.Where(cu => cu.certiType == "GF" && !cu.beforeStartDate);
        }
        public IQueryable<CertUserEquipmentReportData> GetCertEquipment(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var certEquipment = this.CERT_Equipments.Where(ce => (ce.TrialID == trialID)
                && (ce.IsActive)
                && (ce.DateofCertification.Value.Date <= dtEnd.Date));

            var sites = this.PACS_Sites.Where(s => s.PACSTrial.TrialID == trialID && !s.IsTestingSite);
            var joined = certEquipment.Join(sites, ce => ce.CONTACTEquipment.CONTACTAffiliation.AffiliationID, s => s.AffiliationID,
                  (ce, s) => new CertUserEquipmentReportData
                  {
                      userEquip = "Equipment",
                      siteID = s.RandomizedSiteID,
                      siteName = s.CONTACTAffiliation.AffiliationName,
                      EquipmentManufacturer = ce.CONTACTEquipment.CONTACTEquipmentModel.ManufacturerName,
                      EquipmentModel = ce.CONTACTEquipment.CONTACTEquipmentModel.ManufacturerModel,
                      EquipmentSerialNum = ce.CONTACTEquipment.MainSerialNum,
                      procedure = ce.CERTImgProcedureList.ImgProcedureDescription,
                      dateCertified = (DateTime)ce.DateofCertification,
                      certifiedBy = (ce.CertifiedBy != null) ? ce.CertifiedBy.LoweredUserName : "",
                      certiType = (this.QRY_Queries.Any(q => q.IsActive && q.CertEquipmentID == ce.CertEquipmentID)) ? "Full" : "GF",
                      beforeStartDate = (ce.DateofCertification.Value.Date < dtStart.Date)
                  });
            return joined;
        }
        public IQueryable<CertUserEquipmentReportData> GetCertEquipmentFull(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var cEquip = GetCertEquipment(trialID, dtStart, dtEnd);
            return cEquip.Where(ce => ce.certiType == "Full" && !ce.beforeStartDate);
        }
        public IQueryable<CertUserEquipmentReportData> GetCertEquipmentGrandfathered(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var cEquip = GetCertEquipment(trialID, dtStart, dtEnd);
            return cEquip.Where(ce => ce.certiType == "GF" && !ce.beforeStartDate);
        }
        public IQueryable<CertUserEquipmentReportData> GetCertUserEquipCount(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var cEquip = GetCertEquipment(trialID, dtStart, dtEnd);
            var cUser = GetCertUser(trialID, dtStart, dtEnd);

            var c = cUser.ToList();
            c.AddRange(cEquip);
            return c.AsQueryable();
        }
        #endregion

        #region SeriesGradingCompleted
        public IQueryable<SeriesGradingCompleted> GetSeriesGradingSummary(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            var auditRec = this.AUDIT_Records.Where(ar => ar.TrialID == trialID
                && (ar.WFTempStep.WFStepList.WFStepListDes == "Grade")
                && (ar.AUDITAction.AuditActionName == "WorkflowStepCompleted")
                && ar.PACSSeries.IsActive);

            if (!trial.IsTestingPhase)
                auditRec = auditRec.Where(ar => !ar.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject && !ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);

            //            var resDateBand = res.Where(ar => (ar.PerformedDateTime.Value.Date <= dtEnd.Date) && (ar.PerformedDateTime.Value.Date >= dtStart.Date)).ToList();
            var resDateBand = auditRec.Where(ar => (ar.PerformedDateTime.Value.Date <= dtEnd.Date));
            var trialStartDate = trial.TrialStartDate;
            if (trialStartDate != null)
                resDateBand = resDateBand.Where(ar => (ar.PerformedDateTime.Value.Date >= trialStartDate));


            var res = resDateBand.GroupBy(ar => ar.SeriesID, ar => ar, (key, elements) => elements.OrderBy(ar => ar.PerformedDateTime).First());

            //TODO - Check with Yijun, this is how the previous code had it
            //var result = resDateBand.Select(ar => new SeriesGradingCompleted
            var result = res.Select(ar => new SeriesGradingCompleted
            {
                siteID = ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID,
                subjectID = ar.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                subjectAltID = ar.PACSSeries.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID,
                subjectNameCode = ar.PACSSeries.PACSTimePoint.PACSSubject.NameCode,
                timePoint = ar.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                procedure = ar.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureDescription,
                studyDate = ar.PACSSeries.StudyDate,
                eye = (ar.PACSSeries.PACSTPProcList.IsGradeBothLaterality) ? "OU" : "SE",
                completedby = ar.CONTACTUser.LoweredUserName,
                gradeOrg = ar.CONTACTUser.CONTACTAffiliation.AffiliationName,
                beforeStartDate = (ar.PerformedDateTime.Value.Date < dtStart.Date),
                dateCompleted = (DateTime)ar.PerformedDateTime
            });

            return result;
        }
        public IQueryable<SeriesGradingCompleted> GetSeriesGradingCompleted(long trialID, DateTime dtStart, DateTime dtEnd)
        {
            var res = GetSeriesGradingSummary(trialID, dtStart, dtEnd);
            return res.Where(r => r.dateCompleted >= dtStart.Date);
        }
        #endregion

        #region TechnicianCertificationStatus

        public System.Collections.Generic.List<string> GetTechCertType()
        {
            return new System.Collections.Generic.List<string> { "All", "Full", "GF", "No" };
        }

        public System.Collections.Generic.List<string> GetTechCertProcedureList(long trialID)
        {
            var trialProcedures = new System.Collections.Generic.List<string>();
            trialProcedures.Add("All");

            var procedure = this.PACS_TPProcLists.Where(t => t.PACSTimePointsList.PACSTrial.TrialID == trialID);
            var plist = procedure.Select(p => p.CERTImgProcedureList).ToList();
            var pl = plist.GroupBy(p => p.ImgProcedureID).Select(g => g.First());

            foreach (var p in pl)
            {
                trialProcedures.Add(p.ImgProcedureDescription);
            }

            return trialProcedures;
        }

        public System.Collections.Generic.List<string> GetTechCertSiteIDList(long trialID)
        {
            var siteID = new System.Collections.Generic.List<string>();
            siteID.Add("All");

            var sites = this.PACS_Sites.Where(s => s.PACSTrial.TrialID == trialID && !s.IsTestingSite)
                .OrderBy(s => s.RandomizedSiteID);
            foreach (var s in sites)
                siteID.Add(s.RandomizedSiteID);

            return siteID;
        }

        public System.Collections.Generic.List<TechnicianCertificationReportData> GetTechCertStatus(long trialID, Array siteFilter, string procFilter, string certFilter)
        {
            var certUser = this.CERT_Users.Where(cu => cu.CONTACTUserTrial.TrialID == trialID && cu.IsActive);

            if (procFilter != "All")
                certUser = certUser.Where(cu => cu.CERTImgProcedureList.ImgProcedureDescription == procFilter);

            var siteIDSelected = new System.Collections.Generic.List<string>();
            foreach (var s in siteFilter)
                siteIDSelected.Add((string)s);

            var sites = this.PACS_Sites.Where(s => s.PACSTrial.TrialID == trialID && !s.IsTestingSite);

            if (!siteIDSelected.Any(s => s == "All"))
                sites = sites.Where(s => siteIDSelected.Contains(s.RandomizedSiteID));

            var joined = certUser.Join(sites, cu => cu.CONTACTUserTrial.CONTACTUser.CONTACTAffiliation.AffiliationID, s => s.AffiliationID,
                              (cu, s) => new TechnicianCertificationReportData
                              {
                                  siteID = s.RandomizedSiteID,
                                  siteName = s.CONTACTAffiliation.AffiliationName,
                                  procedure = cu.CERTImgProcedureList.ImgProcedureDescription,
                                  tech = cu.CONTACTUserTrial.CONTACTUser,
                                  userEmail = cu.CONTACTUserTrial.CONTACTUser.Email,
                                  userLastName = cu.CONTACTUserTrial.CONTACTUser.LastName,
                                  userFirstName = cu.CONTACTUserTrial.CONTACTUser.FirstName,
                                  registrationStatus = cu.CONTACTUserTrial.CONTACTUser.AspUserID == null ? "No" : "Yes",
                                  certificationStatus = cu.IsCertified ?
                                    (cu.QRY_Queries.Any(q => q.IsActive) ? "Full" : "GF")
                                    : "No",
                                  activeQuery = cu.QRY_Queries.Any(q => q.IsActive && !q.IsResolved) ?
                                    "Yes" : "No",
                                  //queryReplied = cu.QRY_Queries.Any() ?
                                  //  (cu.QRY_Queries.Last().QRY_Messages.Any() ?
                                  //      (cu.QRY_Queries.Last().QRY_Messages.Last().HasResponded ? "Yes" : "No")
                                  //      : "")
                                  //  : "",
                                  //lastMessageDate = cu.QRY_Queries.Any() ?
                                  //  (cu.QRY_Queries.Last().QRY_Messages.Any() ? cu.QRY_Queries.Last().QRY_Messages.Last().DateCreated.Value.ToString("MM/dd/yyyy HH:mm") : "")
                                  //  : ""
                              }).ToList();

            if (certFilter != "All")
                return joined.Where(j => j.certificationStatus == certFilter).ToList();

            return joined;
        }

        #endregion

        #endregion

        #region DataBook

        #region DiscreteDataGraph
        public System.Collections.Generic.List<DiscreteDataSummary> GetDiscreteDataByAnimalID(long trialID, long imgProcedureID, string laterality, string animalID)
        {
            var allData = GetDiscreteDataSummary(trialID, imgProcedureID);
            return allData.Where(d => d.animalID == animalID && d.laterality == laterality).ToList();
        }
        #endregion

        #region DiscreteDataGraphByGroup
        public System.Collections.Generic.List<DiscreteDataSummary> GetDiscreteDataByGroup(long trialID, long imgProcedureID, string laterality, string group)
        {
            var allData = GetDiscreteDataSummary(trialID, imgProcedureID);
            return allData.Where(d => d.group == group && d.laterality == laterality).ToList();
        }
        #endregion

        #region DiscreteDataReport
        public System.Collections.Generic.List<DiscreteDataSummary> GetDiscreteDataSummary(long trialID, long imgProcedureID)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            var discreteDataSummary = new System.Collections.Generic.List<DiscreteDataSummary>();

            var grs = this.GRD_Reports.Where(g => g.IsActive && g.IsPrimaryResult && g.IsSigned
                && g.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == imgProcedureID
                && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && g.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID != null
                && g.PACSSeries.IsActive);

            if (!trial.IsTestingPhase)
                grs = grs.Where(g => !g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                        && !g.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject);

            foreach (var gr in grs)
            {
                var gResults = this.GRD_ReportResults.Where(a => a.GReportID == gr.GReportID).ToList();

                //Remove any non-numeric string
                var gg = new System.Collections.Generic.List<GRD_ReportResult>();
                foreach (var g in gResults)
                {
                    if (Regex.IsMatch(g.GAnswersString, @"\d"))
                        gg.Add(g);
                }
                gResults = gg;

                var ls = gResults.Select(a => a.Laterality).Distinct();

                foreach (var l in ls)
                {
                    var avg = gResults.Where(a => a.Laterality == l).Select(a => Convert.ToDouble(a.GAnswersString)).Average();

                    discreteDataSummary.Add(new DiscreteDataSummary
                    {
                        laterality = l,
                        group = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectGroup.GroupName,
                        animalID = gr.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                        timePointDes = gr.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                        timePointSeq = gr.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsSeq,
                        measurement = avg
                    });
                }
            }

            return discreteDataSummary.OrderBy(g => g.timePointSeq).ToList();
        }

        public string GetDiscreteDataProcedureDescription(long imgProcedureID)
        {
            var tp = this.CERT_ImgProcedureLists.Single(t => t.ImgProcedureID == imgProcedureID).ImgProcedureDescription;
            return tp;
        }

        public string GetTrialName(long trialID)
        {
            return this.PACS_Trials.Single(t => t.TrialID == trialID).TrialName;
        }
        #endregion

        #region FALeakageReport
        public System.Collections.Generic.List<CNVLeakageGradingList> GetCNVLeakageGradingList(long trialID, long ImgProcedureID)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            var cnvLeakageGradingList = new List<CNVLeakageGradingList>();

            var grs = this.GRD_Reports.Where(g => g.IsActive && g.IsPrimaryResult && g.IsSigned
                && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && g.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == ImgProcedureID
                && g.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID != null
                && g.PACSSeries.IsActive);

            if (!trial.IsTestingPhase)
                grs = grs.Where(g => !g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                        && !g.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject);

            //select the latest primary grading result
            grs = grs.GroupBy(g => g.PACSSeries.SeriesID, g => g,
               (key, elements) => elements.OrderBy(g => g.PerformedTime).Last());

            foreach (var gr in grs)
            {
                var grr = this.GRD_ReportResults.Where(g => g.GReportID == gr.GReportID);

                foreach (var gq in grr)
                {
                    if (gq.GQuestionDes.Contains("CNV Leakage Area"))
                    {
                        //Not a measurement question

                        var clgl = new CNVLeakageGradingList
                        {
                            timePointDes = gq.GRDReport.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                            group = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectGroup.GroupName,
                            // cohort = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort.CohortName,
                            gender = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.Gender,
                            laterality = gq.Laterality,
                            animalID = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                            area = gq.GQuestionString,
                            result = gq.GAnswersString
                        };


                        if (gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort != null)
                            clgl.cohort = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort.CohortName;

                        cnvLeakageGradingList.Add(clgl);
                    }
                }
            }

            //order sequence
            //need to track back to grading template sequence
            cnvLeakageGradingList = cnvLeakageGradingList.OrderBy(s => s.group).ThenBy(s => s.laterality).ToList();

            return cnvLeakageGradingList;
        }
        #endregion

        #region OcularExamReport
        public System.Collections.Generic.List<GroupInformationSummary> GetGroupInformationSummary(long trialID)
        {

            var groupInfoSummary = this.PACS_SubjectGroups.Where(sg => sg.TrialID == trialID && sg.IsActive).Select(sg => new GroupInformationSummary
            {
                groupName = sg.GroupName,
                groupDescription = sg.GroupDescription,
                //                    treatmentLaterality = sg.treatmentLaterility,
                dosingInfo = sg.DosingInfo,
                numberofSubjects = sg.NumberofSubjects
            }).ToList();

            return groupInfoSummary;
        }

        public System.Collections.Generic.List<QASequence> GetQASequenceList(long timePointListID, long imgProcedureID)
        {
            //sort group/question/answer sequence

            var qaSequenceList = new List<QASequence>();

            var tppl = this.PACS_TPProcLists.Single(t => t.TimePointsListID == timePointListID && t.ImgProcedureID == imgProcedureID);
            var gtemplateID = tppl.GTemplateID;
            var questionList = this.GRD_TempQuestions.Where(q => q.GRDQuestionGroup.GTemplateID == gtemplateID);

            //                .OrderBy(q => q.GRDQuestionGroup.GQuestionGroupSeq).ThenBy(q => q.GTempQuestionSeqInGroup).Select(q => q.GRDGradingQuestion)  .OrderBy(a => a.GAnswerSeq)
            foreach (var q in questionList)
            {
                var answerList = this.GRD_GradingAnswers.Where(a => a.TrialID == null && a.GQuestionID == q.GQuestionID)
                    .OrderBy(a => a.GAnswerSeq).ToList();
                //add trial specific grading answers
                answerList.AddRange(this.GRD_GradingAnswers.Where(a => a.TrialID == tppl.PACSTimePointsList.TrialID && a.GQuestionID == q.GQuestionID));

                foreach (var a in answerList)
                {
                    qaSequenceList.Add(new QASequence
                    {
                        questionGroup = q.GRDQuestionGroup.GQuestionGroupName,
                        groupInSeq = (int)q.GRDQuestionGroup.GQuestionGroupSeq,
                        question = q.GRDGradingQuestion.GQuestionString,
                        questionInSeq = (int)q.GRDQuestionGroup.GQuestionGroupSeq,
                        answer = a.GAnswerString,
                        answerInSeq = answerList.IndexOf(a),
                        normal = (answerList.IndexOf(a) == 0) ? true : false
                    });
                }
            }

            return qaSequenceList.OrderBy(qa => qa.groupInSeq).ThenBy(qa => qa.questionInSeq).ThenBy(qa => qa.answerInSeq).ToList();
        }

        public System.Collections.Generic.List<OcularExamSummary> GetOcularExamSummaryByTimePoint(long trialID, long timePointListID, long ImgProcedureID)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            var OESummary = new List<OcularExamSummary>();

            //sort group/question/answer sequence

            var qaSequence = GetQASequenceList(timePointListID, ImgProcedureID);
            var qaNormalSequence = qaSequence.Where(qa => qa.normal);

            var grs = this.GRD_Reports.Where(g => g.IsActive && g.IsPrimaryResult && g.IsSigned
                && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && g.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsListID == timePointListID
                && g.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == ImgProcedureID
                && g.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID != null    //remove all prestudy series
                && g.PACSSeries.IsActive);

            if (!trial.IsTestingPhase)
                grs = grs.Where(g => !g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                        && !g.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject);

            //select the latest primary grading result
            grs = grs.GroupBy(g => g.PACSSeries.SeriesID, g => g,
               (key, elements) => elements.OrderBy(g => g.PerformedTime).Last());

            foreach (var gr in grs)
            {
                var grr = this.GRD_ReportResults.Where(g => g.GReportID == gr.GReportID);

                foreach (var gq in grr)
                {
                    if (gq.GAnswerMeasurement == null && !gq.GQuestionGroupName.Contains("IOP"))
                    {
                        //Not a measurement question

                        var oes = new OcularExamSummary
                        {
                            questionGroup = qaSequence.FirstOrDefault(qs => qs.question == gq.GQuestionString).questionGroup,
                            question = gq.GQuestionString,
                            answer = gq.GAnswersString,
                            timePointSeq = (int)gq.GRDReport.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsSeq,
                            timePointDes = gq.GRDReport.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                            group = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectGroup.GroupName,
                            //                            cohort = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort.CohortName,
                            gender = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.Gender,
                            laterality = gq.Laterality,
                            fontcolor = (qaNormalSequence.Count(qa => qa.question == gq.GQuestionString && qa.answer == gq.GAnswersString) > 0) ? Color.Black : Color.Red,
                            animalID = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID
                        };

                        if (gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort != null)
                            oes.cohort = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort.CohortName;

                        OESummary.Add(oes);
                    }
                }
            }

            //order sequence
            //need to track back to grading template sequence
            OESummary = OESummary.OrderBy(s => s.laterality).ThenBy(s => qaSequence.FindIndex(q => q.question == s.question && q.answer == s.answer)).ToList();

            return OESummary;
        }

        public System.Collections.Generic.List<OcularExamSummary> GetOcularExamSummaryDiseaseOnlyByTimePoint(long trialID, long timePointListID, long ImgProcedureID)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            //sort group/question/answer sequence

            var qaSequence = GetQASequenceList(timePointListID, ImgProcedureID);

            var grs = this.GRD_Reports.Where(g => g.IsActive && g.IsPrimaryResult && g.IsSigned
                && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && g.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsListID == timePointListID
                && g.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == ImgProcedureID
                && g.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID != null     //remove all prestudy series
                && g.PACSSeries.IsActive);

            if (!trial.IsTestingPhase)
                grs = grs.Where(g => !g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                        && !g.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject);

            //select the latest primary grading result
            grs = grs.GroupBy(g => g.PACSSeries.SeriesID, g => g,
               (key, elements) => elements.OrderBy(g => g.PerformedTime).Last());

            var OESummary = GetOESummaryDiseaseOnly(grs, qaSequence);
            return OESummary;
        }

        private List<OcularExamSummary> GetOESummaryDiseaseOnly(IQueryable<GRD_Report> grs, List<QASequence> qaSequence)
        {
            var OESummary = new List<OcularExamSummary>();
            var qaNormalSequence = qaSequence.Where(qa => qa.normal).ToList();

            foreach (var gr in grs)
            {
                var grr = this.GRD_ReportResults.Where(g => g.GReportID == gr.GReportID);

                foreach (var qGroup in qaSequence.Select(qa => qa.questionGroup).Distinct())
                {
                    var laterality = new string[] { "R", "L" };
                    foreach (var eye in laterality)
                    {
                        var grrGroup = grr.Where(g => g.GQuestionGroupName == qGroup && g.Laterality == eye);

                        //if nvl then add it to OESummary
                        //nvl for all questions in the group
                        var nvl = true;
                        foreach (var gg in grrGroup)
                        {
                            if (qaNormalSequence.Count(qa => qa.question == gg.GQuestionString && qa.answer == gg.GAnswersString) == 0)
                                nvl = false;
                        }

                        if (nvl)
                        {
                            var oes = new OcularExamSummary
                            {
                                questionGroup = qGroup,
                                qgInSeq = qaSequence.First(qa => qa.questionGroup == qGroup).groupInSeq,
                                question = "",
                                answer = "No Visible Lesion",
                                qaString = "No Visible Lesion",
                                qaStringInSeq = 0,
                                timePointSeq = (int)gr.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsSeq,
                                timePointDes = gr.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                                group = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectGroup.GroupName,
                                //                            cohort = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort.CohortName,
                                gender = gr.PACSSeries.PACSTimePoint.PACSSubject.Gender,
                                laterality = eye,
                                animalID = gr.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID
                            };
                            if (gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort != null)
                                oes.cohort = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort.CohortName;

                            OESummary.Add(oes);
                        }
                    }
                }


                foreach (var gq in grr)
                {
                    if (gq.GAnswerMeasurement == null && !gq.GQuestionGroupName.Contains("IOP"))
                    {
                        //Not a normal measurement question

                        if ((qaSequence.Count(qa => qa.question == gq.GQuestionString) != 0) &&
                            (qaNormalSequence.Count(qa => qa.question == gq.GQuestionString && qa.answer == gq.GAnswersString) == 0))
                        {
                            var firstQuestionInQaSeq = qaSequence.FirstOrDefault(qa => qa.question == gq.GQuestionString);
                            var firstAnswerInQASeq = qaSequence.FirstOrDefault(qa => qa.question == gq.GQuestionString && qa.answer == gq.GAnswersString);

                            int qasInSeq = 0;

                            if (firstAnswerInQASeq != null && firstQuestionInQaSeq != null)
                            {
                                qasInSeq = (int)(firstQuestionInQaSeq.questionInSeq) * 100 +
                                    ((qaSequence.Count(qa => qa.question == gq.GQuestionString && qa.answer == gq.GAnswersString) == 0) ?
                                     50 : firstAnswerInQASeq.answerInSeq);

                                var oes = new OcularExamSummary
                                {
                                    questionGroup = qaSequence.FirstOrDefault(qs => qs.question == gq.GQuestionString).questionGroup,
                                    qgInSeq = qaSequence.First(qa => qa.questionGroup == gq.GQuestionGroupName).groupInSeq,
                                    question = gq.GQuestionString,
                                    answer = gq.GAnswersString,
                                    qaString = gq.GQuestionString + ", " + gq.GAnswersString,
                                    qaStringInSeq = qasInSeq,
                                    timePointSeq = (int)gq.GRDReport.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsSeq,
                                    timePointDes = gq.GRDReport.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                                    group = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectGroup.GroupName,
                                    //cohort = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort.CohortName,
                                    gender = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.Gender,
                                    laterality = gq.Laterality,
                                    animalID = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID
                                };

                                if (gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort != null)
                                    oes.cohort = gq.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectCohort.CohortName;

                                OESummary.Add(oes);
                            }
                        }
                    }
                }
            }

            //order sequence
            //need to track back to grading template sequence
            OESummary = OESummary.OrderBy(s => s.laterality).ThenBy(s => qaSequence.FindIndex(q => q.question == s.question && q.answer == s.answer)).ToList();
            return OESummary;
        }

        public string GetTimePointDescription(long timePointListID)
        {
            var tp = this.PACS_TimePointsLists.Single(t => t.TimePointsListID == timePointListID).TimePointsDescription;
            return tp;
        }
        #endregion

        #region OcularExamReportByGroup
        public string GetSubjectGroupName(long subjectGroupID)
        {
            var tp = this.PACS_SubjectGroups.Single(s => s.SubjectGroupID == subjectGroupID).GroupName;
            return tp;
        }

        public System.Collections.Generic.List<OcularExamSummary> GetOcularExamSummaryDiseaseOnlyByGroup(long trialID, long subjectGroupID, long timePointListID, long ImgProcedureID)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            //sort group/question/answer sequence

            var qaSequence = GetQASequenceList(timePointListID, ImgProcedureID);

            var grs = this.GRD_Reports.Where(g => g.IsActive && g.IsPrimaryResult && g.IsSigned
                && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectGroup.SubjectGroupID == subjectGroupID
                && g.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == ImgProcedureID
                && g.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID != null     //remove all prestudy series
                && g.PACSSeries.IsActive);

            if (!trial.IsTestingPhase)
                grs = grs.Where(g => !g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                        && !g.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject);

            //select the latest primary grading result
            grs = grs.GroupBy(g => g.PACSSeries.SeriesID, g => g,
               (key, elements) => elements.OrderBy(g => g.PerformedTime).Last());

            var OESummary = GetOESummaryDiseaseOnly(grs, qaSequence);
            return OESummary;
        }

        #endregion

        #region OcularExamReportIndividual
        public System.Collections.Generic.List<OcularExamSummary> GetOcularExamSummaryByAnimalID(long trialID, long timePointListID, long ImgProcedureID, string animalID)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            //sort group/question/answer sequence
            var qaSequence = GetQASequenceList(timePointListID, ImgProcedureID);

            var grs = this.GRD_Reports.Where(g => g.IsActive && g.IsPrimaryResult && g.IsSigned
                && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && g.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureID == ImgProcedureID
                && g.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID == animalID
                && g.PACSSeries.IsActive);

            if (!trial.IsTestingPhase)
                grs = grs.Where(g => !g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                        && !g.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject);

            //select the latest primary grading result
            grs = grs.GroupBy(g => g.PACSSeries.SeriesID, g => g,
               (key, elements) => elements.OrderBy(g => g.PerformedTime).Last());

            var OESummary = GetOESummaryDiseaseOnly(grs, qaSequence);
            return OESummary;
        }
        #endregion

        #region OEBarGraph
        public List<OcularExamHistogram> GetOcularExamHistogramByGroup(long trialID, long subjectGroupID, string QuestionString, string QuestionGroup)
        {
            var grs = this.GRD_Reports.Where(g => g.IsActive && g.IsPrimaryResult && g.IsSigned
            && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
            && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSubjectGroup.SubjectGroupID == subjectGroupID
            && !g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
            && !g.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject
            && g.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID != null     //remove all prestudy series
            && g.PACSSeries.IsActive);

            //select the latest primary grading result
            grs = grs.GroupBy(g => g.PACSSeries.SeriesID, g => g,
               (key, elements) => elements.OrderBy(g => g.PerformedTime).Last());

            var gReportID = grs.Select(g => g.GReportID).ToList();

            var gResults = this.GRD_ReportResults.Where(gr => gr.GQuestionGroupName == QuestionGroup
                    && gr.GQuestionString == QuestionString
                    && gReportID.Contains((long)gr.GReportID));

            var oeExam = new List<OcularExamHistogram>();

            foreach (var gr in gResults)
            {
                oeExam.Add(new OcularExamHistogram
                {
                    question = gr.GQuestionString,
                    answer = gr.GAnswersString,
                    laterality = gr.Laterality,
                    timePointDes = gr.GRDReport.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                    timePointSeq = (int)gr.GRDReport.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsSeq
                });
            }

            return oeExam;
        }

        public List<OcularExamHistogram> GetOcularExamHistogramByGroupRightEye(long trialID, long subjectGroupID, string QuestionString, string QuestionGroup)
        {
            var oeExam = GetOcularExamHistogramByGroup(trialID, subjectGroupID, QuestionString, QuestionGroup);
            return (oeExam.Where(a => a.laterality == "R").ToList());
        }

        public List<OcularExamHistogram> GetOcularExamHistogramByGroupLeftEye(long trialID, long subjectGroupID, string QuestionString, string QuestionGroup)
        {
            var oeExam = GetOcularExamHistogramByGroup(trialID, subjectGroupID, QuestionString, QuestionGroup);
            return (oeExam.Where(a => a.laterality == "L").ToList());
        }
        #endregion

        #endregion

        #region Grading

        #region GradingReport

        public GradingReportSeries GetGradingReportSeries(long seriesID)
        {
            var series = this.PACS_Series.SingleOrDefault(g => g.SeriesID == seriesID);

            if (series == null) return null;

            var gr = new GradingReportSeries
            {
                trialName = series.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialName,
                siteID = series.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID,
                siteName = series.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.AffiliationName,
                alternativeSubjectID = series.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID,
                randomizedSubjectID = series.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                nameCode = series.PACSTimePoint.PACSSubject.NameCode,
                studyEye = series.PACSTimePoint.PACSSubject.Laterality,
                studyDate = string.Format("{0:yyyy-MM-dd}", series.StudyDate)
            };

            var wfg = this.AUDIT_Records.Where(a => a.SeriesID == seriesID && a.WFTempStep.WFStepList.WFStepListDes == "Grade").LastOrDefault();
            gr.gradedBy = (wfg == null) ? "" : wfg.CONTACTUser.LastName + ", " + wfg.CONTACTUser.FirstName;
            var wfv = this.AUDIT_Records.Where(a => a.SeriesID == seriesID && a.WFTempStep.WFStepList.WFStepListDes == "Verify").LastOrDefault();
            gr.reviewedBy = (wfv == null) ? "" : wfv.CONTACTUser.LastName + ", " + wfv.CONTACTUser.FirstName;


            gr.eligibilityTimepoint = series.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint;
            gr.eligibility = series.PACSTimePoint.PACSSubject.IsSubjectRejected ? "Not eligible" : "Eligible";

            return gr;
        }

        public List<GradingReportResults> GradingReportGetResults(long seriesID)
        {
            var gradingReportQA = new List<GradingReportResults>();

            var gr = this.GRD_Reports.Where(g => g.IsActive && g.IsPrimaryResult && g.IsSigned && g.SeriesID == seriesID).OrderBy(g => g.PerformedTime).LastOrDefault();
            if (gr == null) return null;

            var grr = this.GRD_ReportResults.Where(g => g.GReportID == gr.GReportID);

            foreach (var gq in grr)
            {
                if (gq.GAnswerMeasurement == null)
                    //Not a measurement question
                    gradingReportQA.Add(new GradingReportResults
                    {
                        laterality = gq.Laterality,
                        question = gq.GQuestionString,
                        answer = gq.GAnswersString
                    });
                else
                {
                    var gam = this.MEA_Measurements.Single(m => m.MeasurementID == gq.GAnswerMeasurement);

                    switch (gam.MEAMeasurementType.MeasurementType)
                    {
                        case "Distance":
                            gradingReportQA.Add(new GradingReportResults
                            {
                                laterality = gq.Laterality,
                                question = gq.GQuestionString,
                                answer = string.Format("{0:0.000} mm", (gam as MEA_Distance).DistanceMm)
                            });
                            break;
                        case "Area":
                            gradingReportQA.Add(new GradingReportResults
                            {
                                laterality = gq.Laterality,
                                question = gq.GQuestionString,
                                answer = string.Format("{0:0.000} mm2", (gam as MEA_Area).AreaSizeMm2)
                            });
                            break;
                        case "OCTGrid":
                            var octGrid = gam as MEA_OCTGrid;
                            var reliable = grr.FirstOrDefault(x => x.GQuestionString.ToLower() == "thickness grid reliable");
                            if (reliable != null && reliable.GAnswersString.ToLower() == "yes")
                            {
                                var result = new GradingReportResults
                                {
                                    laterality = gq.Laterality,
                                    question = string.Format("{0} - CSF thickness", gq.GQuestionString),
                                    answer = string.Format("{0:0.000} mm", octGrid.Sector0Thick)
                                };
                                gradingReportQA.Add(result);

                                result = new GradingReportResults
                                {
                                    laterality = gq.Laterality,
                                    question = string.Format("{0} - CP thickness", gq.GQuestionString),
                                    answer = string.Format("{0:0.000} mm", octGrid.CenterPointThicknessMm)
                                };
                                gradingReportQA.Add(result);
                            }
                            else
                            {
                                var result = new GradingReportResults
                                {
                                    laterality = gq.Laterality,
                                    question = string.Format("{0} - CSF thickness", gq.GQuestionString)
                                };
                                reliable = grr.FirstOrDefault(x => x.GQuestionString.ToLower() == "center subfield thickness reliable");
                                if (reliable != null && reliable.GAnswersString.ToLower() == "no")
                                    result.answer = "CG";
                                else
                                    result.answer = string.Format("{0:0.000} mm", octGrid.Sector0Thick);
                                gradingReportQA.Add(result);

                                result = new GradingReportResults
                                {
                                    laterality = gq.Laterality,
                                    question = string.Format("{0} - CP thickness", gq.GQuestionString)
                                };
                                reliable = grr.FirstOrDefault(x => x.GQuestionString.ToLower() == "center point thickness reliable");
                                if (reliable != null && reliable.GAnswersString.ToLower() == "no")
                                    result.answer = "CG";
                                else
                                    result.answer = string.Format("{0:0.000} mm", octGrid.CenterPointThicknessMm);
                                gradingReportQA.Add(result);
                            }
                            break;
                    }
                }
            }

            return gradingReportQA;
        }

        #endregion

        #endregion

        #region GradingSummary

        #region EligibilityDetailReport
        public System.Collections.Generic.List<EligDetailReport> GetEligDetailReport(long trialID)
        {
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            var EligDetailQA = new List<EligDetailReport>();

            var grs = this.GRD_Reports.Where(g => g.IsActive && g.IsPrimaryResult && g.IsSigned
                && g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && g.PACSSeries.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint
                && g.PACSSeries.IsActive);

            if (!trial.IsTestingPhase)
                grs = grs.Where(g => !g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                        && !g.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject);

            var grs2 = grs.OrderBy(g => g.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID);

            //select the latest primary grading result
            var result2 = grs2.GroupBy(g => g.PACSSeries.SeriesID, g => g,
               (key, elements) => elements.OrderBy(g => g.PerformedTime).Last());

            //eligibility grading involves multiple modalities most of the time. Select one grading result.
            var result = result2.GroupBy(g => g.PACSSeries.PACSTimePoint.TimePointsID, g => g,
               (key, elements) => elements.First());

            foreach (var gr in result)
            {
                var grr = this.GRD_ReportResults.Where(g => g.GReportID == gr.GReportID);

                foreach (var gq in grr)
                {
                    if (gq.GAnswerMeasurement == null)
                        //Not a measurement question
                        EligDetailQA.Add(new EligDetailReport
                        {
                            siteID = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID,
                            siteName = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.AffiliationName,
                            alternativeSubjectID = gr.PACSSeries.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID,
                            randomizedSubjectID = gr.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                            nameCode = gr.PACSSeries.PACSTimePoint.PACSSubject.NameCode,
                            studyEye = gr.PACSSeries.PACSTimePoint.PACSSubject.Laterality,
                            studyDate = string.Format("{0:yyyy-MM-dd}", gr.PACSSeries.StudyDate),
                            question = gq.GQuestionString,
                            answer = gq.GAnswersString
                        });
                    else
                    {
                        var gam = this.MEA_Measurements.Single(m => m.MeasurementID == gq.GAnswerMeasurement);

                        switch (gam.MEAMeasurementType.MeasurementType)
                        {
                            case "Distance":
                                EligDetailQA.Add(new EligDetailReport
                                {
                                    siteID = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID,
                                    siteName = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.AffiliationName,
                                    alternativeSubjectID = gr.PACSSeries.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID,
                                    randomizedSubjectID = gr.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                                    nameCode = gr.PACSSeries.PACSTimePoint.PACSSubject.NameCode,
                                    studyEye = gr.PACSSeries.PACSTimePoint.PACSSubject.Laterality,
                                    studyDate = string.Format("{0:yyyy-MM-dd}", gr.PACSSeries.StudyDate),
                                    question = gq.GQuestionString,
                                    answer = string.Format("{0:0.000} mm", (gam as MEA_Distance).DistanceMm)
                                });
                                break;
                            case "Area":
                                EligDetailQA.Add(new EligDetailReport
                                {
                                    siteID = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID,
                                    siteName = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.AffiliationName,
                                    alternativeSubjectID = gr.PACSSeries.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID,
                                    randomizedSubjectID = gr.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                                    nameCode = gr.PACSSeries.PACSTimePoint.PACSSubject.NameCode,
                                    studyEye = gr.PACSSeries.PACSTimePoint.PACSSubject.Laterality,
                                    studyDate = string.Format("{0:yyyy-MM-dd}", gr.PACSSeries.StudyDate),
                                    question = gq.GQuestionString,
                                    answer = string.Format("{0:0.000} mm2", (gam as MEA_Area).AreaSizeMm2)
                                });
                                break;
                            case "OCTGrid":
                                EligDetailQA.Add(new EligDetailReport
                                {
                                    siteID = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID,
                                    siteName = gr.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.AffiliationName,
                                    alternativeSubjectID = gr.PACSSeries.PACSTimePoint.PACSSubject.AlternativeRandomizedSubjectID,
                                    randomizedSubjectID = gr.PACSSeries.PACSTimePoint.PACSSubject.RandomizedSubjectID,
                                    nameCode = gr.PACSSeries.PACSTimePoint.PACSSubject.NameCode,
                                    studyEye = gr.PACSSeries.PACSTimePoint.PACSSubject.Laterality,
                                    studyDate = string.Format("{0:yyyy-MM-dd}", gr.PACSSeries.StudyDate),
                                    question = "CSF thickness",
                                    answer = string.Format("{0:0.000} mm", (gam as MEA_OCTGrid).Sector1Thick)
                                });
                                break;
                        }
                    }
                }
            }
            return EligDetailQA;
        }
        #endregion

        #region GrdConfidence
        public System.Collections.Generic.List<GrdConfidenceScore> GrdConfidenceGetScore(long trialID, string procedure)
        {
            var cScore = this.GRD_ReportResults.Where(c => (c.GQuestionString.ToLower().Contains("confidence score"))
                && c.GRDReport.IsActive && c.GRDReport.IsPrimaryResult
                && !c.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                && !c.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject
                && (c.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID)
                && c.GAnswersString != null);

            if (procedure != "All")
                cScore = cScore.Where(c => c.GRDReport.PACSSeries.PACSTPProcList.CERTImgProcedureList.ImgProcedureDescription == procedure);

            var grdCScores = new List<GrdConfidenceScore>();

            foreach (var c in cScore)
            {
                var grdCScore = new GrdConfidenceScore();
                grdCScore.siteID = c.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.RandomizedSiteID;
                grdCScore.siteName = c.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.CONTACTAffiliation.AffiliationName;

                if (c.GAnswersString.IndexOf("CS1") != -1) grdCScore.confidenceScore = "CS1: high confidence";
                else
                {
                    if (c.GAnswersString.IndexOf("CS2") != -1) grdCScore.confidenceScore = "CS2: adequate confidence";
                    else
                    {
                        if (c.GAnswersString.IndexOf("CS3") != -1) grdCScore.confidenceScore = "CS3: low confidence";
                        else grdCScore.confidenceScore = c.GAnswersString;
                    }
                }

                grdCScores.Add(grdCScore);
            }
            return grdCScores;
        }

        #endregion

        #region GrdSummary
        public List<GrdSummarySubjectGroup> GetGrdSummarySubjectGroup(long trialID)
        {
            var grp = this.PACS_SubjectGroups.Where(g => g.PACSTrial.TrialID == trialID).ToList();
            return grp.Select(g => new GrdSummarySubjectGroup
            {
                groupID = g.SubjectGroupID,
                groupName = g.GroupName
            }).ToList();
        }
        public List<GrdSummarySubjectCohort> GetGrdSummarySubjectCohort(long trialID)
        {
            var cht = this.PACS_SubjectCohorts.Where(g => g.PACSTrial.TrialID == trialID);

            return cht.Select(c => new GrdSummarySubjectCohort
            {
                cohortID = c.SubjectCohortID,
                cohortName = c.CohortName
            }).ToList();
        }
        public List<string> GetGrdSummaryGraphType()
        {
            return new List<string> { "Table", "Graph" };
        }
        public List<GrdSummarySubject> GetGrdSummarySubjects(long trialID)
        {
            var subj = this.PACS_Subjects.Where(s => s.PACSSite.PACSTrial.TrialID == trialID
                && s.IsActive);

            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);
            if (!trial.IsTestingPhase)
                subj = subj.Where(s => !s.PACSSite.IsTestingSite && !s.IsTestingSubject);

            //if (subjGroupID != null)
            //    subj = subj.Where(s => s.SubjectGroupID == subjGroupID).ToList();
            //if (subjCohortID != null)
            //    subj = subj.Where(s => s.SubjectCohortID == subjCohortID).ToList();

            return subj.Select(s => new GrdSummarySubject
            {
                subjectID = s.SubjectID,
                displayName = (s.RandomizedSubjectID != null) ? s.RandomizedSubjectID : s.AlternativeRandomizedSubjectID
            }).ToList();
        }
        public IQueryable<GRD_GradingTemplate> GetGrdSummaryGradingTemplate(long trialID)
        {
            return this.GRD_GradingTemplates.Where(t => t.TrialID == trialID);
        }
        public List<GRD_GradingQuestion> GetGrdSummaryGradingQuestion(long gTemplate)
        {
            var gQuestions = new System.Collections.Generic.List<GRD_GradingQuestion>();

            var qGroups = this.GRD_QuestionGroups.Where(qg => qg.GRDGradingTemplate.GTemplateID == gTemplate).OrderBy(g => g.GQuestionGroupSeq).ToList();
            foreach (var qg in qGroups)
            {
                var qInGroup = this.GRD_TempQuestions.Where(tq => tq.GRDQuestionGroup.GQuestionGroupID == qg.GQuestionGroupID).OrderBy(g => g.GTempQuestionSeqInGroup).ToList();
                foreach (var q in qInGroup)
                    gQuestions.Add(this.GRD_GradingQuestions.Single(g => g.GQuestionID == q.GQuestionID));
            }

            return gQuestions;
        }
        public List<GrdCategoryAnswerCount> GetGrdCategoryAnswerCount(long trialID, long? subjGroupID, long? subjCohortID, Array subjectID, long gTemplateID, long gQuestionID)
        {
            var grdSummary = new List<GrdCategoryAnswerCount>();
            var subj = new List<PACS_Subject>();
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            if ((subjectID != null) && (subjectID.Length != 0))
            {
                foreach (var s in subjectID)
                    subj.Add(this.PACS_Subjects.Single(t => t.SubjectID == (long)s));
            }
            else
            {
                var subjQ = this.PACS_Subjects.Where(s => s.PACSSite.PACSTrial.TrialID == trialID
                    && s.IsActive);

                if (!trial.IsTestingPhase)
                    subjQ = subjQ.Where(s => !s.PACSSite.IsTestingSite && !s.IsTestingSubject);

                if (subjGroupID != null)
                    subjQ = subjQ.Where(s => s.SubjectGroupID == subjGroupID);
                if (subjCohortID != null)
                    subjQ = subjQ.Where(s => s.SubjectCohortID == subjCohortID);

                subj = subjQ.ToList();
            }

            var timePointList = this.PACS_TimePointsLists.Where(tl => tl.TrialID == trialID).OrderBy(tl => tl.TimePointsSeq).ToList();
            var gAnswerList = this.GRD_GradingAnswers.Where(ga => (ga.TrialID == null || ga.TrialID == trialID) && ga.GQuestionID == gQuestionID).OrderBy(ga => ga.GAnswerSeq);

            var rResults = this.GRD_ReportResults.Where(rr => rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && rr.GRDReport.GTemplateID == gTemplateID
                && rr.GRDReport.IsActive && rr.GRDReport.IsPrimaryResult);

            if (!trial.IsTestingPhase)
                rResults = rResults.Where(rr => !rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject && !rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);

            //only selected subjects
            rResults = rResults.Where(rr => subj.Contains(rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject));
            var gQString = this.GRD_GradingQuestions.Single(q => q.GQuestionID == gQuestionID).GQuestionString;
            //only the matched questions
            rResults = rResults.Where(rr => rr.GQuestionString == gQString);
            //study eye only
            rResults = rResults.Where(rr => rr.Laterality == rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.Laterality);


            foreach (var tp in timePointList)
                foreach (var ga in gAnswerList)
                {
                    var temp = rResults.Where(r => r.GAnswersString == ga.GAnswerString
                        && r.GRDReport.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsListID == tp.TimePointsListID);

                    var aCount = (temp.Count() > 0) ? temp.Count().ToString() : "-";
                    grdSummary.Add(new GrdCategoryAnswerCount { tPointList = tp.TimePointsDescription, gAnswer = ga.GAnswerString, resultID = aCount });
                }

            return grdSummary;
        }
        public List<GrdNumericalAnswer> GetGrdNumericalAnswer(long trialID, long? subjGroupID, long? subjCohortID, Array subjectID, long gTemplateID, long gQuestionID)
        {
            var grdSummary = new List<GrdNumericalAnswer>();
            var subj = new List<PACS_Subject>();
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);

            if ((subjectID != null) && (subjectID.Length != 0))
            {
                foreach (var s in subjectID)
                    subj.Add(this.PACS_Subjects.Single(t => t.SubjectID == (long)s));
            }
            else
            {
                var subjQ = this.PACS_Subjects.Where(s => s.PACSSite.PACSTrial.TrialID == trialID
                    && s.IsActive);

                if (!trial.IsTestingPhase)
                    subjQ = subjQ.Where(s => !s.PACSSite.IsTestingSite && !s.IsTestingSubject);

                if (subjGroupID != null)
                    subjQ = subjQ.Where(s => s.SubjectGroupID == subjGroupID);
                if (subjCohortID != null)
                    subjQ = subjQ.Where(s => s.SubjectCohortID == subjCohortID);

                subj = subjQ.ToList();
            }

            var timePointList = this.PACS_TimePointsLists.Where(tl => tl.TrialID == trialID).OrderBy(tl => tl.TimePointsSeq);

            var rResults = this.GRD_ReportResults.Where(rr => rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && rr.GRDReport.GTemplateID == gTemplateID
                && rr.GRDReport.IsActive && rr.GRDReport.IsPrimaryResult);

            if (!trial.IsTestingPhase)
                rResults = rResults.Where(rr => !rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject && !rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);

            //only selected subjects
            rResults = rResults.Where(rr => subj.Contains(rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject));

            var gQString = this.GRD_GradingQuestions.Single(q => q.GQuestionID == gQuestionID).GQuestionString;
            //only the matched questions
            rResults = rResults.Where(rr => rr.GQuestionString == gQString);
            //study eye only
            rResults = rResults.Where(rr => rr.Laterality == rr.GRDReport.PACSSeries.PACSTimePoint.PACSSubject.Laterality);

            //            var tpl = 0;
            foreach (var tp in timePointList)
            {
                var temp = rResults.Where(r => r.GRDReport.PACSSeries.PACSTimePoint.PACSTimePointsList.TimePointsListID == tp.TimePointsListID);

                var cum = 0.0;
                foreach (var t in temp)
                {
                    //Check if measurement
                    if (t.GAnswerMeasurement != null)
                    {
                        var meas = this.MEA_Measurements.Single(m => m.MeasurementID == t.GAnswerMeasurement);

                        try
                        {
                            switch (meas.MEAMeasurementType.MeasurementTypeID)
                            {
                                case 1: //distance
                                    cum += (double)(meas as MEA_Distance).DistanceMm;
                                    break;
                                case 2: //area
                                    cum += (double)(meas as MEA_Area).AreaSizeMm2;
                                    break;
                                case 5: //OCT grid
                                    cum += (double)(meas as MEA_OCTGrid).Sector0Thick;
                                    break;
                                default:
                                    // case 3 ETDRS grid
                                    // case 4 OCT layer 
                                    break;
                            }
                        }
                        catch { }
                    }

                    else
                    //if typed-in measurement, try to convert from string to values
                    {
                        try
                        {
                            cum += Convert.ToDouble(t.GAnswersString);
                        }
                        catch
                        { }
                    }
                }
                grdSummary.Add(new GrdNumericalAnswer { avgValue = cum / temp.Count(), tPointList = tp.TimePointsDescription });

            }

            return grdSummary;
        }

        #endregion

        #region SubjEligibility
        public System.Collections.Generic.List<subjEligibilityReport> GetSubjEligibility(long trialID, bool? e)
        {
            var subj = this.PACS_Subjects.Where(s => s.PACSSite.PACSTrial.TrialID == trialID
                && s.IsActive && !s.PACSSite.IsTestingSite && !s.IsTestingSubject);

            if (e != null)
                subj = subj.Where(s => s.IsSubjectRejected != e);

            //eligibility is determined with at least one series that is not elgibility timeponint exists.
            //need to change in the next version a different logic in determining eligibility
            return subj.ToList().Select(s => new subjEligibilityReport
            {
                siteID = s.PACSSite.RandomizedSiteID,
                siteName = s.PACSSite.CONTACTAffiliation.AffiliationName,
                subjID = (s.RandomizedSubjectID != null) ? s.RandomizedSubjectID : s.AlternativeRandomizedSubjectID,
                subjNameCode = s.NameCode,
                laterality = s.Laterality,
                eligbility = s.IsSubjectRejected ? "No" :
                        ((this.PACS_Series.Count(sr => sr.IsActive && sr.PACSTimePoint.SubjectID == s.SubjectID && !sr.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint) == 0) ?
                        "NA" : "Yes"),
                firstStudyDate = string.Format("{0:ddMMMyyyy}", this.PACS_Series.Where(sr => sr.IsActive && sr.PACSTimePoint.SubjectID == s.SubjectID).OrderBy(sr => sr.StudyDate).FirstOrDefault()?.StudyDate)
            }).ToList();
        }

        #endregion

        #endregion

        #region Imaging

        #region KeyFramesReport
        public IQueryable<WF_Sequence> GetSeriesDetails(long seriesID)
        {
            var series = this.WF_Sequences.Where(w => w.SeriesID == seriesID);

            return series;
        }
        public IQueryable<KeyFrame> GetKeyFrames(long seriesID)
        {
            //var url = GetMediaStorageURL();
            var url = "";
            var serie = this.WF_Sequences.Single(w => w.SeriesID == seriesID);

            var seriesWithoutKeyFrames = this.PACS_Series.Where(s => s.IsActive &&
                s.PACSTimePoint.SubjectID == serie.PACSTimePoint.SubjectID &&
                s.PACSTPProcList.ImgProcedureID == serie.PACSTPProcList.ImgProcedureID &&
                !s.PACS_RawData.Any(rd => rd.IsActive && rd.PACS_DicomFrames.Any(f => f.IsActive && f.IsKeyFrame))).Select(s => s.SeriesID).ToList();

            var firstFrames = this.PACS_DicomFrames.Where(f => f.IsActive && seriesWithoutKeyFrames.Contains(f.PACSRawDatum.SeriesID.Value)).OrderBy(f => f.PACSRawDatum.RawDataID).ThenBy(f => f.FrameIndex).GroupBy(f => f.PACSRawDatum.SeriesID).ToList().Select(x => x.First());

            var keyFrames = this.PACS_DicomFrames.Where(f => f.PACSRawDatum.PACSSeries.PACSTimePoint.SubjectID == serie.PACSTimePoint.SubjectID &&
                f.PACSRawDatum.PACSSeries.PACSTPProcList.ImgProcedureID == serie.PACSTPProcList.ImgProcedureID && f.PACSRawDatum.IsActive &&
                f.PACSRawDatum.PACSSeries.IsActive && f.IsKeyFrame && f.IsActive);

            var frames = keyFrames.Union(firstFrames).Select(f => new KeyFrame(f, url)).OrderBy(kf => kf.timepointSeq).ThenBy(f => f.rdid).ThenBy(kf => kf.index);

            return frames;
        }
        //public string GetMediaStorageURL()
        //{
        //    try
        //    {
        //        string sas = null;
        //        var account = CloudStorageAccount.Parse(Config.GetSetting("StorageConnection"));
        //        //var account = CloudStorageAccount.FromConfigurationSetting("StorageConnection");//**Azure update
        //        var blobs = account.CreateCloudBlobClient();
        //        var container = blobs.GetContainerReference("media-container");

        //        var sasExpirationTime = Convert.ToDouble(Config.GetSetting("ReadOnlySASExpirationTime"));
        //        //sas = container.GetSharedAccessSignature(new SharedAccessPolicy()//**Azure update
        //        sas = container.GetSharedAccessSignature(new SharedAccessBlobPolicy()
        //        {
        //            //Permissions = SharedAccessPermissions.Read,
        //            SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(sasExpirationTime),
        //            //SharedAccessStartTime = DateTime.UtcNow
        //        }, Config.GetSetting("MediaContainerROSharedKey"));

        //        UriBuilder uriBuilder = (new UriBuilder(container.Uri)
        //        {
        //            Query = sas.TrimStart('?')
        //        });

        //        if (HttpContext.Current.Request.IsSecureConnection)
        //        {
        //            uriBuilder.Scheme = Uri.UriSchemeHttps;
        //            uriBuilder.Port = 443;
        //        }
        //        else
        //        {
        //            uriBuilder.Scheme = Uri.UriSchemeHttp;
        //            uriBuilder.Port = 80;
        //        }
        //        return uriBuilder.Uri.AbsoluteUri;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Error getting media storage url.", e);
        //    }
        //}

        #endregion

        #endregion

        #region Performance

        #region QueryReport
        public System.Collections.Generic.List<QueryReportItem> GetQueryReport(long trialID, DateTime startDate)
        {
            var queryReportItems = new List<QueryReportItem>();

            var queries = this.QRY_Queries.Where(q => q.TrialID == trialID
                                && ((q.PACSSeries != null) ? !q.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject
                                    && !q.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite : true));

            var resolvedbefore = string.Format("Resolved Before: {0:yyyy-MM-dd}", startDate.Date);
            var resolvedafter = string.Format("Resolved After: {0:yyyy-MM-dd}", startDate.Date);

            foreach (var q in queries)
            {
                var site = new PACS_Site();
                if (q.CertUserID != null)
                    site = this.PACS_Sites.SingleOrDefault(s => s.TrialID == trialID && s.AffiliationID == q.CERTUser.CONTACTUserTrial.CONTACTUser.CONTACTAffiliation.AffiliationID);
                else
                {
                    if (q.CertEquipmentID != null)
                        site = this.PACS_Sites.SingleOrDefault(s => s.TrialID == trialID && s.AffiliationID == q.CERTEquipment.CONTACTEquipment.AffiliationID);
                    else
                        site = (q.PACSSeries.PACSTimePoint.PACSSubject != null) ? q.PACSSeries.PACSTimePoint.PACSSubject.PACSSite : null;
                }

                if (site != null)
                {
                    queryReportItems.Add(new QueryReportItem
                    {
                        type = (q.CertUserID == null && q.CertEquipmentID == null) ? "Study" : "Certification",
                        siteID = site.RandomizedSiteID,
                        siteName = site.CONTACTAffiliation.AffiliationName,
                        progress = (!q.IsResolved) ? "Pending" : (((q.DateResolved.Value.Date) <= startDate.Date) ? resolvedbefore : resolvedafter),
                        queryID = q.QueryID
                    });
                }
            }

            return queryReportItems;
        }

        #endregion

        #region RCEligibilityPerformance
        public RCEligibilityPerformanceMetrics GetRCEligibilityPerformanceMetrics(long trialID)
        {
            var rm = new RCEligibilityPerformanceMetrics();
            var trial = this.PACS_TrialKeyMetrics.Single(t => t.TrialID == trialID);
            rm.trialName = trial.TrialName;
            var trialStartDate = trial.TrialStartDate;
            if (trialStartDate == null) trialStartDate = new DateTime(2000, 1, 1);
            var usHolidays = USHolidays((DateTime)trialStartDate, DateTime.UtcNow);

            //Eligibility turn around metrics
            rm.targetRCEligibilityTAT = trial.TargetEligibilityTotalTAT;
            rm.targetDQEEligibilityTAT = trial.TargetEligibilityCheckinTAT;
            rm.targetGradingEligibilityTAT = trial.TargetEligibilityGradingTAT;

            var auditEligibilityRecQ = this.AUDIT_Records.WithOption(new QueryOptions()
            {
                CommandTimeout = 300 //in seconds                    
            }).Where(ar => ar.TrialID == trialID
                && ar.PACSSeries != null
                && ar.PACSSeries.IsActive
                && ar.PACSSeries.PACSTimePoint.PACSTimePointsList.IsInitialTimePoint
                && !ar.PACSSeries.QRY_Queries.Any());

            if (!trial.IsTestingPhase)
                auditEligibilityRecQ = auditEligibilityRecQ.Where(ar => !ar.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject && !ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);

            var auditEligibilityRec = auditEligibilityRecQ.ToList();

            var audEligibilityCompleted = auditEligibilityRec.Where(ar => (ar.WFTempStep != null) && (ar.WFTempStep.WFStepList.WFStepListDes == "Completed"))
                .GroupBy(ar => ar.SeriesID, ar => ar, (key, elements) => elements.OrderBy(ar => ar.PerformedDateTime).First()).ToList();

            var cumEligibilityRCTAT = 0.0;
            var cumEligibilityDQETAT = 0.0;
            var cumEligibilityGradingTAT = 0.0;
            rm.countEligibilityRC = 0;
            rm.countEligibilityGrading = 0;
            rm.countEligibilityDQE = 0;
            rm.countEligibilityTimelinessRC = 0;
            rm.countEligibilityTimelinessGrading = 0;
            rm.countEligibilityTimelinessDQE = 0;

            foreach (var dtCompleted in audEligibilityCompleted)
            {
                var temp = auditEligibilityRec.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                    && (au.AuditRecordID < dtCompleted.AuditRecordID)
                    && (au.WFTempStep != null) && (au.WFTempStep.WFStepList.WFStepListDes == "Check-in")
                            && (au.AUDITAction.AuditActionName == "WorkflowStepInitiated")).OrderBy(au => au.PerformedDateTime);
                var dtCheckIn = temp.LastOrDefault();
                if (dtCheckIn == null)
                {
                    temp = auditEligibilityRec.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                    && (au.AuditRecordID < dtCompleted.AuditRecordID)
                    && (au.WFTempStep != null) && (au.WFTempStep.WFStepList.WFStepListDes == "Upload")
                        && (au.AUDITAction.AuditActionName == "WorkflowStepCompleted")).OrderBy(au => au.PerformedDateTime);
                    dtCheckIn = temp.LastOrDefault();
                }

                temp = auditEligibilityRec.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                    && (au.AuditRecordID < dtCompleted.AuditRecordID)
                    && (au.WFTempStep != null) && (au.WFTempStep.WFStepList.WFStepListDes == "Grade")
                        && (au.AUDITAction.AuditActionName == "WorkflowStepInitiated")).OrderBy(au => au.PerformedDateTime);
                var dtGrade = temp.LastOrDefault();
                if (dtGrade == null)
                {
                    temp = auditEligibilityRec.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                       && (au.AuditRecordID < dtCompleted.AuditRecordID)
                       && (au.WFTempStep != null) && (au.WFTempStep.WFStepList.WFStepListDes == "Check-in")
                            && (au.AUDITAction.AuditActionName == "WorkflowStepCompleted")).OrderBy(au => au.PerformedDateTime);
                    dtGrade = temp.LastOrDefault();
                }

                if ((dtCheckIn != null) && (dtGrade != null))
                {
                    if (dtGrade.PerformedDateTime < dtCheckIn.PerformedDateTime)
                        dtGrade = dtCheckIn;

                    var rcEligibilityTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);
                    var dqeEligibilityTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtGrade.PerformedDateTime, usHolidays);
                    var gradingEligibilityTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);

                    cumEligibilityRCTAT += rcEligibilityTAT;
                    cumEligibilityDQETAT += dqeEligibilityTAT;
                    cumEligibilityGradingTAT += gradingEligibilityTAT;
                    rm.countEligibilityRC++;
                    rm.countEligibilityDQE++;
                    rm.countEligibilityGrading++;
                    if (rcEligibilityTAT <= rm.targetRCEligibilityTAT) rm.countEligibilityTimelinessRC++;
                    if (dqeEligibilityTAT <= rm.targetDQEEligibilityTAT) rm.countEligibilityTimelinessDQE++;
                    if (gradingEligibilityTAT <= rm.targetGradingEligibilityTAT) rm.countEligibilityTimelinessGrading++;
                }
                else if (dtGrade != null) //sometime series automatically generated
                {
                    var rcEligibilityTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);
                    var gradingEligibilityTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);

                    cumEligibilityRCTAT += rcEligibilityTAT;
                    cumEligibilityGradingTAT += gradingEligibilityTAT;
                    rm.countEligibilityRC++;
                    rm.countEligibilityGrading++;
                    if (rcEligibilityTAT <= rm.targetRCEligibilityTAT) rm.countEligibilityTimelinessRC++;
                    if (gradingEligibilityTAT <= rm.targetGradingEligibilityTAT) rm.countEligibilityTimelinessGrading++;
                }
                else if (dtCheckIn != null)
                {
                    var rcEligibilityTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);
                    var dqeEligibilityTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);

                    cumEligibilityRCTAT += rcEligibilityTAT;
                    cumEligibilityDQETAT += dqeEligibilityTAT;
                    rm.countEligibilityRC++;
                    rm.countEligibilityDQE++;
                    if (rcEligibilityTAT <= rm.targetRCEligibilityTAT) rm.countEligibilityTimelinessRC++;
                    if (dqeEligibilityTAT <= rm.targetDQEEligibilityTAT) rm.countEligibilityTimelinessDQE++;
                }
            }

            rm.avgRCEligibilityTAT = (rm.countEligibilityRC > 0) ? cumEligibilityRCTAT / rm.countEligibilityRC : double.NaN;
            rm.avgDQEEligibilityTAT = (rm.countEligibilityDQE > 0) ? cumEligibilityDQETAT / rm.countEligibilityDQE : double.NaN;
            rm.avgGradingEligibilityTAT = (rm.countEligibilityGrading > 0) ? cumEligibilityGradingTAT / rm.countEligibilityGrading : double.NaN;
            rm.timelinessRCEligibility = (rm.countEligibilityRC > 0) ? rm.countEligibilityTimelinessRC / rm.countEligibilityRC : double.NaN;
            rm.timelinessDQEEligibility = (rm.countEligibilityDQE > 0) ? rm.countEligibilityTimelinessDQE / rm.countEligibilityDQE : double.NaN;
            rm.timelinessGradingEligibility = (rm.countEligibilityGrading > 0) ? rm.countEligibilityTimelinessGrading / rm.countEligibilityGrading : double.NaN;

            return rm;
        }

        #endregion

        #region RCPerformance
        public RCPerformanceMetrics GetRCPerformanceMetrics(long trialID)
        {
            //            return null;

            //            The following code works fine, but takes too long to execute

            var rm = new RCPerformanceMetrics();
            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);
            rm.trialName = trial.TrialName;
            var trialStartDate = trial.TrialStartDate;
            if (trialStartDate == null) trialStartDate = new DateTime(2000, 1, 1);
            var usHolidays = USHolidays((DateTime)trialStartDate, DateTime.UtcNow);

            //Overall turn around metrics
            rm.targetRCTAT = (trial as PACS_TrialKeyMetric).TargetTotalTAT;
            rm.targetDQETAT = (trial as PACS_TrialKeyMetric).TargetCheckinTAT;
            rm.targetGradingTAT = (trial as PACS_TrialKeyMetric).TargetGradingTAT;

            var auditRecQ = this.AUDIT_Records.Where(ar =>
                ar.PerformedDateTime > (DateTime.UtcNow).AddMonths(-3)
                && ar.TrialID == trialID
                && ar.PACSSeries != null
                && ar.PACSSeries.IsActive
                && (!ar.PACSSeries.QRY_Queries.Any()));

            if (!trial.IsTestingPhase)
                auditRecQ = auditRecQ.Where(ar => !ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                    && !ar.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject);

            var auditRec = auditRecQ.ToList();
            var audCompletedQ = auditRec.Where(ar => (ar.WFTempStep != null) && (ar.WFTempStep.WFStepList.WFStepListDes == "Completed")).ToList();

            var audCompleted = audCompletedQ.GroupBy(ar => ar.SeriesID, ar => ar, (key, elements) => elements.OrderBy(ar => ar.PerformedDateTime).First()).ToList();

            var cumAvgRCTAT = 0.0;
            var cumAvgDQETAT = 0.0;
            var cumAvgGradingTAT = 0.0;
            rm.countRC = 0;
            rm.countGrading = 0;
            rm.countDQE = 0;
            rm.countTimelinessRC = 0;
            rm.countTimelinessGrading = 0;
            rm.countTimelinessDQE = 0;

            foreach (var dtCompleted in audCompleted)
            {
                var temp = auditRec.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                       && (au.AuditRecordID < dtCompleted.AuditRecordID)
                       && (au.WFTempStep != null) && (au.WFTempStep.WFStepList.WFStepListDes == "Check-in")
                        && (au.AUDITAction.AuditActionName == "WorkflowStepInitiated")).OrderBy(au => au.PerformedDateTime);
                var dtCheckIn = temp.LastOrDefault();
                //if (dtCheckIn == null)
                //{
                //    temp = auditRec.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                //       && (au.AuditRecordID < dtCompleted.AuditRecordID)
                //       && (au.WFTempStep != null) && (au.WFTempStep.WFStepList.WFStepListDes == "Upload")
                //        && (au.AUDITAction.AuditActionName == "WorkflowStepCompleted")).OrderBy(au => au.PerformedDateTime);
                //    dtCheckIn = temp.LastOrDefault();
                //}

                temp = auditRec.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                    && (au.AuditRecordID < dtCompleted.AuditRecordID)
                    && (au.WFTempStep != null) && (au.WFTempStep.WFStepList.WFStepListDes == "Grade")
                        && (au.AUDITAction.AuditActionName == "WorkflowStepInitiated")).OrderBy(au => au.PerformedDateTime);
                var dtGrade = temp.LastOrDefault();
                //if (dtGrade == null)
                //{
                //    temp = auditRec.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                //        && (au.AuditRecordID < dtCompleted.AuditRecordID)
                //        && (au.WFTempStep != null) && (au.WFTempStep.WFStepList.WFStepListDes == "Check-in")
                //            && (au.AUDITAction.AuditActionName == "WorkflowStepCompleted")).OrderBy(au => au.PerformedDateTime);
                //    dtGrade = temp.LastOrDefault();
                //}

                if ((dtCheckIn != null) && (dtGrade != null))
                {
                    if (dtGrade.PerformedDateTime < dtCheckIn.PerformedDateTime)
                        dtGrade = dtCheckIn;

                    var rcTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, null);
                    var dqeTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtGrade.PerformedDateTime, null);
                    var gradingTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, null);

                    cumAvgRCTAT += rcTAT;
                    cumAvgDQETAT += dqeTAT;
                    cumAvgGradingTAT += gradingTAT;
                    rm.countRC++;
                    rm.countDQE++;
                    rm.countGrading++;
                    if (rcTAT <= rm.targetRCTAT) rm.countTimelinessRC++;
                    if (dqeTAT <= rm.targetDQETAT) rm.countTimelinessDQE++;
                    if (gradingTAT <= rm.targetGradingTAT) rm.countTimelinessGrading++;
                }
                else if (dtGrade != null) //sometime series automatically generated
                {
                    var rcTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, null);
                    var gradingTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, null);

                    cumAvgRCTAT += rcTAT;
                    cumAvgGradingTAT += gradingTAT;
                    rm.countRC++;
                    rm.countGrading++;
                    if (rcTAT <= rm.targetRCTAT) rm.countTimelinessRC++;
                    if (gradingTAT <= rm.targetGradingTAT) rm.countTimelinessGrading++;
                }
                else if (dtCheckIn != null)
                {

                    var rcTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, null);
                    var dqeTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, null);

                    cumAvgRCTAT += rcTAT;
                    cumAvgDQETAT += dqeTAT;
                    rm.countRC++;
                    rm.countDQE++;
                    if (rcTAT <= rm.targetRCTAT) rm.countTimelinessRC++;
                    if (dqeTAT <= rm.targetDQETAT) rm.countTimelinessDQE++;
                }
            }

            rm.avgRCTAT = (rm.countRC > 0) ? cumAvgRCTAT / rm.countRC : double.NaN;
            rm.avgDQETAT = (rm.countDQE > 0) ? cumAvgDQETAT / rm.countDQE : double.NaN;
            rm.avgGradingTAT = (rm.countGrading > 0) ? cumAvgGradingTAT / rm.countGrading : double.NaN;
            rm.timelinessRC = (rm.countRC > 0) ? rm.countTimelinessRC / rm.countRC : double.NaN;
            rm.timelinessDQE = (rm.countDQE > 0) ? rm.countTimelinessDQE / rm.countDQE : double.NaN;
            rm.timelinessGrading = (rm.countGrading > 0) ? rm.countTimelinessGrading / rm.countGrading : double.NaN;

            return rm;
        }


        public System.Collections.Generic.List<gradingBacklog> GetGradingBacklog(long trialID)
        {
            var series = this.WF_Sequences.Where(w => w.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && w.IsActive && !w.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite
                && w.WFTempStep.WFStepList.WFStepListDes == "Grade").ToList();

            var today = DateTime.UtcNow;
            var backlog = series.Select(s => new gradingBacklog
            {
                modality = s.PACSTPProcList.CERTImgProcedureList.ImgProcedureDescription,
                seriesID = s.SeriesID,
                visit = s.PACSTimePoint.PACSTimePointsList.TimePointsDescription,
                stage = (s.LastStepCompletionDate.Value.Date >= today.AddDays(-15).Date) ? "Current" :
                ((s.LastStepCompletionDate.Value.Date >= today.AddDays(-30).Date) ? "15-30 Days" : ">30 Days")
            }).ToList();

            return backlog;
        }

        public double TotalBusinessDays(DateTime firstDay, DateTime lastDay, List<DateTime> usHolidays)
        {
            if (firstDay > lastDay)
                return 0;

            var totaldays = ((TimeSpan)(lastDay - firstDay)).TotalDays;
            var weekendsCount = 0;
            var holidayCount = 0;

            if (usHolidays == null)
                return Math.Round(totaldays);

            for (var date = firstDay; date < lastDay; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday
                    || date.DayOfWeek == DayOfWeek.Sunday)
                    weekendsCount++;
            }

            // subtract the number of bank holidays during the time interval
            foreach (DateTime holiday in usHolidays)
            {
                if (firstDay <= holiday && holiday <= lastDay)
                    holidayCount++;
            }

            totaldays = totaldays - weekendsCount - holidayCount;
            return (totaldays >= 0) ? totaldays : 0;
        }
        public List<DateTime> USHolidays(DateTime startDate, DateTime endDate)
        {
            var holidays = new List<DateTime>();

            for (var year = startDate.Year; year <= endDate.Year; year++)
            {
                //NEW YEARS 
                DateTime newYearsDate = AdjustForWeekendHoliday(new DateTime(year, 1, 1).Date);
                holidays.Add(newYearsDate);

                //MLK DAY  -- third monday in Jan 
                DateTime MLK_Day = new DateTime(year, 1, 15);
                while (MLK_Day.DayOfWeek != DayOfWeek.Monday)
                {
                    MLK_Day = MLK_Day.AddDays(1);
                }
                holidays.Add(MLK_Day.Date);

                //MEMORIAL DAY  -- last monday in May 
                DateTime memorialDay = new DateTime(year, 5, 31);
                DayOfWeek dayOfWeek = memorialDay.DayOfWeek;
                while (dayOfWeek != DayOfWeek.Monday)
                {
                    memorialDay = memorialDay.AddDays(-1);
                    dayOfWeek = memorialDay.DayOfWeek;
                }
                holidays.Add(memorialDay.Date);

                //INDEPENCENCE DAY 
                DateTime independenceDay = AdjustForWeekendHoliday(new DateTime(year, 7, 4).Date);
                holidays.Add(independenceDay);

                //LABOR DAY -- 1st Monday in September 
                DateTime laborDay = new DateTime(year, 9, 1);
                dayOfWeek = laborDay.DayOfWeek;
                while (dayOfWeek != DayOfWeek.Monday)
                {
                    laborDay = laborDay.AddDays(1);
                    dayOfWeek = laborDay.DayOfWeek;
                }
                holidays.Add(laborDay.Date);

                //THANKSGIVING DAY - 4th Thursday in November 
                var thanksgiving = (from day in Enumerable.Range(1, 30)
                                    where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                    select day).ElementAt(3);
                DateTime thanksgivingDay = new DateTime(year, 11, thanksgiving);
                holidays.Add(thanksgivingDay.Date);

                //CHRISTMAS EVE
                DateTime christmasEve = AdjustForWeekendHoliday(new DateTime(year, 12, 24).Date);
                holidays.Add(christmasEve);

                //CHRISTMAS DAY
                DateTime christmasDay = AdjustForWeekendHoliday(new DateTime(year, 12, 25).Date);
                holidays.Add(christmasDay);

                //NEW YEAR EVE
                DateTime newYearEve = AdjustForWeekendHoliday(new DateTime(year, 12, 31).Date);
                holidays.Add(newYearEve);
            }
            return holidays;
        }
        public static DateTime AdjustForWeekendHoliday(DateTime holiday)
        {
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }

        #endregion

        #region SitePerformance
        public SitePerformanceMetrics GetSitePerformanceMetrics(long trialID)
        {
            var spm = new SitePerformanceMetrics();

            var trial = this.PACS_Trials.Single(t => t.TrialID == trialID);
            spm.trialName = trial.TrialName;
            var trialStartDate = trial.TrialStartDate;
            if (trialStartDate == null) trialStartDate = new DateTime(2000, 1, 1);
            var usHolidays = USHolidays(trialStartDate.Value, DateTime.UtcNow);

            spm.targetSiteUploadTAT = (trial as PACS_TrialKeyMetric).TargetUploadTAT;

            var auditRecQ = this.AUDIT_Records.WithOption(new QueryOptions()
            {
                CommandTimeout = 300 //in seconds                    
            }).Where(ar => ar.TrialID == trialID
            && ar.PACSSeries != null
            && ar.PACSSeries.IsActive
            && ar.WFTempStep.WFStepList.WFStepListDes == "Upload"
            && ar.AUDITAction.AuditActionName == "WorkflowStepCompleted"
            && !ar.PACSSeries.QRY_Queries.Any());

            if (!trial.IsTestingPhase)
                auditRecQ = auditRecQ.Where(ar => !ar.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject && !ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);

            var auditRec = auditRecQ.ToList();

            var audUpload = auditRec.GroupBy(ar => ar.SeriesID, ar => ar, (key, elements) => elements.OrderBy(ar => ar.PerformedDateTime).First()).ToList();

            double cumSiteUploadTAT = 0.0;
            spm.countUpload = 0;
            spm.countTimelinessUpload = 0;
            foreach (var au in audUpload)
            {
                var uploadInfo = au.PACSSeries.UPLD_UploadInfos.FirstOrDefault();

                var uploadDate = (au.PACSSeries.StudyDate != null) ? au.PACSSeries.StudyDate :
                    (((uploadInfo != null) && (uploadInfo.PhotoDate != null)) ? uploadInfo.PhotoDate : null);

                if ((uploadDate != null) && (uploadDate <= au.PerformedDateTime))
                {
                    var uploadTAT = TotalBusinessDays(uploadDate.Value, au.PerformedDateTime.Value, usHolidays);
                    cumSiteUploadTAT += uploadTAT;
                    spm.countUpload++;

                    if (uploadTAT < spm.targetSiteUploadTAT)
                    {
                        spm.countTimelinessUpload++;
                    }
                }
            }
            spm.siteUploadTAT = (spm.countUpload > 0) ? cumSiteUploadTAT / spm.countUpload : double.NaN;
            spm.timelinessUpload = (spm.countUpload > 0) ? spm.countTimelinessUpload / spm.countUpload : double.NaN;


            spm.targetQueryTAT = (trial as PACS_TrialKeyMetric).TargetQueryTAT;
            var queryResolved = this.QRY_Queries.Where(q => q.TrialID == trialID
                                            && q.IsResolved
                                            && q.CertUserID == null && q.CertEquipmentID == null
                                            && q.PACSSeries != null);

            if (!trial.IsTestingPhase)
                queryResolved = queryResolved.Where(q => !q.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject && !q.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);

            var cumQueryTAT = 0.0;
            spm.countTimelinessquery = 0;
            foreach (var q in queryResolved)
            {
                var qTAT = TotalBusinessDays((DateTime)q.InitiateDate, q.DateResolved.Value, usHolidays);
                cumQueryTAT += qTAT;

                if (qTAT < spm.targetQueryTAT)
                    spm.countTimelinessquery++;
            }
            spm.totalQueryResolved = queryResolved.Count();
            spm.queryTAT = (spm.totalQueryResolved > 0) ? cumQueryTAT / spm.totalQueryResolved : double.NaN;
            spm.timelinessquery = (spm.totalQueryResolved > 0) ? spm.countTimelinessquery / spm.totalQueryResolved : double.NaN;


            var totalQueryQ = this.QRY_Queries.Where(q => q.TrialID == trialID && q.CertUserID == null && q.CertEquipmentID == null && q.PACSSeries != null);

            var totalSeriesQ = this.PACS_Series.Where(s => s.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID && s.IsActive);

            var countSuboptimalQ = this.PACS_Series.Where(s => s.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && !s.IsDataQualityAdequate && s.IsActive);

            if (!trial.IsTestingPhase)
            {
                totalQueryQ = totalQueryQ.Where(q => !q.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject && !q.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);
                totalSeriesQ = totalSeriesQ.Where(s => !s.PACSTimePoint.PACSSubject.IsTestingSubject && !s.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);
                countSuboptimalQ = countSuboptimalQ.Where(s => !s.PACSTimePoint.PACSSubject.IsTestingSubject && !s.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite);
            }

            spm.totalQuery = totalQueryQ.Count();
            spm.totalSeries = totalSeriesQ.Count();
            spm.countSuboptimal = countSuboptimalQ.Count();

            spm.queryRatio = (spm.totalSeries > 0) ? spm.totalQuery / spm.totalSeries : double.NaN;
            spm.targetQueryRatio = (trial as PACS_TrialKeyMetric).TargetQueryRatio;

            spm.subOptimalRatio = (spm.totalSeries > 0) ? spm.countSuboptimal / spm.totalSeries : double.NaN;
            spm.targetSubOptimalRatio = (trial as PACS_TrialKeyMetric).TargetPercentSuboptimalData;

            return spm;
        }

        #endregion

        #region TrialPerformanceReport
        public TpfTrialRecruitmentMetrics TpfGetTrialRecruitmentMetrics(long trialID)
        {
            var trm = new TpfTrialRecruitmentMetrics();

            var certUser = this.CERT_Users.Where(cu => (cu.CONTACTUserTrial.TrialID == trialID)
                && (cu.IsActive)).ToList();
            trm.userCertified = certUser.Count(cu => cu.IsCertified);
            trm.totalUsers = certUser.Count();
            trm.userCertifiedRatio = (trm.totalUsers != 0) ? ((double)trm.userCertified / trm.totalUsers) : double.NaN;

            var certEquipment = this.CERT_Equipments.Where(ce => (ce.TrialID == trialID)
                && (ce.IsActive)).ToList();
            trm.equipmentCertified = certEquipment.Count(ce => ce.IsCertified);
            trm.totalEquipment = certEquipment.Count();
            trm.equipmentCertifiedRatio = (trm.totalEquipment != 0) ? ((double)trm.equipmentCertified / trm.totalEquipment) : double.NaN;

            trm.subjectEnrolled = this.PACS_Subjects.Count(s => s.PACSSite.PACSTrial.TrialID == trialID && s.IsActive && !s.IsTestingSubject);
            trm.totalSubject = (int?)(this.PACS_Trials.Single(t => t.TrialID == trialID) as PACS_TrialKeyMetric).TargetSubjectsEnrolled;
            trm.subjectEnrollmentRatio = (trm.totalSubject != null) ? ((trm.subjectEnrolled >= trm.totalSubject) ? 1 : trm.subjectEnrolled / (double)trm.totalSubject) : double.NaN;

            trm.dataSeriesCollected = this.PACS_Series.Count(sr => sr.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                                && (sr.IsActive && (sr.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !sr.PACSTimePoint.PACSSubject.IsTestingSubject)));
            trm.totalDataSeries = (int?)(this.PACS_Trials.Single(t => t.TrialID == trialID) as PACS_TrialKeyMetric).TargetSeriesCollected;
            trm.dataSeriesRatio = (trm.totalDataSeries != null) ? ((trm.dataSeriesCollected >= trm.totalDataSeries) ? 1 : trm.dataSeriesCollected / (double)trm.totalDataSeries) : double.NaN;

            return trm;
        }
        public TpfRCPerformanceMetrics TpfGetRCPerformanceMetrics(long trialID)
        {
            var rm = new TpfRCPerformanceMetrics();

            var trialStartDate = this.PACS_Trials.Single(t => t.TrialID == trialID).TrialStartDate;
            if (trialStartDate == null) trialStartDate = new DateTime(2000, 1, 1);
            var usHolidays = TpfUSHolidays((DateTime)trialStartDate, DateTime.UtcNow);

            //Overall turn around metrics
            rm.targetRCTAT = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetTotalTAT;
            rm.targetDQETAT = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetCheckinTAT;
            rm.targetGradingTAT = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetGradingTAT;

            var auditRec = this.AUDIT_Records.Where(ar => ar.TrialID == trialID
                && (ar.WFTempStep.WFStepList.WFStepListDes == "Completed")
                && (!this.QRY_Queries.Any(q => q.SeriesID == ar.SeriesID))
                && (ar.PACSSeries.IsActive && (ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !ar.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject))).ToList();

            var audCompleted = auditRec.GroupBy(ar => ar.SeriesID, ar => ar, (key, elements) => elements.OrderBy(ar => ar.PerformedDateTime).First());

            var cumAvgRCTAT = 0.0;
            var cumAvgDQETAT = 0.0;
            var cumAvgGradingTAT = 0.0;
            rm.countRC = 0;
            rm.countGrading = 0;
            rm.countDQE = 0;
            rm.countTimelinessRC = 0;
            rm.countTimelinessGrading = 0;
            rm.countTimelinessDQE = 0;

            foreach (var dtCompleted in audCompleted)
            {
                var temp = this.AUDIT_Records.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                        && (au.WFTempStep.WFStepList.WFStepListDes == "Check-in")
                        && (au.AUDITAction.AuditActionName == "WorkflowStepInitiated")).OrderBy(au => au.PerformedDateTime);
                var dtCheckIn = temp.LastOrDefault();
                temp = this.AUDIT_Records.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                        && (au.WFTempStep.WFStepList.WFStepListDes == "Grade")
                        && (au.AUDITAction.AuditActionName == "WorkflowStepInitiated")).OrderBy(au => au.PerformedDateTime);
                var dtGrade = temp.LastOrDefault();

                if ((dtCheckIn != null) && (dtGrade != null))
                {
                    if (dtGrade.PerformedDateTime < dtCheckIn.PerformedDateTime)
                        dtGrade = dtCheckIn;

                    var rcTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);
                    var dqeTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtGrade.PerformedDateTime, usHolidays);
                    var gradingTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);

                    cumAvgRCTAT += rcTAT;
                    cumAvgDQETAT += dqeTAT;
                    cumAvgGradingTAT += gradingTAT;
                    rm.countRC++;
                    rm.countDQE++;
                    rm.countGrading++;
                    if (rcTAT <= rm.targetRCTAT) rm.countTimelinessRC++;
                    if (dqeTAT <= rm.targetDQETAT) rm.countTimelinessDQE++;
                    if (gradingTAT <= rm.targetGradingTAT) rm.countTimelinessGrading++;
                }
                else if (dtGrade != null) //sometime series automatically generated
                {
                    var rcTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);
                    var gradingTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);

                    cumAvgRCTAT += rcTAT;
                    cumAvgGradingTAT += gradingTAT;
                    rm.countRC++;
                    rm.countGrading++;
                    if (rcTAT <= rm.targetRCTAT) rm.countTimelinessRC++;
                    if (gradingTAT <= rm.targetGradingTAT) rm.countTimelinessGrading++;
                }
                else if (dtCheckIn != null)
                {

                    var rcTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);
                    var dqeTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);

                    cumAvgRCTAT += rcTAT;
                    cumAvgDQETAT += dqeTAT;
                    rm.countRC++;
                    rm.countDQE++;
                    if (rcTAT <= rm.targetRCTAT) rm.countTimelinessRC++;
                    if (dqeTAT <= rm.targetDQETAT) rm.countTimelinessDQE++;
                }
            }

            rm.avgRCTAT = (rm.countRC > 0) ? cumAvgRCTAT / rm.countRC : double.NaN;
            rm.avgDQETAT = (rm.countDQE > 0) ? cumAvgDQETAT / rm.countDQE : double.NaN;
            rm.avgGradingTAT = (rm.countGrading > 0) ? cumAvgGradingTAT / rm.countGrading : double.NaN;
            rm.timelinessRC = (rm.countRC > 0) ? rm.countTimelinessRC / rm.countRC : double.NaN;
            rm.timelinessDQE = (rm.countDQE > 0) ? rm.countTimelinessDQE / rm.countDQE : double.NaN;
            rm.timelinessGrading = (rm.countGrading > 0) ? rm.countTimelinessGrading / rm.countGrading : double.NaN;

            //Eligibility turn around metrics
            rm.targetRCEligibilityTAT = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetEligibilityTotalTAT;
            rm.targetDQEEligibilityTAT = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetEligibilityCheckinTAT;
            rm.targetGradingEligibilityTAT = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetEligibilityGradingTAT;

            var auditEligibilityRec = this.AUDIT_Records.Where(ar => ar.TrialID == trialID
                && (ar.WFTempStep.WFStepList.WFStepListDes == "Completed")
                && (!this.QRY_Queries.Any(q => q.SeriesID == ar.SeriesID))
                && (ar.PACSSeries.PACSTimePoint.PACSTimePointsList.IsEligibilityTimePoint)
                && (ar.PACSSeries.IsActive && (ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !ar.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject))).ToList();

            var audEligibilityCompleted = auditEligibilityRec.GroupBy(ar => ar.SeriesID, ar => ar, (key, elements) => elements.OrderBy(ar => ar.PerformedDateTime).First());

            var cumEligibilityRCTAT = 0.0;
            var cumEligibilityDQETAT = 0.0;
            var cumEligibilityGradingTAT = 0.0;
            rm.countEligibilityRC = 0;
            rm.countEligibilityGrading = 0;
            rm.countEligibilityDQE = 0;
            rm.countEligibilityTimelinessRC = 0;
            rm.countEligibilityTimelinessGrading = 0;
            rm.countEligibilityTimelinessDQE = 0;

            foreach (var dtCompleted in audEligibilityCompleted)
            {
                var temp = this.AUDIT_Records.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                        && (au.WFTempStep.WFStepList.WFStepListDes == "Check-in")
                        && (au.AUDITAction.AuditActionName == "WorkflowStepInitiated")).OrderBy(au => au.PerformedDateTime);
                var dtCheckIn = temp.LastOrDefault();
                temp = this.AUDIT_Records.Where(au => (au.SeriesID == dtCompleted.SeriesID)
                        && (au.WFTempStep.WFStepList.WFStepListDes == "Grade")
                        && (au.AUDITAction.AuditActionName == "WorkflowStepInitiated")).OrderBy(au => au.PerformedDateTime);
                var dtGrade = temp.LastOrDefault();

                if ((dtCheckIn != null) && (dtGrade != null))
                {
                    if (dtGrade.PerformedDateTime < dtCheckIn.PerformedDateTime)
                        dtGrade = dtCheckIn;

                    var rcEligibilityTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);
                    var dqeEligibilityTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtGrade.PerformedDateTime, usHolidays);
                    var gradingEligibilityTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);

                    cumEligibilityRCTAT += rcEligibilityTAT;
                    cumEligibilityDQETAT += dqeEligibilityTAT;
                    cumEligibilityGradingTAT += gradingEligibilityTAT;
                    rm.countEligibilityRC++;
                    rm.countEligibilityDQE++;
                    rm.countEligibilityGrading++;
                    if (rcEligibilityTAT <= rm.targetRCEligibilityTAT) rm.countEligibilityTimelinessRC++;
                    if (dqeEligibilityTAT <= rm.targetDQEEligibilityTAT) rm.countEligibilityTimelinessDQE++;
                    if (gradingEligibilityTAT <= rm.targetGradingEligibilityTAT) rm.countEligibilityTimelinessGrading++;
                }
                else if (dtGrade != null) //sometime series automatically generated
                {
                    var rcEligibilityTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);
                    var gradingEligibilityTAT = TotalBusinessDays((DateTime)dtGrade.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);

                    cumEligibilityRCTAT += rcEligibilityTAT;
                    cumEligibilityGradingTAT += gradingEligibilityTAT;
                    rm.countEligibilityRC++;
                    rm.countEligibilityGrading++;
                    if (rcEligibilityTAT <= rm.targetRCEligibilityTAT) rm.countEligibilityTimelinessRC++;
                    if (gradingEligibilityTAT <= rm.targetGradingEligibilityTAT) rm.countEligibilityTimelinessGrading++;
                }
                else if (dtCheckIn != null)
                {
                    var rcEligibilityTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);
                    var dqeEligibilityTAT = TotalBusinessDays((DateTime)dtCheckIn.PerformedDateTime, (DateTime)dtCompleted.PerformedDateTime, usHolidays);

                    cumEligibilityRCTAT += rcEligibilityTAT;
                    cumEligibilityDQETAT += dqeEligibilityTAT;
                    rm.countEligibilityRC++;
                    rm.countEligibilityDQE++;
                    if (rcEligibilityTAT <= rm.targetRCEligibilityTAT) rm.countEligibilityTimelinessRC++;
                    if (dqeEligibilityTAT <= rm.targetDQEEligibilityTAT) rm.countEligibilityTimelinessDQE++;
                }
            }

            rm.avgRCEligibilityTAT = (rm.countEligibilityRC > 0) ? cumEligibilityRCTAT / rm.countEligibilityRC : double.NaN;
            rm.avgDQEEligibilityTAT = (rm.countEligibilityDQE > 0) ? cumEligibilityDQETAT / rm.countEligibilityDQE : double.NaN;
            rm.avgGradingEligibilityTAT = (rm.countEligibilityGrading > 0) ? cumEligibilityGradingTAT / rm.countEligibilityGrading : double.NaN;
            rm.timelinessRCEligibility = (rm.countEligibilityRC > 0) ? rm.countEligibilityTimelinessRC / rm.countEligibilityRC : double.NaN;
            rm.timelinessDQEEligibility = (rm.countEligibilityDQE > 0) ? rm.countEligibilityTimelinessDQE / rm.countEligibilityDQE : double.NaN;
            rm.timelinessGradingEligibility = (rm.countEligibilityGrading > 0) ? rm.countEligibilityTimelinessGrading / rm.countEligibilityGrading : double.NaN;

            return rm;
        }
        public TpfSitePerformanceMetrics TpfGetSitePerformanceMetrics(long trialID)
        {
            var spm = new TpfSitePerformanceMetrics();

            var trialStartDate = this.PACS_Trials.Single(t => t.TrialID == trialID).TrialStartDate;
            if (trialStartDate == null) trialStartDate = new DateTime(2000, 1, 1);
            var usHolidays = TpfUSHolidays((DateTime)trialStartDate, DateTime.UtcNow);

            spm.targetSiteUploadTAT = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetUploadTAT;

            var auditRec = this.AUDIT_Records.Where(ar => ar.TrialID == trialID
                && (!this.QRY_Queries.Any(q => q.SeriesID == ar.SeriesID))
                && (ar.WFTempStep.WFStepList.WFStepListDes == "Upload")
                && (ar.AUDITAction.AuditActionName == "WorkflowStepCompleted")
                && (ar.PACSSeries.IsActive && (ar.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !ar.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject))).ToList();

            var audUpload = auditRec.GroupBy(ar => ar.SeriesID, ar => ar, (key, elements) => elements.OrderBy(ar => ar.PerformedDateTime).First());

            double cumSiteUploadTAT = 0.0;
            spm.countUpload = 0;
            spm.countTimelinessUpload = 0;
            foreach (var au in audUpload)
            {
                var uploadInfo = au.PACSSeries.UPLD_UploadInfos.FirstOrDefault();
                if ((uploadInfo != null) && (uploadInfo.PhotoDate != null) && (uploadInfo.PhotoDate <= au.PerformedDateTime))
                {
                    var uploadTAT = TotalBusinessDays((DateTime)uploadInfo.PhotoDate, (DateTime)au.PerformedDateTime, usHolidays);
                    cumSiteUploadTAT += uploadTAT;
                    spm.countUpload++;

                    if (uploadTAT < spm.targetSiteUploadTAT)
                    {
                        spm.countTimelinessUpload++;
                    }
                }
            }
            spm.siteUploadTAT = (spm.countUpload > 0) ? cumSiteUploadTAT / spm.countUpload : double.NaN;
            spm.timelinessUpload = (spm.countUpload > 0) ? spm.countTimelinessUpload / spm.countUpload : double.NaN;


            spm.targetQueryTAT = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetQueryTAT;
            var queryResolved = this.QRY_Queries.Where(q => q.TrialID == trialID
                                            && q.IsResolved
                                            && q.CertUserID == null && q.CertEquipmentID == null
                                            && (!q.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject)
                                            && (!q.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite));
            var cumQueryTAT = 0.0;
            spm.countTimelinessquery = 0;
            foreach (var q in queryResolved)
            {
                var qTAT = TotalBusinessDays((DateTime)q.InitiateDate, (DateTime)q.DateResolved, usHolidays);
                cumQueryTAT += qTAT;

                if (qTAT < spm.targetQueryTAT)
                    spm.countTimelinessquery++;
            }
            spm.totalQueryResolved = queryResolved.Count();
            spm.queryTAT = (spm.totalQueryResolved > 0) ? cumQueryTAT / spm.totalQueryResolved : double.NaN;
            spm.timelinessquery = (spm.totalQueryResolved > 0) ? spm.countTimelinessquery / spm.totalQueryResolved : double.NaN;


            spm.totalQuery = this.QRY_Queries.Count(q => q.TrialID == trialID && q.CertUserID == null && q.CertEquipmentID == null
                && (q.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !q.PACSSeries.PACSTimePoint.PACSSubject.IsTestingSubject)
                && (q.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !q.PACSSeries.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite));
            spm.totalSeries = this.PACS_Series.Count(s => s.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && (s.IsActive)
                && (s.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !s.PACSTimePoint.PACSSubject.IsTestingSubject)
                && (s.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !s.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite)
                );
            spm.queryRatio = (spm.totalSeries > 0) ? spm.totalQuery / spm.totalSeries : double.NaN;
            spm.targetQueryRatio = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetQueryRatio;

            spm.countSuboptimal = this.PACS_Series.Count(s => s.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                && (!s.IsDataQualityAdequate)
                && (s.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !s.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite)
                && (s.IsActive && (s.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.IsTestingPhase ? true : !s.PACSTimePoint.PACSSubject.IsTestingSubject)));

            spm.subOptimalRatio = (spm.totalSeries > 0) ? spm.countSuboptimal / spm.totalSeries : double.NaN;
            spm.targetSubOptimalRatio = (this.PACS_Trials.Single(ar => ar.TrialID == trialID) as PACS_TrialKeyMetric).TargetPercentSuboptimalData;

            return spm;
        }
        public List<DateTime> TpfUSHolidays(DateTime startDate, DateTime endDate)
        {
            var holidays = new List<DateTime>();

            for (var year = startDate.Year; year <= endDate.Year; year++)
            {
                //NEW YEARS 
                DateTime newYearsDate = TpfAdjustForWeekendHoliday(new DateTime(year, 1, 1).Date);
                holidays.Add(newYearsDate);

                //MLK DAY  -- third monday in Jan 
                DateTime MLK_Day = new DateTime(year, 1, 15);
                while (MLK_Day.DayOfWeek != DayOfWeek.Monday)
                {
                    MLK_Day = MLK_Day.AddDays(1);
                }
                holidays.Add(MLK_Day.Date);

                //MEMORIAL DAY  -- last monday in May 
                DateTime memorialDay = new DateTime(year, 5, 31);
                DayOfWeek dayOfWeek = memorialDay.DayOfWeek;
                while (dayOfWeek != DayOfWeek.Monday)
                {
                    memorialDay = memorialDay.AddDays(-1);
                    dayOfWeek = memorialDay.DayOfWeek;
                }
                holidays.Add(memorialDay.Date);

                //INDEPENCENCE DAY 
                DateTime independenceDay = TpfAdjustForWeekendHoliday(new DateTime(year, 7, 4).Date);
                holidays.Add(independenceDay);

                //LABOR DAY -- 1st Monday in September 
                DateTime laborDay = new DateTime(year, 9, 1);
                dayOfWeek = laborDay.DayOfWeek;
                while (dayOfWeek != DayOfWeek.Monday)
                {
                    laborDay = laborDay.AddDays(1);
                    dayOfWeek = laborDay.DayOfWeek;
                }
                holidays.Add(laborDay.Date);

                //THANKSGIVING DAY - 4th Thursday in November 
                var thanksgiving = (from day in Enumerable.Range(1, 30)
                                    where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                    select day).ElementAt(3);
                DateTime thanksgivingDay = new DateTime(year, 11, thanksgiving);
                holidays.Add(thanksgivingDay.Date);

                //CHRISTMAS EVE
                DateTime christmasEve = TpfAdjustForWeekendHoliday(new DateTime(year, 12, 24).Date);
                holidays.Add(christmasEve);

                //CHRISTMAS DAY
                DateTime christmasDay = TpfAdjustForWeekendHoliday(new DateTime(year, 12, 25).Date);
                holidays.Add(christmasDay);

                //NEW YEAR EVE
                DateTime newYearEve = TpfAdjustForWeekendHoliday(new DateTime(year, 12, 31).Date);
                holidays.Add(newYearEve);

            }

            return holidays;
        }
        public static DateTime TpfAdjustForWeekendHoliday(DateTime holiday)
        {
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }

        #endregion

        #region TrialRecruitmentProgress
        public TrialRecruitmentMetrics GetTrialRecruitmentMetrics(long trialID)
        {
            var trm = new TrialRecruitmentMetrics();
            var trial = this.PACS_TrialKeyMetrics.Single(t => t.TrialID == trialID);
            var testSite = this.PACS_Sites.Where(s => s.IsTestingSite && s.PACSTrial.TrialID == trialID);

            trm.trialName = trial.TrialName;

            var certUser = this.CERT_Users.Where(cu => (cu.CONTACTUserTrial.TrialID == trialID)
                && (cu.IsActive) && !testSite.Any(ts => ts.AffiliationID == cu.CONTACTUserTrial.CONTACTUser.AffiliationID));
            trm.userCertified = certUser.Count(cu => cu.IsCertified);
            trm.totalUsers = certUser.Count();
            trm.userCertifiedRatio = (trm.totalUsers != 0) ? ((double)trm.userCertified / trm.totalUsers) : double.NaN;

            var certEquipment = this.CERT_Equipments.Where(ce => (ce.TrialID == trialID)
                && (ce.IsActive) && !testSite.Any(ts => ts.AffiliationID == ce.CONTACTEquipment.AffiliationID));
            trm.equipmentCertified = certEquipment.Count(ce => ce.IsCertified);
            trm.totalEquipment = certEquipment.Count();
            trm.equipmentCertifiedRatio = (trm.totalEquipment != 0) ? ((double)trm.equipmentCertified / trm.totalEquipment) : double.NaN;

            var subjectEnrolledQ = this.PACS_Subjects.Where(s => s.PACSSite.PACSTrial.TrialID == trialID
                && s.IsActive);

            var dataSeriesCollectedQ = this.PACS_Series.Where(sr => sr.PACSTimePoint.PACSSubject.PACSSite.PACSTrial.TrialID == trialID
                                && sr.IsActive);

            if (!trial.IsTestingPhase)
            {
                subjectEnrolledQ = subjectEnrolledQ.Where(s => !s.PACSSite.IsTestingSite && !s.IsTestingSubject);
                dataSeriesCollectedQ = dataSeriesCollectedQ.Where(sr => !sr.PACSTimePoint.PACSSubject.PACSSite.IsTestingSite && !sr.PACSTimePoint.PACSSubject.IsTestingSubject);
            }

            trm.subjectEnrolled = subjectEnrolledQ.Count();
            trm.dataSeriesCollected = dataSeriesCollectedQ.Count();

            trm.totalSubject = (int?)trial.TargetSubjectsEnrolled;
            trm.subjectEnrollmentRatio = (trm.totalSubject != null) ? ((trm.subjectEnrolled >= trm.totalSubject) ? 1 : trm.subjectEnrolled / (double)trm.totalSubject) : double.NaN;
            trm.totalDataSeries = (int?)trial.TargetSeriesCollected;
            trm.dataSeriesRatio = (trm.totalDataSeries != null) ? ((trm.dataSeriesCollected >= trm.totalDataSeries) ? 1 : trm.dataSeriesCollected / (double)trm.totalDataSeries) : double.NaN;

            return trm;
        }

        #endregion

        #endregion

        #region Query

        #region QueryPrint
        public QueryReportQuery QueryPrintGetQuery(long queryID)
        {
            var q = QRY_Queries.Single(a => a.QueryID == queryID);

            var qp = new QueryReportQuery
            {
                trialName = q.PACSTrial.TrialName,
                sender = q.Sender.LastName + ", " + q.Sender.FirstName,
                receipent = q.Recipient.LastName + ", " + q.Recipient.FirstName,
                receipentEmail = q.Recipient.Email,
                subject = q.Subject,
                dateInitiated = string.Format("{0:yyyy-MM-dd}", q.InitiateDate),
                dateResolved = string.Format("{0:yyyy-MM-dd}", q.DateResolved)
            };

            if ((q.CertUserID == null) && (q.CertEquipmentID == null))
                qp.queryType = "Data Submission";
            else
            {
                if (q.CertEquipmentID == null)
                    qp.queryType = "Prestudy Technician Certification";
                else
                    qp.queryType = "Prestudy Equipment Certification";
            }

            return qp;
        }
        public System.Collections.Generic.List<QueryPrintQueryMessage> QueryPrintGetMessage(long queryID)
        {
            var qm = this.QRY_Messages.Where(q => q.QueryID == queryID).OrderBy(a => a.DateCreated).ToList();
            return qm.Select(qms => new QueryPrintQueryMessage
            {
                msgFrom = ((qms.SeqCount == null) || qms.IsRequest) ? (qms.QRYQuery.Sender.LastName + ", " + qms.QRYQuery.Sender.FirstName)
                                        : (qms.QRYQuery.Recipient.LastName + ", " + qms.QRYQuery.Recipient.FirstName),
                msgDate = qms.DateCreated.ToString(),
                msgBody = qms.MessageBody
            }).ToList();

        }

        #endregion

        #endregion

        #region Statistics

        #region TrialUserList
        public List<TrialUser> GetSponsorUserList(int trialID)
        {
            var siteUsers = this.CONTACT_UserTrials.Where(u => u.TrialID == trialID).Select(c => c.CONTACTUser).ToList();
            var sponsors = this.CONTACT_TrialSponsors.Where(u => u.TrialID == trialID).ToList();

            var result = siteUsers.Join(sponsors, u => u.AffiliationID, s => s.AffiliationID,
                (u, s) => new TrialUser
                {
                    userOrgName = u.CONTACTAffiliation.AffiliationName,
                    userName = u.LoweredUserName,
                    userEmail = u.LoweredEmail,
                    userStatus = u.IsActive ? "Yes" : "No",
                    userRole = u.AspnetRole.RoleName
                }).ToList();

            return result;
        }

        public List<TrialUser> GetRCUserList(int trialID)
        {
            var siteUsers = this.CONTACT_UserTrials.Where(u => u.TrialID == trialID).Select(c => c.CONTACTUser).ToList();
            var rcUsers = this.CONTACT_TrialReadingCenters.Where(u => u.TrialID == trialID)
                .GroupBy(u => u.AffiliationID, u => u, (key, elements) => elements.OrderBy(u => u.AffiliationID).First()).ToList();

            var result = siteUsers.Join(rcUsers, u => u.AffiliationID, s => s.AffiliationID,
                (u, s) => new TrialUser
                {
                    userOrgName = u.CONTACTAffiliation.AffiliationName,
                    userName = u.LoweredUserName,
                    userEmail = u.LoweredEmail,
                    userStatus = u.IsActive ? "Yes" : "No",
                    userRole = u.AspnetRole.RoleName
                }).ToList();

            return result;
        }

        public List<TrialUser> GetSiteUserList(int trialID)
        {
            var siteUsers = this.CONTACT_UserTrials.Where(u => u.TrialID == trialID).Select(c => c.CONTACTUser).ToList();
            var sites = this.PACS_Sites.Where(s => s.PACSTrial.TrialID == trialID && !s.IsTestingSite).ToList();

            var result = siteUsers.Join(sites, c => c.CONTACTAffiliation.AffiliationID, s => s.AffiliationID,
                (c, s) => new TrialUser
                {
                    userOrgName = c.CONTACTAffiliation.AffiliationName,
                    useOrgID = s.RandomizedSiteID,
                    userName = c.LoweredUserName,
                    userEmail = c.LoweredEmail,
                    userStatus = c.IsActive ? "Yes" : "No",
                    userRole = c.AspnetRole.RoleName
                }).ToList();

            return result;
        }

        public List<TrialEquipment> GetSiteEquipmentList(int trialID)
        {
            var certEquipment = this.CERT_Equipments.Where(cu => cu.TrialID == trialID && cu.IsActive);
            var sites = this.PACS_Sites.Where(s => s.PACSTrial.TrialID == trialID && !s.IsTestingSite);

            var result = certEquipment.Join(sites, ce => ce.CONTACTEquipment.CONTACTAffiliation.AffiliationID, s => s.AffiliationID,
                (ce, s) => new TrialEquipment
                {
                    siteName = ce.CONTACTEquipment.CONTACTAffiliation.AffiliationName,
                    siteID = s.RandomizedSiteID,
                    procedure = ce.CERTImgProcedureList.ImgProcedureDescription,
                    equipmentType = ce.CONTACTEquipment.CONTACTEquipmentModel.EquipmentType,
                    equipmentSerial = ce.CONTACTEquipment.MainSerialNum
                }).ToList();

            return result;
        }

        #endregion

        #region User
        public UserStat UserStatGetUser(long userID)
        {
            var uStat = new UserStat();

            var user = this.CONTACT_Users.Single(u => u.UserID == userID);
            uStat.userName = user.LastName + ", " + user.FirstName;
            uStat.userEmail = user.LoweredEmail;
            uStat.userRole = user.AspnetRole.RoleName;

            return uStat;
        }
        public System.Collections.Generic.List<string> GetUserTrials(long userID)
        {
            return this.CONTACT_UserTrials.Where(ut => ut.UserID == userID).Select(ut => ut.PACSTrial.TrialName).ToList();
        }
        public System.Collections.Generic.List<UserStatLogInOut> GetUserLogInOut(long userID, DateTime StartDate, DateTime EndDate)
        {
            var userLogInOut = this.AUDIT_Records.Where(a => a.UserID == userID
                && (a.AUDITAction.AuditActionName == "UserLogIn" || a.AUDITAction.AuditActionName == "UserLogOut")
                && (a.PerformedDateTime.Value.Date >= StartDate.Date && a.PerformedDateTime.Value.Date <= EndDate.Date)
                ).ToList();

            var uRec = new List<UserStatLogInOut>();

            var lg = userLogInOut.Count();
            for (var ii = 0; ii < lg; ii++)
            {
                var u = userLogInOut[ii];
                if (u.AUDITAction.AuditActionName == "UserLogIn")
                {
                    var inTime = (DateTime)u.PerformedDateTime;
                    TimeSpan inDur;

                    var jj = ii + 1;
                    if ((jj == lg)
                        || ((userLogInOut[jj].AUDITAction.AuditActionName == "UserLogIn")
                            && (userLogInOut[jj].PerformedDateTime - u.PerformedDateTime > TimeSpan.FromMinutes(90))))
                        inDur = TimeSpan.FromMinutes(30);
                    else
                        inDur = (TimeSpan)(userLogInOut[jj].PerformedDateTime - u.PerformedDateTime);

                    uRec.Add(new UserStatLogInOut
                    {
                        logInTime = inTime,
                        logInDuration = inDur
                    });
                }
            }

            return uRec;

        }

        #endregion

        #endregion

        #endregion
    }

    public interface IDataModelUnitOfWork : IUnitOfWork
    {
        IQueryable<PACS_Trial> PACS_Trials
        {
            get;
        }
        IQueryable<PACS_TPProcList> PACS_TPProcLists
        {
            get;
        }
        IQueryable<PACS_TimePointsList> PACS_TimePointsLists
        {
            get;
        }
        IQueryable<PACS_Site> PACS_Sites
        {
            get;
        }
        IQueryable<MEA_MeasurementType> MEA_MeasurementTypes
        {
            get;
        }
        IQueryable<CONTACT_User> CONTACT_Users
        {
            get;
        }
        IQueryable<CONTACT_TrialSponsor> CONTACT_TrialSponsors
        {
            get;
        }
        IQueryable<CONTACT_Equipment> CONTACT_Equipments
        {
            get;
        }
        IQueryable<CONTACT_EquipmentModel> CONTACT_EquipmentModels
        {
            get;
        }
        IQueryable<CONTACT_Affiliation> CONTACT_Affiliations
        {
            get;
        }
        IQueryable<CERT_ImgProcedureList> CERT_ImgProcedureLists
        {
            get;
        }
        IQueryable<Aspnet_User> Aspnet_Users
        {
            get;
        }
        IQueryable<Aspnet_Membership> Aspnet_Memberships
        {
            get;
        }
        IQueryable<CERT_Result> CERT_Results
        {
            get;
        }
        IQueryable<CERT_QuestionList> CERT_QuestionLists
        {
            get;
        }
        IQueryable<Aspnet_Role> Aspnet_Roles
        {
            get;
        }
        IQueryable<Aspnet_Application> Aspnet_Applications
        {
            get;
        }
        IQueryable<WF_TempStep> WF_TempSteps
        {
            get;
        }
        IQueryable<WF_Template> WF_Templates
        {
            get;
        }
        IQueryable<WF_StepList> WF_StepLists
        {
            get;
        }
        IQueryable<PACS_Series> PACS_Series
        {
            get;
        }
        IQueryable<WF_Sequence> WF_Sequences
        {
            get;
        }
        IQueryable<PACS_RawDatum> PACS_RawData
        {
            get;
        }
        IQueryable<PACS_DicomEPDF> PACS_DicomEPDFs
        {
            get;
        }
        IQueryable<PACS_DicomOP> PACS_DicomOPs
        {
            get;
        }
        IQueryable<PACS_DicomOPT> PACS_DicomOPTs
        {
            get;
        }
        IQueryable<CONTACT_UserTrial> CONTACT_UserTrials
        {
            get;
        }
        IQueryable<CERT_User> CERT_Users
        {
            get;
        }
        IQueryable<CERT_Equipment> CERT_Equipments
        {
            get;
        }
        IQueryable<UPLD_UploadInfo> UPLD_UploadInfos
        {
            get;
        }
        IQueryable<PACS_DataType> PACS_DataTypes
        {
            get;
        }
        IQueryable<PACS_DicomFrame> PACS_DicomFrames
        {
            get;
        }
        IQueryable<GRD_ReportResult> GRD_ReportResults
        {
            get;
        }
        IQueryable<GRD_QuestionTag> GRD_QuestionTags
        {
            get;
        }
        IQueryable<GRD_GradingQuestion> GRD_GradingQuestions
        {
            get;
        }
        IQueryable<GRD_GradingAnswer> GRD_GradingAnswers
        {
            get;
        }
        IQueryable<GRD_GradingTemplate> GRD_GradingTemplates
        {
            get;
        }
        IQueryable<GRD_QuestionGroup> GRD_QuestionGroups
        {
            get;
        }
        IQueryable<GRD_TempQuestion> GRD_TempQuestions
        {
            get;
        }
        IQueryable<GRD_Dependency> GRD_Dependencies
        {
            get;
        }
        IQueryable<QRY_Message> QRY_Messages
        {
            get;
        }
        IQueryable<MEA_MeasDataType> MEA_MeasDataTypes
        {
            get;
        }
        IQueryable<EXCELSIOR_SYSTEM> EXCELSIOR_SYSTEMs
        {
            get;
        }
        IQueryable<DOCU_AuthorizationType> DOCU_AuthorizationTypes
        {
            get;
        }
        IQueryable<MEA_OCTLayer> MEA_OCTLayers
        {
            get;
        }
        IQueryable<MEA_OCTGrid> MEA_OCTGrids
        {
            get;
        }
        IQueryable<MEA_ETDRSGrid> MEA_ETDRSGrids
        {
            get;
        }
        IQueryable<MEA_Distance> MEA_Distances
        {
            get;
        }
        IQueryable<MEA_Area> MEA_Areas
        {
            get;
        }
        IQueryable<MEA_Measurement> MEA_Measurements
        {
            get;
        }
        IQueryable<PACS_TrialKeyMetric> PACS_TrialKeyMetrics
        {
            get;
        }
        IQueryable<CONTACT_Country> CONTACT_Countries
        {
            get;
        }
        IQueryable<PACS_ProcessedDatum> PACS_ProcessedData
        {
            get;
        }
        IQueryable<WF_CategoryFlag> WF_CategoryFlags
        {
            get;
        }
        IQueryable<CONTACT_TrialReadingCenter> CONTACT_TrialReadingCenters
        {
            get;
        }
        IQueryable<AUDIT_Action> AUDIT_Actions
        {
            get;
        }
        IQueryable<AUDIT_CRUDAudit> AUDIT_CRUDAudits
        {
            get;
        }
        IQueryable<QRY_Query> QRY_Queries
        {
            get;
        }
        IQueryable<QRY_Status> QRY_Status
        {
            get;
        }
        IQueryable<GRD_Report> GRD_Reports
        {
            get;
        }
        IQueryable<DOCU_DocumentVersion> DOCU_DocumentVersions
        {
            get;
        }
        IQueryable<DOCU_DocumentGroup> DOCU_DocumentGroups
        {
            get;
        }
        IQueryable<DOCU_Document> DOCU_Documents
        {
            get;
        }
        IQueryable<DOCU_DocumentUser> DOCU_DocumentUsers
        {
            get;
        }
        IQueryable<DOCU_DocumentRole> DOCU_DocumentRoles
        {
            get;
        }
        IQueryable<PACS_Subject> PACS_Subjects
        {
            get;
        }
        IQueryable<PACS_TimePoint> PACS_TimePoints
        {
            get;
        }
        IQueryable<CERT_UploadInfo> CERT_UploadInfos
        {
            get;
        }
        IQueryable<AUDIT_Record> AUDIT_Records
        {
            get;
        }
        IQueryable<PACS_SeriesComment> PACS_SeriesComments
        {
            get;
        }
        IQueryable<PACS_SeriesAttachment> PACS_SeriesAttachments
        {
            get;
        }
        IQueryable<PACS_RawDataStatus> PACS_RawDataStatus
        {
            get;
        }
        IQueryable<DOCU_DocumentVersionUser> DOCU_DocumentVersionUsers
        {
            get;
        }
        IQueryable<PACS_DicomWSI> PACS_DicomWSIs
        {
            get;
        }
        IQueryable<CONTACT_UserNotification> CONTACT_UserNotifications
        {
            get;
        }
        IQueryable<CONTACT_Notification> CONTACT_Notifications
        {
            get;
        }
        IQueryable<CONTACT_NotificationRole> CONTACT_NotificationRoles
        {
            get;
        }
        IQueryable<PACS_SubjectGroup> PACS_SubjectGroups
        {
            get;
        }
        IQueryable<PACS_SubjectCohort> PACS_SubjectCohorts
        {
            get;
        }
        IQueryable<CONTACT_UserAffiliation> CONTACT_UserAffiliations
        {
            get;
        }
        IQueryable<MEA_Freehand> MEA_Freehands
        {
            get;
        }
        IQueryable<PACS_SeriesGroup> PACS_SeriesGroups
        {
            get;
        }
        IQueryable<RPT_TrialReport> RPT_TrialReports
        {
            get;
        }
        IQueryable<RPT_Report> RPT_Reports
        {
            get;
        }
        IQueryable<RPT_ReportCategory> RPT_ReportCategories
        {
            get;
        }
        IQueryable<RPT_TrialReportRole> RPT_TrialReportRoles
        {
            get;
        }
        IQueryable<BGD_JobStatus> BGD_JobStatus
        {
            get;
        }
        IQueryable<BGD_Job> BGD_Jobs
        {
            get;
        }
        IQueryable<MEA_DeltaVolume> MEA_DeltaVolumes
        {
            get;
        }
        IQueryable<CFG_AnimalSpecy> CFG_AnimalSpecies
        {
            get;
        }
        IQueryable<GRD_Impression> GRD_Impressions
        {
            get;
        }
        IQueryable<MEA_Stencil> MEA_Stencils
        {
            get;
        }

        #region CRF

        IQueryable<CRF_DataRELREC> CRF_DataRELRECs
        {
            get;
        }
        IQueryable<CRF_DataResult> CRF_DataResults
        {
            get;
        }
        IQueryable<CRF_Datum> CRF_Data
        {
            get;
        }
        IQueryable<CRF_AnswerType> CRF_AnswerTypes
        {
            get;
        }
        IQueryable<CRF_AnswerValidation> CRF_AnswerValidations
        {
            get;
        }
        IQueryable<CRF_TemplateGroup> CRF_TemplateGroups
        {
            get;
        }
        IQueryable<CRF_TemplateAnswer> CRF_TemplateAnswers
        {
            get;
        }
        IQueryable<CRF_TemplateDependency> CRF_TemplateDependencies
        {
            get;
        }
        IQueryable<CRF_TemplateDependencySource> CRF_TemplateDependencySources
        {
            get;
        }
        IQueryable<CRF_TemplateQuestion> CRF_TemplateQuestions
        {
            get;
        }
        IQueryable<CRF_TemplateQuestionTag> CRF_TemplateQuestionTags
        {
            get;
        }
        IQueryable<CRF_Template> CRF_Templates
        {
            get;
        }

        #endregion

        #region AUTH

        IQueryable<AUTH_Client> AUTH_Clients
        {
            get;
        }

        IQueryable<AUTH_ClientScope> AUTH_ClientScopes
        {
            get;
        }

        IQueryable<AUTH_ClientSecret> AUTH_ClientSecrets
        {
            get;
        }

        IQueryable<AUTH_Scope> AUTH_Scopes
        {
            get;
        }

        IQueryable<AUTH_ScopeClaim> AUTH_ScopeClaims
        {
            get;
        }

        #endregion
    }
}
#pragma warning restore 1591
