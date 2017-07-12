using Excelsior.API.Helpers;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class MediaController : Controller
    {
        public IMediaGateway Gateway { get; set; }
        public IUploadsHelper UploadsHelper { get; set; }

        public MediaController(IMediaGateway gateway, IUploadsHelper uploadsHelper)
        {
            Gateway = gateway;
            UploadsHelper = uploadsHelper;
        }

        /**
       * @api {get} api/v1/Media Get all Media
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup Media
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  }
       * @apiParam (Request Parameters) {Number}                                     seriesId=0 Series identifier
       * @apiParam (Request Parameters) {Number}                                     certUserId=0 CertUser identifier
       * @apiParam (Request Parameters) {Number}                                     certEquipmentId=0 CertEquipment identifier
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
       * @apiParam (Request Parameters) {String}                                     [search] Search text.
       * @apiSuccess {JSON} Result                                                   The paginated array of Media JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
                {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": [
                    {
                      "id": 0,
                      "dataTypeId": 0,
                      "dicomFileLocation": "10/21/10052/10123/10187/Check-Ins/Image.dcm",
                      "dicomInstanceUId": null,
                      "hasError": false,
                      "isActive": true,
                      "lastError": null,
                      "laterality": "",
                      "index": 1,
                      "seriesId": 0,
                      "statusId": 0,
                      "thumbImageLocation": "10/21/10052/10123/10187/Check-Ins/Image.jpg",
                      "segmentationStatus": "complete",
                      "status": {
                        "id": 5,
                        "name": "Ready"
                      },
                      "dicomOP": null,
                      "dicomOPT": {
                        "rawDataID": 0,
                        "pixelSpacingX": 0,
                        "pixelSpacingY":0,
                        "frameSpacing": 0,
                        "imageWidth": null,
                        "imageHeight": null,
                        "scanType": null,
                        "refImageCoveredArea": null,
                        "refDCMInstanceUID": null,
                        "refRawDataID": null
                      },
                      "dicomWSI": null,
                      "dicomEPDF": null
                    }
                  ],
                  "pager": {
                    "itemCount": 1,
                    "pageIndex": 1,
                    "pageSize": 10,
                    "pageCount": 1
                  }
                }
       *
       */
        [HttpGet]
        [Route("")]
        public IActionResult GetAll(long? seriesId, long? certUserId, long? certEquipmentId, string dataType, bool? isActive, int? page, int? pageSize, string filter, string search, string sort)
        {
            var request = new MediaRequestDto
            {
                SeriesId = seriesId,
                CertUserId = certUserId,
                CertEquipmentId = certEquipmentId,
                DataType = dataType,
                IsActive = isActive,
                Filter = filter,
                Page = page,
                PageSize = pageSize,
                Search = search,
                Sort = sort,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/Media/{id} Get Media by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup Media
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id Media identifier
        * @apiSuccess {JSON} Result The Media JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                               {
                      "id": 0,
                      "dataTypeId": 0,
                      "dicomFileLocation": "10/21/10052/10123/10187/Check-Ins/Image.dcm",
                      "dicomInstanceUId": null,
                      "hasError": false,
                      "isActive": true,
                      "lastError": null,
                      "laterality": "",
                      "index": 1,
                      "seriesId": 0,
                      "statusId": 0,
                      "thumbImageLocation": "10/21/10052/10123/10187/Check-Ins/Image.jpg",
                      "segmentationStatus": "complete",
                      "status": {
                        "id": 5,
                        "name": "Ready"
                      },
                      "dicomOP": null,
                      "dicomOPT": {
                        "rawDataID": 0,
                        "pixelSpacingX": 0,
                        "pixelSpacingY":0,
                        "frameSpacing": 0,
                        "imageWidth": null,
                        "imageHeight": null,
                        "scanType": null,
                        "refImageCoveredArea": null,
                        "refDCMInstanceUID": null,
                        "refRawDataID": null
                      },
                      "dicomWSI": null,
                      "dicomEPDF": null
                    }
              }
        *
        */
        [HttpGet("{id}")]
        [Route("{id}")]
        public IActionResult GetSingle(long id)
        {
            var result = Gateway.GetSingle(id);
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/Media Post Media
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Media
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Media
         * @apiParamExample {json} Request-Example:
                            {
                              "id": 0,
                              "dataTypeId": 0,
                              "dicomFileLocation": "10/21/10052/10123/10187/Check-Ins/Image.dcm",
                              "dicomInstanceUId": null,
                              "hasError": false,
                              "isActive": true,
                              "lastError": null,
                              "laterality": "",
                              "index": 1,
                              "seriesId": 0,
                              "statusId": 0,
                              "thumbImageLocation": "10/21/10052/10123/10187/Check-Ins/Image.jpg",
                              "segmentationStatus": "complete",
                              "status": {
                                "id": 5,
                                "name": "Ready"
                              },
                              "dicomOP": null,
                              "dicomOPT": {
                                "rawDataID": 0,
                                "pixelSpacingX": 0,
                                "pixelSpacingY":0,
                                "frameSpacing": 0,
                                "imageWidth": null,
                                "imageHeight": null,
                                "scanType": null,
                                "refImageCoveredArea": null,
                                "refDCMInstanceUID": null,
                                "refRawDataID": null
                              },
                              "dicomWSI": null,
                              "dicomEPDF": null
                            }
         * @apiSuccess {JSON} Result The Media JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": 
                            {
                              "id": 0,
                              "dataTypeId": 0,
                              "dicomFileLocation": "10/21/10052/10123/10187/Check-Ins/Image.dcm",
                              "dicomInstanceUId": null,
                              "hasError": false,
                              "isActive": true,
                              "lastError": null,
                              "laterality": "",
                              "index": 1,
                              "seriesId": 0,
                              "statusId": 0,
                              "thumbImageLocation": "10/21/10052/10123/10187/Check-Ins/Image.jpg",
                              "segmentationStatus": "complete",
                              "status": {
                                "id": 5,
                                "name": "Ready"
                              },
                              "dicomOP": null,
                              "dicomOPT": {
                                "rawDataID": 0,
                                "pixelSpacingX": 0,
                                "pixelSpacingY":0,
                                "frameSpacing": 0,
                                "imageWidth": null,
                                "imageHeight": null,
                                "scanType": null,
                                "refImageCoveredArea": null,
                                "refDCMInstanceUID": null,
                                "refRawDataID": null
                              },
                              "dicomWSI": null,
                              "dicomEPDF": null
                            }
                }
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                {
                  "isSuccess": false,
                  "message": "Invalid Model",
                  "exception": "",
                  "result": [
                                {
                                  "key": "Name",
                                  "ErrorMessage": "The Name field is required."
                                }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]MediaFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
                * @api {put} api/v1/Media/{id} Put Media
                * @apiName Put
                * @apiVersion 1.0.0
                * @apiGroup Media
                *
                * @apiHeader (Header) {String} Authorization Authorization Bearer token.
                * @apiHeaderExample Header Example
                *  {
                *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
                *       "fields": "index,laterality,dicominstanceuid,dicomfilelocation,thumbimagelocation,originalfilename,isactive,haserror,lasterror,seriesid,datatypeid,statusid" 
                *  }
                * @apiParam (Request Parameters) {Number}                                  Id Media identifier
                * @apiParam {JSON} Object modified of Media
                * @apiParamExample {json} Request-Example:
                                       {
                              "id": 0,
                              "dataTypeId": 0,
                              "dicomFileLocation": "10/21/10052/10123/10187/Check-Ins/Image.dcm",
                              "dicomInstanceUId": null,
                              "hasError": false,
                              "isActive": true,
                              "lastError": null,
                              "laterality": "",
                              "index": 1,
                              "seriesId": 0,
                              "statusId": 0,
                              "thumbImageLocation": "10/21/10052/10123/10187/Check-Ins/Image.jpg",
                              "segmentationStatus": "complete",
                              "status": {
                                "id": 5,
                                "name": "Ready"
                              },
                              "dicomOP": null,
                              "dicomOPT": {
                                "rawDataID": 0,
                                "pixelSpacingX": 0,
                                "pixelSpacingY":0,
                                "frameSpacing": 0,
                                "imageWidth": null,
                                "imageHeight": null,
                                "scanType": null,
                                "refImageCoveredArea": null,
                                "refDCMInstanceUID": null,
                                "refRawDataID": null
                              },
                              "dicomWSI": null,
                              "dicomEPDF": null
                            }
                * @apiSuccess {JSON} Result The Media JSON object.
                * @apiSuccessExample Success-Response
                *  HTTP/1.1 200 OK
                *       {
                        "IsSuccess": true,
                        "Message": "Successful",
                        "Exception": "Exception Message",
                        "Result": 
                                       {
                              "id": 0,
                              "dataTypeId": 0,
                              "dicomFileLocation": "10/21/10052/10123/10187/Check-Ins/Image.dcm",
                              "dicomInstanceUId": null,
                              "hasError": false,
                              "isActive": true,
                              "lastError": null,
                              "laterality": "",
                              "index": 1,
                              "seriesId": 0,
                              "statusId": 0,
                              "thumbImageLocation": "10/21/10052/10123/10187/Check-Ins/Image.jpg",
                              "segmentationStatus": "complete",
                              "status": {
                                "id": 5,
                                "name": "Ready"
                              },
                              "dicomOP": null,
                              "dicomOPT": {
                                "rawDataID": 0,
                                "pixelSpacingX": 0,
                                "pixelSpacingY":0,
                                "frameSpacing": 0,
                                "imageWidth": null,
                                "imageHeight": null,
                                "scanType": null,
                                "refImageCoveredArea": null,
                                "refDCMInstanceUID": null,
                                "refRawDataID": null
                              },
                              "dicomWSI": null,
                              "dicomEPDF": null
                            }
                      }
                 * @apiError BadRequest (400) The model state validation JSON object
                 * @apiErrorExample {json} Error example:
                 *     HTTP/1.1 400 Bad Request
                        {
                          "isSuccess": false,
                          "message": "Invalid Model",
                          "exception": "",
                          "result": [
                                        {
                                            "key": "DataTypeId",
                                            "ErrorMessage": "Error converting value 86477942313454789879898 to type 'System.long`"
                                        }
                          ],
                          "pager": null
                        } 

                *
                */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]MediaFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/Media/{id} Delete Media
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Media
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id Media identifier
         * @apiSuccess {JSON} Result The Template JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": true,
                    "Message": "successful"
                    "Exception": "Exception Message",
                    "Result": true
                }
         *
         */
        [HttpDelete("{id}")]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/media/storage Get Request path blob storage
        * @apiName GetMediaStorageUrl
        * @apiVersion 1.0.0
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
        [Route("storage")]
        public IActionResult GetMediaStorageUrl(bool isReadOnly = true)
        {
            var result = UploadsHelper.GetMediaStorageUrl(Request, isReadOnly);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/media/{id}/blocklist Post Commit file upload in blocks
        * @apiName PostBlockList
        * @apiVersion 1.0.0
        * @apiGroup Media
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {Number} Id Media identifier
        * @apiParam {JSON} Object
        * @apiParamExample {json} Request-Example:
           {
                "originalFileName": "file.ext",
                "blockList":
                    [
                        "YTM0NZomIzI2OTsmIzM0NTueYQ==",
                        "BLHGVZomIdI3OTvgIzM8UTueKG=="
                    ]
            }
        * @apiSuccess {JSON} Result The Media JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                    "IsSuccess": true,
                    "Message": "Successful",
                    "Exception": "Exception Message",
                    "Result": 
                    {
                        "id": 0,
                        "dataTypeId": 0,
                        "dicomFileLocation": "10/21/10052/10123/10187/Check-Ins/Image.dcm",
                        "dicomInstanceUId": null,
                        "hasError": false,
                        "isActive": true,
                        "lastError": null,
                        "laterality": "",
                        "index": 1,
                        "seriesId": 0,
                        "statusId": 0,
                        "thumbImageLocation": "10/21/10052/10123/10187/Check-Ins/Image.jpg",
                        "segmentationStatus": "complete",
                        "status": {
                            "id": 5,
                            "name": "Ready"
                        },
                        "dicomOP": null,
                        "dicomOPT": {
                            "rawDataID": 0,
                            "pixelSpacingX": 0,
                            "pixelSpacingY":0,
                            "frameSpacing": 0,
                            "imageWidth": null,
                            "imageHeight": null,
                            "scanType": null,
                            "refImageCoveredArea": null,
                            "refDCMInstanceUID": null,
                            "refRawDataID": null
                        },
                        "dicomWSI": null,
                        "dicomEPDF": null
                    }
                }
        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        [Route("{id}/blocklist")]
        public IActionResult SetBlockList(long id, [FromBody]BlockListRequestDto request)
        {
            var mediaResult = Gateway.GetSingle(id);
            if(!mediaResult.IsSuccess)
            {
                return new OkObjectResult(mediaResult);
            }
            var mediaDto = mediaResult.Result;
            var pblr = UploadsHelper.PutBlockList(mediaDto.DicomFileLocation, request).Result;
            mediaDto.DicomFileLocation = pblr.FileLocation;
            mediaDto.OriginalFileName = pblr.OriginalFileName;
            mediaDto.MediaStatusId = Gateway.GetMediaStatus("Uploaded").Id;
            var result = Gateway.Update(mediaDto);
            if (result.IsSuccess)
                UploadsHelper.SendMediaProcessingRequest(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/media/{id}/blocklist Get Get the uncommitted blocks
        * @apiName GetBlockList
        * @apiVersion 1.0.0
        * @apiGroup Media
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {Number} Id Media identifier
        * @apiParam (Request Parameters) {String}                             OriginalFileName Original File Name
        * @apiSuccess {JSON} Result The list of blocks JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                    "IsSuccess": true,
                    "Message": "Successful",
                    "Exception": "Exception Message",
                    "Result": 
                        [
                            {
                                "Index": 1,
                                "BlockId": "YTM0NZomIzI2OTsmIzM0NTueYQ=="
                            },
                            {
                                "Index": 2,
                                "BlockId": "BLHGVZomIdI3OTvgIzM8UTueKG=="
                            }
                        ]
                }
        *
        */
        [HttpGet]
        [Route("{id}/blocklist")]
        public IActionResult GetBlockList(long id, string orginalFileName)
        {
            var mediaResult = Gateway.GetSingle(id);
            if (!mediaResult.IsSuccess)
            {
                return new OkObjectResult(mediaResult);
            }
            var mediaDto = mediaResult.Result;
            var result = UploadsHelper.GetBlockList(orginalFileName, mediaDto.DicomFileLocation);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/media/{id}/process Post Process uploaded file
        * @apiName ProcessMedia
        * @apiVersion 1.0.0
        * @apiGroup Media
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {Number} Id Media identifier
        * @apiSuccess {JSON} Result The Media JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                    "IsSuccess": true,
                    "Message": "Successful",
                    "Exception": "Exception Message",
                    "Result": 
                    {
                        "id": 0,
                        "dataTypeId": 0,
                        "dicomFileLocation": "10/21/10052/10123/10187/Check-Ins/Image.dcm",
                        "dicomInstanceUId": null,
                        "hasError": false,
                        "isActive": true,
                        "lastError": null,
                        "laterality": "",
                        "index": 1,
                        "seriesId": 0,
                        "statusId": 0,
                        "thumbImageLocation": "10/21/10052/10123/10187/Check-Ins/Image.jpg",
                        "segmentationStatus": "complete",
                        "status": {
                            "id": 5,
                            "name": "Ready"
                        },
                        "dicomOP": null,
                        "dicomOPT": {
                            "rawDataID": 0,
                            "pixelSpacingX": 0,
                            "pixelSpacingY":0,
                            "frameSpacing": 0,
                            "imageWidth": null,
                            "imageHeight": null,
                            "scanType": null,
                            "refImageCoveredArea": null,
                            "refDCMInstanceUID": null,
                            "refRawDataID": null
                        },
                        "dicomWSI": null,
                        "dicomEPDF": null
                    }
                }
        *
        */
        [HttpGet]
        [Route("{id}/process")]
        public IActionResult ProcessMedia(long id)
        {
            var result = Gateway.GetSingle(id);
            if (!result.IsSuccess)
            {
                return new OkObjectResult(result);
            }
            //var mediaDto = mediaResult.Result;
            //var smpr = UploadsHelper.SetMediaProperties(id, mediaDto.DicomFileLocation, originalFileName);
            //if (!smpr.IsSuccess)
            //{
            //    return new OkObjectResult(smpr);
            //}
            //mediaDto.DicomFileLocation = smpr.Result;
            //mediaDto.OriginalFileName = originalFileName;
            //mediaDto.MediaStatusId = Gateway.GetMediaStatus("Uploaded").Id;
            //var result = Gateway.Update(mediaDto);
            //if(result.IsSuccess)
                UploadsHelper.SendMediaProcessingRequest(id);
            return new OkObjectResult(result);
        }
        
        /**
        * @api {post} api/v1/media/cors Post Configure CORS for the storage
        * @apiName CORS
        * @apiVersion 1.0.0
        * @apiGroup Media
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *
        */
        [HttpPost]
        [Route("cors")]
        public IActionResult ConfigureStorageCORS()
        {
            UploadsHelper.ConfigureStorageCORS();
            return new OkResult();
        }
    }
}