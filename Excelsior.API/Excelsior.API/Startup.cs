using Excelsior.API.Helpers;
using Excelsior.API.OAuth.Extensions;
using Excelsior.API.OAuth.Services;
using Excelsior.Business.Gateways;
using Excelsior.Business.Gateways.Interfaces;
using Excelsior.Domain;
using Excelsior.Domain.Repositories;
using Excelsior.Domain.Repositories.Interface;
using Excelsior.Infrastructure.Interfaces;
using Excelsior.Infrastructure.Utilities;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Excelsior.API
{
    public class Startup
    {
        private IHostingEnvironment environment;
        private Settings setting;
        public IConfigurationRoot Configuration { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            environment = env;
            setting = new Settings();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var cert = new X509Certificate2(Path.Combine(environment.ContentRootPath, Path.Combine("Configurations", "idsvr3test.pfx")), "idsrv3test");

            var builder = services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/ui/login";
                options.UserInteraction.LogoutUrl = "/ui/logout";
                options.UserInteraction.ConsentUrl = "/ui/consent";
                options.UserInteraction.ErrorUrl = "/ui/error";
            })
            .AddInMemoryCaching()


            //.AddInMemoryClients(Clients.Get())
            .AddInMemoryApiResources(Resources.GetApiResources())
            .AddInMemoryIdentityResources(Resources.GetIdentityResources())
            //.AddInMemoryUsers(Users.Get());

            .AddSigningCredential(cert);

            builder.Services.AddScoped<IClientStore, ClientStore>();
            //builder.Services.AddScoped<IScopeStore, ScopeStore>();
            //builder.Services.AddScoped<IResourceStore, ResourceStore>();
            builder.Services.AddScoped<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            builder.Services.AddScoped<IProfileService, ProfileService>();


            //var x = setting.GetConnectionString("EyeKorConnection");
            services.AddScoped(p => new DataModel(setting));

            services.AddScoped<IResourceOwnerData, ResourceOwnerData>();
            //services.AddScoped<IDataService, DataService>();

            // Repositories
            services.AddScoped<IAuthClientRepository, AuthClientRepository>();
            services.AddScoped<IAuthScopeRepository, AuthScopeRepository>();
            services.AddScoped<IAuthUserRepository, AuthUserRepository>();
            services.AddScoped<IAspUserRepository, AspUserRepository>();
            services.AddScoped<IStudiesRepository, StudiesRepository>();
            services.AddScoped<IDocumentsRepository, DocucmentsRepository>();
            services.AddScoped<IAffiliationsRepository, AffiliationsRepository>();
            services.AddScoped<ISitesRepository, SitesRepository>();
            services.AddScoped<ISubjectsRepository, SubjectsRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();
            services.AddScoped<IGradingTemplatesRepository, GradingTemplatesRepository>();
            services.AddScoped<IVisitMatrixRepository, VisitMatrixRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IReportsRepository, GradingReportsRepository>();
            services.AddScoped<IAnswerTypesRepository, AnswerTypesRepository>();
            services.AddScoped<IAnswerValidationsRepository, AnswerValidationsRepository>();
            services.AddScoped<ITemplateAnswersRepository, TemplateAnswersRepository>();
            services.AddScoped<ITemplateDependenciesRepository, TemplateDependenciesRepository>();
            services.AddScoped<ITemplateGroupsRepository, TemplateGroupsRepository>();
            services.AddScoped<ITemplateQuestionsRepository, TemplateQuestionsRepository>();
            services.AddScoped<ITemplateQuestionTagsRepository, TemplateQuestionTagsRepository>();
            services.AddScoped<ITemplatesRepository, TemplatesRepository>();
            services.AddScoped<ITimePointsRepository, TimePointsRepository>();
            services.AddScoped<ISchedulesRepository, SchedulesRepository>();
            services.AddScoped<IMediaTypesRepository, MediaTypesRepository>();
            services.AddScoped<IWorkflowTemplatesRepository, WorkflowTemplatesRepository>();
            services.AddScoped<IWorkflowStepsRepository, WorkflowStepsRepository>();
            services.AddScoped<IAuditActionsRepository, AuditActionsRepository>();
            services.AddScoped<IAuditRecordsRepository, AuditRecordsRepository>();
            services.AddScoped<ISchedulingRepository, SchedulingRepository>();
            services.AddScoped<ICrfDataRepository, CrfDataRepository>();
            services.AddScoped<IVisitsRepository, VisitsRepository>();
            services.AddScoped<IFramesRepository, FramesRepository>();
            services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddScoped<IUploadsRepository, UploadsRepository>();
            services.AddScoped<ICertUploadsRepository, CertUploadsRepository>();
            services.AddScoped<ICertEquipmentRepository, CertEquipmentRepository>();
            services.AddScoped<ICertUsersRepository, CertUsersRepository>();
            services.AddScoped<ICertQuestionsRepository, CertQuestionsRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IEquipmentModelRepository, EquipmentModelRepository>();
            services.AddScoped<IStudyUserRepository, StudyUserRepository>();
            services.AddScoped<IColorCategoriesRepository, ColorCategoriesRepository>();
            services.AddScoped<IProcessedDataRepository, ProcessedDataRepository>();
            services.AddScoped<IDocumentVersionsRepository, DocumentVersionsRepository>();
            services.AddScoped<IDocumentRolesRepository, DocucmentRolesRepository>();
            services.AddScoped<IDocumentGroupsRepository, DocumentGroupsRepository>();
            services.AddScoped<IProceduresRepository, ProceduresRepository>();
            services.AddScoped<IQueriesRepository, QueriesRepository>();
            services.AddScoped<IQueryMessagesRepository, QueryMessagesRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IAnimalSpeciesRepository, AnimalSpeciesRepository>();
            services.AddScoped<ISubjectCohortsRepository, SubjectCohortsRepository>();
            services.AddScoped<ISubjectGroupsRepository, SubjectGroupsRepository>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
            services.AddScoped<IStencilsRepository, StencilsRepository>();
            services.AddScoped<IQueriesRepository, QueriesRepository>();
            services.AddScoped<IQueryMessagesRepository, QueryMessagesRepository>();
            services.AddScoped<IStudyReportsRepository, StudyReportsRepository>();
            services.AddScoped<ISystemRepository, SystemRepository>();
            services.AddScoped<IUserNotificationsRepository, UserNotificationsRepository>();
            services.AddScoped<IMediaStatusRepository, MediaStatusRepository>();

            //Gateways
            services.AddScoped<ICommonGateway, CommonGateway>();
            services.AddScoped<IAuthClientGateway, AuthClientGateway>();
            services.AddScoped<IAccountGateway, AccountGateway>();
            services.AddScoped<IAuthScopeGateway, AuthScopeGateway>();
            services.AddScoped<IAuthUserGateway, AuthUserGateway>();
            services.AddScoped<IDocumentsGateway, DocumentsGateway>();
            services.AddScoped<IDocumentVersionsGateway, DocumentVersionsGateway>();
            services.AddScoped<IDocumentRolesGateway, DocumentRolesGateway>();
            services.AddScoped<IRolesGateway, RolesGateway>();
            services.AddScoped<IStudiesGateway, StudiesGateway>();
            services.AddScoped<IAffiliationsGateway, AffiliationsGateway>();
            services.AddScoped<ISitesGateway, SitesGateway>();
            services.AddScoped<ISubjectsGateway, SubjectsGateway>();
            services.AddScoped<IGradingTemplatesGateway, GradingTemplatesGateway>();
            services.AddScoped<ISeriesGateway, SeriesGateway>();
            services.AddScoped<IVisitMatrixGateway, VisitMatrixGateway>();
            services.AddScoped<IEDCGateway, EDCGateway>();
            services.AddScoped<IUsersGateway, UsersGateway>();
            services.AddScoped<IGradingReportsGateway, GradingReportsGateway>();
            services.AddScoped<IAnswerTypesGateway, AnswerTypesGateway>();
            services.AddScoped<IAnswerValidationsGateway, AnswerValidationsGateway>();
            services.AddScoped<ITemplateAnswersGateway, TemplateAnswersGateway>();
            services.AddScoped<ITemplateDependenciesGateway, TemplateDependenciesGateway>();
            services.AddScoped<ITemplateGroupsGateway, TemplateGroupsGateway>();
            services.AddScoped<ITemplateQuestionsGateway, TemplateQuestionsGateway>();
            services.AddScoped<ITemplateQuestionTagsGateway, TemplateQuestionTagsGateway>();
            services.AddScoped<ITemplatesGateway, TemplatesGateway>();
            services.AddScoped<ITimePointsGateway, TimePointsGateway>();
            services.AddScoped<IProceduresGateway, ProceduresGateway>();
            services.AddScoped<ISchedulesGateway, SchedulesGateway>();
            services.AddScoped<IMediaTypesGateway, MediaTypesGateway>();
            services.AddScoped<IWorkflowTemplatesGateway, WorkflowTemplatesGateway>();
            services.AddScoped<IWorkflowStepsGateway, WorkflowStepsGateway>();
            services.AddScoped<ISchedulingGateway, SchedulingGateway>();
            services.AddScoped<ICrfDataGateway, CrfDataGateway>();
            services.AddScoped<IFramesGateway, FramesGateway>();
            services.AddScoped<IMediaGateway, MediaGateway>();
            services.AddScoped<IUploadsGateway, UploadsGateway>();
            services.AddScoped<ICertUploadsGateway, CertUploadsGateway>();
            services.AddScoped<ICertEquipmentGateway, CertEquipmentGateway>();
            services.AddScoped<ICertUsersGateway, CertUsersGateway>();
            services.AddScoped<ICertQuestionsGateway, CertQuestionsGateway>();
            services.AddScoped<IEquipmentGateway, EquipmentGateway>();
            services.AddScoped<IEquipmentModelGateway, EquipmentModelGateway>();
            services.AddScoped<IStudyUserGateway, StudyUserGateway>();
            services.AddScoped<IColorCategoriesGateway, ColorCategoriesGateway>();
            services.AddScoped<IProcessedDataGateway, ProcessedDataGateway>();
            services.AddScoped<IAttachmentsGateway, AttachmentsGateway>();
            services.AddScoped<IStencilsGateway, StencilsGateway>();
            services.AddScoped<IQueriesGateway, QueriesGateway>();
            services.AddScoped<IQueryMessagesGateway, QueryMessagesGateway>();
            services.AddScoped<IStudyReportsGateway, StudyReportsGateway>();
            services.AddScoped<IUserNotificationsGateway, UserNotificationsGateway>();
            services.AddScoped<IMediaStatusGateway, MediaStatusGateway>();

            //Helpers
            services.AddScoped<IUploadsHelper, UploadsHelper>();

            services.AddScoped<ValidateModelAttribute>();
            
            // for the UI
            services
                .AddMvc(options =>
                {
                    //options.Filters.Add(new RequireHttpsAttribute());
                    //options.Filters.Add(new ValidateModelAttribute()); // an instance
                })
                .AddRazorOptions(razor =>
                {
                    razor.ViewLocationExpanders.Add(new Host.UI.CustomViewLocationExpander());
                });

            //services.AddTransient<Host.UI.Login.LoginService>();

            services.AddSingleton<ISettings>(setting);
            services.AddSingleton<IStringResources, StringResources>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.Use(async (context, next) =>
            {
                if (context.Request.Method == "OPTIONS")
                    context.Response.StatusCode = (int)HttpStatusCode.OK;

                var headers = context.Response.Headers;
                if (headers.ContainsKey("Access-Control-Allow-Origin"))
                {
                    headers["Access-Control-Allow-Origin"] = string.Join(",", context.Request.Headers["Origin"].Select(x => x.Substring(0, x.Length)));
                }
                else
                {
                    context.Response.Headers.Append("Access-Control-Allow-Origin", string.Join(",", context.Request.Headers["Origin"].Select(x => x.Substring(0, x.Length))));
                }
                if (headers.ContainsKey("Access-Control-Allow-Headers"))
                {
                    headers["Access-Control-Allow-Headers"] = "Origin, Content-Type, Accept, Client, Authorization, X-Auth-Token, X-Requested-With";
                }
                else
                {
                    context.Response.Headers.Append("Access-Control-Allow-Headers", "Origin, Content-Type, Accept, Client, Authorization, X-Auth-Token, X-Requested-With");
                }
                if (headers.ContainsKey("Access-Control-Allow-Methods"))
                {
                    headers["Access-Control-Allow-Credentials"] = "GET, POST, PATCH, PUT, DELETE, OPTIONS";
                }
                else
                {
                    context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS");
                }
                if (headers.ContainsKey("Access-Control-Allow-Credentials"))
                {
                    headers["Access-Control-Allow-Credentials"] = "true";
                }
                else
                {
                    context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
                }

                context.Response.Headers.Append("Access-Control-Expose-Headers", "X-Auth-Token");
                context.Response.Headers.Append("Vary", "Origin");

                await next();
            });

            Func<string, LogLevel, bool> filter = (scope, level) =>
                scope.StartsWith("IdentityServer") ||
                scope.StartsWith("IdentityModel") ||
                level == LogLevel.Error ||
                level == LogLevel.Critical;

            loggerFactory.AddConsole(filter);
            loggerFactory.AddDebug(filter);

            //var options = new RewriteOptions()
            //       .AddRedirectToHttps();

            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationScheme = "Temp",
            //    AutomaticAuthenticate = false,
            //    AutomaticChallenge = false,
                

            //});

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            string apiUrl = setting.GetSetting("ApiUrl");

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = apiUrl,
                //ScopeName = "webapi",
                //ScopeSecret = "webapiSecret",
                //AllowedScopes= new List<string> {
                //    "webapi"
                //},
                //AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                
            });

            app.UseIdentityServer();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            //app.UseWelcomePage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
