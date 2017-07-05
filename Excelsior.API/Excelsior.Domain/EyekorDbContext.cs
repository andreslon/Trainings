namespace Excelsior.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EyekorDbContext : DbContext
    {
        public EyekorDbContext(string connectionString) : base(connectionString)
        {
        }

        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<AUDIT_Actions> AUDIT_Actions { get; set; }
        public virtual DbSet<AUDIT_CRUDAudits> AUDIT_CRUDAudits { get; set; }
        public virtual DbSet<AUDIT_Records> AUDIT_Records { get; set; }
        public virtual DbSet<AUTH_Client> AUTH_Client { get; set; }
        public virtual DbSet<AUTH_ClientScope> AUTH_ClientScope { get; set; }
        public virtual DbSet<AUTH_ClientSecret> AUTH_ClientSecret { get; set; }
        public virtual DbSet<AUTH_Scope> AUTH_Scope { get; set; }
        public virtual DbSet<AUTH_ScopeClaim> AUTH_ScopeClaim { get; set; }
        public virtual DbSet<BGD_Jobs> BGD_Jobs { get; set; }
        public virtual DbSet<BGD_JobStatus> BGD_JobStatus { get; set; }
        public virtual DbSet<CERT_Equipments> CERT_Equipments { get; set; }
        public virtual DbSet<CERT_ImgProcedureList> CERT_ImgProcedureList { get; set; }
        public virtual DbSet<CERT_QuestionList> CERT_QuestionList { get; set; }
        public virtual DbSet<CERT_Results> CERT_Results { get; set; }
        public virtual DbSet<CERT_UploadInfo> CERT_UploadInfo { get; set; }
        public virtual DbSet<CERT_Users> CERT_Users { get; set; }
        public virtual DbSet<CFG_AnimalSpecies> CFG_AnimalSpecies { get; set; }
        public virtual DbSet<CONTACT_Affiliations> CONTACT_Affiliations { get; set; }
        public virtual DbSet<CONTACT_Countries> CONTACT_Countries { get; set; }
        public virtual DbSet<CONTACT_EquipmentModel> CONTACT_EquipmentModel { get; set; }
        public virtual DbSet<CONTACT_Equipments> CONTACT_Equipments { get; set; }
        public virtual DbSet<CONTACT_NotificationRoles> CONTACT_NotificationRoles { get; set; }
        public virtual DbSet<CONTACT_Notifications> CONTACT_Notifications { get; set; }
        public virtual DbSet<CONTACT_TrialReadingCenters> CONTACT_TrialReadingCenters { get; set; }
        public virtual DbSet<CONTACT_TrialSponsors> CONTACT_TrialSponsors { get; set; }
        public virtual DbSet<CONTACT_UserAffiliations> CONTACT_UserAffiliations { get; set; }
        public virtual DbSet<CONTACT_UserNotifications> CONTACT_UserNotifications { get; set; }
        public virtual DbSet<CONTACT_Users> CONTACT_Users { get; set; }
        public virtual DbSet<CONTACT_UserTrial> CONTACT_UserTrial { get; set; }
        public virtual DbSet<CRF_Data> CRF_Data { get; set; }
        public virtual DbSet<CRF_DataRELREC> CRF_DataRELREC { get; set; }
        public virtual DbSet<CRF_DataResults> CRF_DataResults { get; set; }
        public virtual DbSet<CRF_LibraryAnswers> CRF_LibraryAnswers { get; set; }
        public virtual DbSet<CRF_LibraryAnswerTypes> CRF_LibraryAnswerTypes { get; set; }
        public virtual DbSet<CRF_LibraryDependencies> CRF_LibraryDependencies { get; set; }
        public virtual DbSet<CRF_LibraryDomains> CRF_LibraryDomains { get; set; }
        public virtual DbSet<CRF_LibraryQuestions> CRF_LibraryQuestions { get; set; }
        public virtual DbSet<CRF_LibraryQuestionTags> CRF_LibraryQuestionTags { get; set; }
        public virtual DbSet<CRF_TrialAnswers> CRF_TrialAnswers { get; set; }
        public virtual DbSet<CRF_TrialDependencies> CRF_TrialDependencies { get; set; }
        public virtual DbSet<CRF_TrialQuestions> CRF_TrialQuestions { get; set; }
        public virtual DbSet<CRF_TrialQuestionTags> CRF_TrialQuestionTags { get; set; }
        public virtual DbSet<CRF_TrialTemplates> CRF_TrialTemplates { get; set; }
        public virtual DbSet<DOCU_AuthorizationTypes> DOCU_AuthorizationTypes { get; set; }
        public virtual DbSet<DOCU_DocumentGroups> DOCU_DocumentGroups { get; set; }
        public virtual DbSet<DOCU_Documents> DOCU_Documents { get; set; }
        public virtual DbSet<DOCU_DocumentUsers> DOCU_DocumentUsers { get; set; }
        public virtual DbSet<DOCU_DocumentVersions> DOCU_DocumentVersions { get; set; }
        public virtual DbSet<DOCU_DocumentVersionUsers> DOCU_DocumentVersionUsers { get; set; }
        public virtual DbSet<EXCELSIOR_SYSTEM> EXCELSIOR_SYSTEM { get; set; }
        public virtual DbSet<GRD_Dependency> GRD_Dependencies { get; set; }
        public virtual DbSet<GRD_GradingAnswer> GRD_GradingAnswers { get; set; }
        public virtual DbSet<GRD_GradingQuestions> GRD_GradingQuestions { get; set; }
        public virtual DbSet<GRD_GradingTemplate> GRD_GradingTemplates { get; set; }
        public virtual DbSet<GRD_Impressions> GRD_Impressions { get; set; }
        public virtual DbSet<GRD_QuestionGroups> GRD_QuestionGroups { get; set; }
        public virtual DbSet<GRD_QuestionTags> GRD_QuestionTags { get; set; }
        public virtual DbSet<GRD_ReportResults> GRD_ReportResults { get; set; }
        public virtual DbSet<GRD_Reports> GRD_Reports { get; set; }
        public virtual DbSet<GRD_TempQuestion> GRD_TempQuestions { get; set; }
        public virtual DbSet<MEA_Area> MEA_Area { get; set; }
        public virtual DbSet<MEA_DeltaVolume> MEA_DeltaVolume { get; set; }
        public virtual DbSet<MEA_Distance> MEA_Distance { get; set; }
        public virtual DbSet<MEA_ETDRSGrid> MEA_ETDRSGrid { get; set; }
        public virtual DbSet<MEA_Freehand> MEA_Freehand { get; set; }
        public virtual DbSet<MEA_MeasDataTypes> MEA_MeasDataTypes { get; set; }
        public virtual DbSet<MEA_Measurements> MEA_Measurements { get; set; }
        public virtual DbSet<MEA_MeasurementTypes> MEA_MeasurementTypes { get; set; }
        public virtual DbSet<MEA_OCTGrid> MEA_OCTGrid { get; set; }
        public virtual DbSet<MEA_OCTLayer> MEA_OCTLayer { get; set; }
        public virtual DbSet<MEA_Stencils> MEA_Stencils { get; set; }
        public virtual DbSet<PACS_DataType> PACS_DataTypes { get; set; }
        public virtual DbSet<PACS_DicomEPDF> PACS_DicomEPDF { get; set; }
        public virtual DbSet<PACS_DicomFrame> PACS_DicomFrames { get; set; }
        public virtual DbSet<PACS_DicomOP> PACS_DicomOP { get; set; }
        public virtual DbSet<PACS_DicomOPT> PACS_DicomOPT { get; set; }
        public virtual DbSet<PACS_DicomWSI> PACS_DicomWSI { get; set; }
        public virtual DbSet<PACS_ProcessedData> PACS_ProcessedData { get; set; }
        public virtual DbSet<PACS_RawData> PACS_RawData { get; set; }
        public virtual DbSet<PACS_RawDataStatus> PACS_RawDataStatus { get; set; }
        public virtual DbSet<PACS_Series> PACS_Series { get; set; }
        public virtual DbSet<PACS_SeriesComments> PACS_SeriesComments { get; set; }
        public virtual DbSet<PACS_SeriesGroups> PACS_SeriesGroups { get; set; }
        public virtual DbSet<PACS_Site> PACS_Sites { get; set; }
        public virtual DbSet<PACS_SubjectCohorts> PACS_SubjectCohorts { get; set; }
        public virtual DbSet<PACS_SubjectGroups> PACS_SubjectGroups { get; set; }
        public virtual DbSet<PACS_Subject> PACS_Subjects { get; set; }
        public virtual DbSet<PACS_TimePoints> PACS_TimePoints { get; set; }
        public virtual DbSet<PACS_TimePointsList> PACS_TimePointsList { get; set; }
        public virtual DbSet<PACS_TPProcList> PACS_TPProcList { get; set; }
        public virtual DbSet<PACS_Trial> PACS_Trial { get; set; }
        public virtual DbSet<PACS_TrialKeyMetrics> PACS_TrialKeyMetrics { get; set; }
        public virtual DbSet<QRY_Messages> QRY_Messages { get; set; }
        public virtual DbSet<QRY_Query> QRY_Query { get; set; }
        public virtual DbSet<RPT_ReportCategories> RPT_ReportCategories { get; set; }
        public virtual DbSet<RPT_Reports> RPT_Reports { get; set; }
        public virtual DbSet<RPT_TrialReports> RPT_TrialReports { get; set; }
        public virtual DbSet<UPLD_UploadInfo> UPLD_UploadInfo { get; set; }
        public virtual DbSet<WF_CategoryFlags> WF_CategoryFlags { get; set; }
        public virtual DbSet<WF_Sequences> WF_Sequences { get; set; }
        public virtual DbSet<WF_StepList> WF_StepList { get; set; }
        public virtual DbSet<WF_Templates> WF_Templates { get; set; }
        public virtual DbSet<WF_TempSteps> WF_TempSteps { get; set; }
        public virtual DbSet<vw_aspnet_Applications> vw_aspnet_Applications { get; set; }
        public virtual DbSet<vw_aspnet_MembershipUsers> vw_aspnet_MembershipUsers { get; set; }
        public virtual DbSet<vw_aspnet_Profiles> vw_aspnet_Profiles { get; set; }
        public virtual DbSet<vw_aspnet_Roles> vw_aspnet_Roles { get; set; }
        public virtual DbSet<vw_aspnet_Users> vw_aspnet_Users { get; set; }
        public virtual DbSet<vw_aspnet_UsersInRoles> vw_aspnet_UsersInRoles { get; set; }
        public virtual DbSet<vw_aspnet_WebPartState_Paths> vw_aspnet_WebPartState_Paths { get; set; }
        public virtual DbSet<vw_aspnet_WebPartState_Shared> vw_aspnet_WebPartState_Shared { get; set; }
        public virtual DbSet<vw_aspnet_WebPartState_User> vw_aspnet_WebPartState_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Paths)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Roles)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Users)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Paths>()
                .HasOptional(e => e.aspnet_PersonalizationAllUsers)
                .WithRequired(e => e.aspnet_Paths);

            modelBuilder.Entity<aspnet_Roles>()
                .HasMany(e => e.aspnet_Users)
                .WithMany(e => e.aspnet_Roles)
                .Map(m => m.ToTable("aspnet_UsersInRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<aspnet_Roles>()
                .HasMany(e => e.RPT_TrialReports)
                .WithMany(e => e.aspnet_Roles)
                .Map(m => m.ToTable("RPT_TrialReportRoles").MapLeftKey("RoleId").MapRightKey("TrialReportID"));

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Profile)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.AUTH_Client)
                .WithRequired(e => e.aspnet_Users)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Users>()
                .HasMany(e => e.CONTACT_Users)
                .WithOptional(e => e.aspnet_Users)
                .HasForeignKey(e => e.AspUserID);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventSequence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventOccurrence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<AUDIT_CRUDAudits>()
                .Property(e => e.OldData)
                .IsUnicode(false);

            modelBuilder.Entity<AUDIT_CRUDAudits>()
                .Property(e => e.NewData)
                .IsUnicode(false);

            modelBuilder.Entity<AUTH_Client>()
                .HasMany(e => e.AUTH_ClientSecret)
                .WithRequired(e => e.AUTH_Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AUTH_Client>()
                .HasMany(e => e.AUTH_ClientScope)
                .WithRequired(e => e.AUTH_Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AUTH_Scope>()
                .HasMany(e => e.AUTH_ClientScope)
                .WithRequired(e => e.AUTH_Scope)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AUTH_Scope>()
                .HasMany(e => e.AUTH_ScopeClaim)
                .WithRequired(e => e.AUTH_Scope)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CERT_ImgProcedureList>()
                .Property(e => e.ImgProcedureName)
                .IsUnicode(false);

            modelBuilder.Entity<CERT_ImgProcedureList>()
                .Property(e => e.ImgProcedureDescription)
                .IsUnicode(false);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.AUDIT_Records)
                .WithOptional(e => e.CONTACT_Users)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.AUDIT_Records1)
                .WithOptional(e => e.CONTACT_Users1)
                .HasForeignKey(e => e.RelatedUserID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.CERT_Equipments)
                .WithOptional(e => e.CONTACT_Users)
                .HasForeignKey(e => e.CertifiedByID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.CERT_Users)
                .WithOptional(e => e.CONTACT_Users)
                .HasForeignKey(e => e.CertifiedByID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.CRF_Data)
                .WithOptional(e => e.CONTACT_Users)
                .HasForeignKey(e => e.VerifiedByID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.CRF_Data1)
                .WithOptional(e => e.CONTACT_Users1)
                .HasForeignKey(e => e.SignedByID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.GRD_Reports)
                .WithOptional(e => e.CONTACT_Users)
                .HasForeignKey(e => e.PerformedBy);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.PACS_Series)
                .WithOptional(e => e.CONTACT_Users)
                .HasForeignKey(e => e.PhotographerID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.QRY_Query)
                .WithOptional(e => e.CONTACT_Users)
                .HasForeignKey(e => e.SenderID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.QRY_Query1)
                .WithOptional(e => e.CONTACT_Users1)
                .HasForeignKey(e => e.ReceipientID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.UPLD_UploadInfo)
                .WithOptional(e => e.CONTACT_Users)
                .HasForeignKey(e => e.UploaderID);

            modelBuilder.Entity<CONTACT_Users>()
                .HasMany(e => e.WF_Sequences)
                .WithOptional(e => e.CONTACT_Users)
                .HasForeignKey(e => e.AssignedToID);

            modelBuilder.Entity<CRF_Data>()
                .HasMany(e => e.CRF_DataRELREC)
                .WithOptional(e => e.CRF_Data)
                .HasForeignKey(e => e.SourceCRFDataID);

            modelBuilder.Entity<CRF_Data>()
                .HasMany(e => e.CRF_DataRELREC1)
                .WithOptional(e => e.CRF_Data1)
                .HasForeignKey(e => e.TargetCRFDataID);

            modelBuilder.Entity<CRF_LibraryAnswers>()
                .HasMany(e => e.CRF_LibraryDependencies)
                .WithOptional(e => e.CRF_LibraryAnswers)
                .HasForeignKey(e => e.SourceAnswerID);

            modelBuilder.Entity<CRF_LibraryAnswers>()
                .HasMany(e => e.CRF_LibraryDependencies1)
                .WithOptional(e => e.CRF_LibraryAnswers1)
                .HasForeignKey(e => e.TargetAnswerID);

            modelBuilder.Entity<CRF_LibraryQuestions>()
                .HasMany(e => e.CRF_LibraryDependencies)
                .WithOptional(e => e.CRF_LibraryQuestions)
                .HasForeignKey(e => e.TargetQuestionID);

            modelBuilder.Entity<CRF_TrialAnswers>()
                .HasMany(e => e.CRF_TrialDependencies)
                .WithOptional(e => e.CRF_TrialAnswers)
                .HasForeignKey(e => e.SourceAnswerID);

            modelBuilder.Entity<CRF_TrialAnswers>()
                .HasMany(e => e.CRF_TrialDependencies1)
                .WithOptional(e => e.CRF_TrialAnswers1)
                .HasForeignKey(e => e.TargetAnswerID);

            modelBuilder.Entity<CRF_TrialQuestions>()
                .HasMany(e => e.CRF_TrialDependencies)
                .WithOptional(e => e.CRF_TrialQuestions)
                .HasForeignKey(e => e.TargetQuestionID);

            modelBuilder.Entity<DOCU_AuthorizationTypes>()
                .HasMany(e => e.DOCU_DocumentGroups)
                .WithOptional(e => e.DOCU_AuthorizationTypes)
                .HasForeignKey(e => e.DocuAuthorizationID);

            modelBuilder.Entity<GRD_GradingAnswer>()
                .HasMany(e => e.GRD_DependenciesGSourceAnswer)
                .WithOptional(e => e.GRD_GradingAnswers)
                .HasForeignKey(e => e.GSourceAnswerID);

            modelBuilder.Entity<GRD_GradingAnswer>()
                .HasMany(e => e.GRD_DependenciesGTargetAnswer)
                .WithOptional(e => e.GRD_GradingAnswers1)
                .HasForeignKey(e => e.GTargetAnswerID);

            modelBuilder.Entity<GRD_GradingQuestions>()
                .HasMany(e => e.GRD_Dependencies)
                .WithOptional(e => e.GRD_GradingQuestions)
                .HasForeignKey(e => e.GTargetQuestionID);

            modelBuilder.Entity<MEA_Measurements>()
                .HasMany(e => e.GRD_ReportResults)
                .WithOptional(e => e.MEA_Measurements)
                .HasForeignKey(e => e.GAnswerMeasurement);

            modelBuilder.Entity<MEA_Measurements>()
                .HasOptional(e => e.MEA_Area)
                .WithRequired(e => e.MEA_Measurements);

            modelBuilder.Entity<MEA_Measurements>()
                .HasOptional(e => e.MEA_DeltaVolume)
                .WithRequired(e => e.MEA_Measurements);

            modelBuilder.Entity<MEA_Measurements>()
                .HasOptional(e => e.MEA_Distance)
                .WithRequired(e => e.MEA_Measurements);

            modelBuilder.Entity<MEA_Measurements>()
                .HasOptional(e => e.MEA_ETDRSGrid)
                .WithRequired(e => e.MEA_Measurements);

            modelBuilder.Entity<MEA_Measurements>()
                .HasOptional(e => e.MEA_Freehand)
                .WithRequired(e => e.MEA_Measurements);

            modelBuilder.Entity<MEA_Measurements>()
                .HasOptional(e => e.MEA_OCTGrid)
                .WithRequired(e => e.MEA_Measurements);

            modelBuilder.Entity<MEA_Measurements>()
                .HasOptional(e => e.MEA_OCTLayer)
                .WithRequired(e => e.MEA_Measurements);

            modelBuilder.Entity<MEA_MeasurementTypes>()
                .Property(e => e.MeasurementType)
                .IsUnicode(false);

            modelBuilder.Entity<PACS_ProcessedData>()
                .Property(e => e.ProcessDataXMLString)
                .IsUnicode(false);

            modelBuilder.Entity<PACS_RawData>()
                .HasOptional(e => e.PACS_DicomEPDF)
                .WithRequired(e => e.PACS_RawData);

            modelBuilder.Entity<PACS_RawData>()
                .HasOptional(e => e.PACS_DicomOP)
                .WithRequired(e => e.PACS_RawData);

            modelBuilder.Entity<PACS_RawData>()
                .HasMany(e => e.PACS_DicomOPT)
                .WithOptional(e => e.PACS_RawData)
                .HasForeignKey(e => e.RefRawDataID);

            modelBuilder.Entity<PACS_RawData>()
                .HasOptional(e => e.PACS_DicomOPT1)
                .WithRequired(e => e.PACS_RawData1);

            modelBuilder.Entity<PACS_RawData>()
                .HasOptional(e => e.PACS_DicomWSI)
                .WithRequired(e => e.PACS_RawData);

            modelBuilder.Entity<PACS_Series>()
                .HasOptional(e => e.WFSequence)
                .WithRequired(e => e.PACS_Series);

            modelBuilder.Entity<PACS_TPProcList>()
                .HasMany(e => e.PACS_Series)
                .WithOptional(e => e.TPProcList)
                .HasForeignKey(e => e.TPProcListID);

            modelBuilder.Entity<PACS_Trial>()
                .HasOptional(e => e.PACS_TrialKeyMetrics)
                .WithRequired(e => e.PACS_Trial);

            modelBuilder.Entity<QRY_Messages>()
                .Property(e => e.MessageBody)
                .IsUnicode(false);

            modelBuilder.Entity<QRY_Query>()
                .Property(e => e.Subject)
                .IsUnicode(false);

            modelBuilder.Entity<WF_StepList>()
                .Property(e => e.WFStepListDes)
                .IsUnicode(false);
        }
    }
}
