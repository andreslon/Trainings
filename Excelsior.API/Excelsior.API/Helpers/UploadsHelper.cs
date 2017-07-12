using Excelsior.Business.DtoEntities;
using Excelsior.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Excelsior.API.Helpers
{
    public class UploadsHelper : IUploadsHelper
    {
        private ISettings Settings { get; set; }
        private CloudBlobClient StorageClient { get; set; }
        private CloudBlobContainer MediaContainer { get; set; }
        private CloudQueue ProcessMediaQueue { get; set; }

        public UploadsHelper(ISettings settings)
        {
            Settings = settings;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Settings.GetSetting("StorageConnection"));
            StorageClient = storageAccount.CreateCloudBlobClient();
            MediaContainer = StorageClient.GetContainerReference("media-container");
            CloudQueueClient queueStorage = storageAccount.CreateCloudQueueClient();
            ProcessMediaQueue = queueStorage.GetQueueReference("process-media");
            ProcessMediaQueue.CreateIfNotExists();
        }

        public void ConfigureStorageCORS()
        {
            var serviceProperties = StorageClient.GetServiceProperties();
            serviceProperties.Cors = new CorsProperties();
            serviceProperties.Cors.CorsRules.Add(new CorsRule()
            {
                AllowedHeaders = new List<string>() { "*" },
                AllowedMethods = CorsHttpMethods.Put | CorsHttpMethods.Get | CorsHttpMethods.Head | CorsHttpMethods.Post,
                AllowedOrigins = new List<string>() { "*" },
                ExposedHeaders = new List<string>() { "*" },
                MaxAgeInSeconds = 3600 // 60 minutes
            });

            StorageClient.SetServiceProperties(serviceProperties);
        }

        public ResultInfo<string> SetMediaProperties(long mediaId, string path, string originalFileName)
        {
            var result = new ResultInfo<string>();
            try
            {
                path = path.Substring(0, path.LastIndexOf("."));
                var ext = Path.GetExtension(originalFileName);
                path = string.Format("{0}{1}", path, ext);
                var blob = MediaContainer.GetBlockBlobReference(path);
                var contentType = "application/octet-stream";
                switch (ext.ToLower())
                {
                    case ".jpg":
                    case ".jepg":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".tiff":
                        contentType = "image/tiff";
                        break;
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    default:
                        break;
                }
                blob.Properties.ContentType = contentType;
                blob.SetProperties();
                result.Result = path;
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

        public ResultInfo<string> GetMediaStorageUrl(HttpRequest request, bool isReadOnly = true)
        {
            ResultInfo<string> response = new ResultInfo<string>();
            try
            {
                string sas = null;
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

                    var sasExpirationTime = Convert.ToDouble(Settings.GetSetting("SASExpirationTime"));
                    //sas = container.GetSharedAccessSignature(new SharedAccessPolicy()//**Azure update
                    sas = MediaContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                    {
                        SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(sasExpirationTime),
                        //SharedAccessStartTime = DateTime.UtcNow
                    }, Settings.GetSetting("MediaContainerSharedKey"));
                }
                else
                {
                    var sasExpirationTime = Convert.ToDouble(Settings.GetSetting("ReadOnlySASExpirationTime"));
                    //sas = container.GetSharedAccessSignature(new SharedAccessPolicy()//**Azure update
                    sas = MediaContainer.GetSharedAccessSignature(new SharedAccessBlobPolicy()
                    {
                        //Permissions = SharedAccessPermissions.Read,
                        SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(sasExpirationTime),
                        //SharedAccessStartTime = DateTime.UtcNow.ToUniversalTime()
                    }, Settings.GetSetting("MediaContainerROSharedKey"));
                }

                UriBuilder uriBuilder = (new UriBuilder(MediaContainer.Uri)
                {
                    Query = sas.TrimStart('?')
                });

                if (request.IsHttps)
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
        
        public void SendMediaProcessingRequest(long mediaId)
        {
            ProcessMediaQueue.AddMessage(new CloudQueueMessage(string.Format("{0}", mediaId)));
        }

        public ResultInfo<BlockListResponseDto> PutBlockList(string path, BlockListRequestDto request)
        {
            var result = new ResultInfo<BlockListResponseDto>();
            try
            {
                var fileName = request.OriginalFileName;
                path = path.Substring(0, path.LastIndexOf("."));
                var ext = Path.GetExtension(fileName);
                path = string.Format("{0}{1}", path, ext);
                var blob = MediaContainer.GetBlockBlobReference(path);
                blob.PutBlockList(request.BlockList);
                var contentType = "application/octet-stream";
                switch(ext.ToLower())
                {
                    case ".jpg":
                    case ".jepg":
                        contentType = "image/jpeg";
                        break;
                    case ".png":
                        contentType = "image/png";
                        break;
                    case ".tiff":
                        contentType = "image/tiff";
                        break;
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    default:
                        break;
                }
                blob.Properties.ContentType = contentType;
                blob.SetProperties();
                result.Result = new BlockListResponseDto()
                {
                    OriginalFileName = fileName,
                    FileLocation = path
                };
                result.IsSuccess = true;
            }
            catch(Exception ex)
            {
                result.Result = null;
                result.Exception = ex.Message;
                result.IsSuccess = false;
                result.Message = "Exception";
            }
            return result;
        }

        public ResultInfo<IList<BlockResponseDto>> GetBlockList(string orginalFileName, string path)
        {
            var result = new ResultInfo<IList<BlockResponseDto>>();

            try {
                path = path.Substring(0, path.LastIndexOf("."));
                path = string.Format("{0}{1}", path, Path.GetExtension(orginalFileName));
                var blob = MediaContainer.GetBlockBlobReference(path);
                var blobList = blob.DownloadBlockList(BlockListingFilter.Uncommitted);
                result.Result = blobList.Select(x => new BlockResponseDto()
                {
                    BlockId = x.Name,
                    Index = BitConverter.ToInt32(Convert.FromBase64String(x.Name), 0),
                    OriginalFileName = orginalFileName,
                    DicomFileLocation = path
                }).ToList();
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
    }
}
