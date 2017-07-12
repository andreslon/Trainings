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
    public class UploadsController : Controller
    {
        public IUploadsGateway Gateway { get; set; }
        public IUploadsHelper UploadsHelper { get; set; }

        public UploadsController(IUploadsGateway gateway, IUploadsHelper uploadsHelper)
        {
            Gateway = gateway;
            UploadsHelper = uploadsHelper;
        }

        /**
       * @api {get} api/v1/Uploads Get all Uploads
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup Uploads
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  }
       * @apiParam (Request Parameters) {Number}                                     seriesId=0 Series identifier
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
       * @apiParam (Request Parameters) {String}                                     [search] Search text.
       * @apiSuccess {JSON} Result                                                   The paginated array of Upload JSON objects.
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
        public IActionResult GetAll(long? seriesId, bool? isActive, int? page, int? pageSize, string filter, string search, string sort)
        {
            var request = new UploadsRequestDto
            {
                SeriesId = seriesId,
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
        * @api {get} api/v1/Uploads/{id} Get Upload by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup Uploads
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id Upload identifier
        * @apiSuccess {JSON} Result The Upload JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                    {
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
         * @api {post} api/v1/Uploads Post Upload
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Uploads
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Upload
         * @apiParamExample {json} Request-Example:
                            {
                            }
         * @apiSuccess {JSON} Result The Upload JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                    "IsSuccess": false,
                    "Message": "successful"
                    "Exception": "Error",
                    "Result": 
                            {
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
        public IActionResult Post([FromBody]UploadFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/Uploads/{id} Put Upload
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup Uploads
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "UploadDate,AcquisitionDate,DataFileLocation,Note,IsActive,SeriesId,UploaderId,MediaStatusId" 
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id Upload identifier
        * @apiParam {JSON} Object modified of Upload
        * @apiParamExample {json} Request-Example:
                    {
                    }
        * @apiSuccess {JSON} Result The Upload JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                "IsSuccess": true,
                "Message": "Successful",
                "Exception": "Exception Message",
                "Result": 
                    {

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
        public IActionResult Put(int id, [FromBody]UploadFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/Uploads/{id} Delete Upload
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Uploads
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id Upload identifier
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
        * @api {get} api/v1/upload/storage Get Request path blob storage
        * @apiName GetUploadStorageUrl
        * @apiVersion 1.0.0
        * @apiGroup Uploads
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
        public IActionResult GetUploadStorageUrl(bool isReadOnly = true)
        {
            var result = UploadsHelper.GetMediaStorageUrl(Request, isReadOnly);
            return new OkObjectResult(result);
        }

        /**
        * @api {post} api/v1/uploads/{id}/blocklist Post Commit file upload in blocks
        * @apiName PostBlockList
        * @apiVersion 1.0.0
        * @apiGroup Uploads
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {Number} Id Upload identifier
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
        * @apiSuccess {JSON} Result The Upload JSON object.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                    "IsSuccess": true,
                    "Message": "Successful",
                    "Exception": "Exception Message",
                    "Result": 
                    {
                    }
                }
        *
        */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        [Route("{id}/blocklist")]
        public IActionResult SetBlockList(long id, [FromBody]BlockListRequestDto request)
        {
            var uploadResult = Gateway.GetSingle(id);
            if(!uploadResult.IsSuccess)
            {
                return new OkObjectResult(uploadResult);
            }
            var uploadDto = uploadResult.Result;
            var pblr = UploadsHelper.PutBlockList(uploadDto.DataFileLocation, request).Result;
            uploadDto.DataFileLocation = pblr.FileLocation;
            uploadDto.MediaStatusId = Gateway.GetMediaStatus("Uploaded").Id;
            var result = Gateway.Update(uploadDto);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/uploads/{id}/blocklist Get Get the uncommitted blocks
        * @apiName GetBlockList
        * @apiVersion 1.0.0
        * @apiGroup Uploads
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam {Number} Id Upload identifier
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
            var uploadResult = Gateway.GetSingle(id);
            if (!uploadResult.IsSuccess)
            {
                return new OkObjectResult(uploadResult);
            }
            var dto = uploadResult.Result;
            var result = UploadsHelper.GetBlockList(orginalFileName, dto.DataFileLocation);
            return new OkObjectResult(result);
        }
    }
}