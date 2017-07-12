using Excelsior.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Cache.StackExchangeRedis;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;

namespace Excelsior.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class ReportsController : ReportsControllerBase
    {
        private ISettings Settings { get; set; }

        public ReportsController(IHostingEnvironment environment, ISettings settings)
        {
            //var reportsPath =
            //   Path.Combine(environment.WebRootPath, "Reports");

            this.ReportServiceConfiguration =
               new ReportServiceConfiguration
               {
                   HostAppId = "excelsior-api",
                   ReportResolver = new ReportTypeResolver(),
                   //ReportResolver = new ReportFileResolver(reportsPath),
                   // ReportSharingTimeout = 0,
                   // ClientSessionTimeout = 15,
               };

            switch(settings.GetSetting("ReportsCacheType"))
            {
                case "Redis":
                    var options = new ConfigurationOptions();
                    options.EndPoints.Add(settings.GetSetting("ReportsCacheEndpoint"));
                    options.Ssl = true;
                    options.ConnectRetry = 3;
                    options.ConnectTimeout = 15;
                    options.Password = settings.GetSetting("ReportsCacheKey");
                    var connection = ConnectionMultiplexer.Connect(options);
                    ReportServiceConfiguration.Storage = new RedisStorage(connection);
                    break;
                default:
                    ReportServiceConfiguration.Storage = new FileStorage();
                    break;
            }
        }
    }
}