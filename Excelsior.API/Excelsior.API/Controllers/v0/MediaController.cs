using Excelsior.Business.DtoEntities;
using Excelsior.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.API.Controllers.v0
{
    [Authorize]
    [Route("api/v0/[controller]")]
    public class MediaController : Controller
    {
        public ISettings ConfigurationApi { get; set; }
        public MediaController(ISettings configurationApi)
        {
            ConfigurationApi = configurationApi;
        }


        /**
        * @api {get} api/v0/media/{id} Get Request path blob storage
        * @apiName GetMediaStorageUrl
        * @apiVersion 0.0.0
        * @apiGroup Media
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Boolean=true,false}          isReadOnly="true" Get only path for read?
        * @apiSuccess {JSON} Result                                    string path blob storage
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                         "IsSuccess": false,
                         "Message": "successful"
                         "Exception": "Error",
                         "Result": "http://sample.blob.core.windows.net/media-container"
                       }
        *
        */ 
        [HttpGet]
        [Route("getmediastorageurl")]
        public ResultInfo<string> GetMediaStorageUrl(bool isReadOnly)
        {
            ResultInfo<string> response = new ResultInfo<string>();
            try
            {
                string sas = null;
                var account = CloudStorageAccount.Parse(ConfigurationApi.GetSetting("StorageConnection"));
                //var account = CloudStorageAccount.FromConfigurationSetting("StorageConnection");//**Azure update
                var blobs = account.CreateCloudBlobClient();
                var container = blobs.GetContainerReference("media-container");

                if (!isReadOnly)
                {
                    //Container level access policy
                    //BlobContainerPermissions containerPermissions = new BlobContainerPermissions();
                    //containerPermissions.SharedAccessPolicies.Add("default", new SharedAccessPolicy()
                    //{
                    //    SharedAccessStartTime = DateTime.UtcNow,
                    //    SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                    //    Permissions = isReadOnly ? SharedAccessPermissions.Read : SharedAccessPermissions.Write
                    //});

                    //containerPermissions.PublicAccess = BlobContainerPublicAccessType.Off;
                    //container.SetPermissions(containerPermissions);

                    var sasExpirationTime = Convert.ToDouble(ConfigurationApi.GetSetting("SASExpirationTime"));
                    //sas = container.GetSharedAccessSignature(new SharedAccessPolicy()//**Azure update
                    sas = container.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                    {
                        SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(sasExpirationTime),
                        //SharedAccessStartTime = DateTime.UtcNow
                    }, ConfigurationApi.GetSetting("MediaContainerSharedKey"));
                }
                else
                {
                    var sasExpirationTime = Convert.ToDouble(ConfigurationApi.GetSetting("ReadOnlySASExpirationTime"));
                    //sas = container.GetSharedAccessSignature(new SharedAccessPolicy()//**Azure update
                    sas = container.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                    {
                        //Permissions = SharedAccessPermissions.Read,
                        SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(sasExpirationTime),
                        //SharedAccessStartTime = DateTime.UtcNow
                    }, ConfigurationApi.GetSetting("MediaContainerROSharedKey"));
                }

                UriBuilder uriBuilder = (new UriBuilder(container.Uri)
                {
                    Query = sas.TrimStart('?')
                });

                if (HttpContext.Request.IsHttps)
                {
                    uriBuilder.Scheme = "https";
                    uriBuilder.Port = 443;
                }
                else
                {
                    uriBuilder.Scheme = "http";
                    uriBuilder.Port = 80;
                }
                response.Result = uriBuilder.Uri.AbsoluteUri;
                response.IsSuccess = true;

            }
            catch (Exception e)
            {
                response.Message = "Error getting media storage url.";
                response.Exception = e.Message;
            }
            return response;
        }

    }
}
