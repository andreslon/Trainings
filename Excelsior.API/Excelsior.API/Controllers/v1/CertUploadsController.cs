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
    public class CertUploadsController : Controller
    {
        public ICertUploadsGateway Gateway { get; set; }
        public IUploadsHelper UploadsHelper { get; set; }

        public CertUploadsController(ICertUploadsGateway gateway, IUploadsHelper uploadsHelper)
        {
            Gateway = gateway;
            UploadsHelper = uploadsHelper;
        }

        /**
       * @api {get} api/v1/CertUploads Get all CertUploads
       * @apiName GetAll
       * @apiVersion 1.0.0
       * @apiGroup CertUploads
       *
       * @apiHeader (Header) {String} Authorization Authorization Bearer token.
       * @apiHeaderExample Header Example
       *  {
       *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
       *  }
       * @apiParam (Request Parameters) {Number}                                     certUserId=0 CertUser identifier
       * @apiParam (Request Parameters) {Number}                                     certEquipmentId=0 CertEquipment identifier
       * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
       * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
       * @apiParam (Request Parameters) {String}                                     [search] Search text.
       * @apiSuccess {JSON} Result                                                   The paginated array of CertUpload JSON objects.
       * @apiSuccessExample Success-Response
       *  HTTP/1.1 200 OK
                {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": [
                    {
                      "id": 0,
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
        public IActionResult GetAll(long? certUserId, long? certEquipmentId, bool? isActive, int? page, int? pageSize, string filter, string search, string sort)
        {
            var request = new CertUploadsRequestDto
            {
                CertUserId = certUserId,
                CertEquipmentId = certEquipmentId,
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
        * @api {get} api/v1/CertUploads/{id} Get CertUpload by Id
        * @apiName GetSingle   
        * @apiVersion 1.0.0
        * @apiGroup CertUploads
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id CertUpload identifier
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
         * @api {post} api/v1/CertUploads Post CertUpload
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup CertUploads
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of CertUpload
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
        public IActionResult Post([FromBody]CertUploadFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
        * @api {put} api/v1/CertUploads/{id} Put CertUpload
        * @apiName Put
        * @apiVersion 1.0.0
        * @apiGroup CertUploads
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *       "fields": "UploadDate,AcquisitionDate,DataFileLocation,Note,IsActive,SeriesId,UploaderId,MediaStatusId" 
        *  }
        * @apiParam (Request Parameters) {Number}                                  Id CertUpload identifier
        * @apiParam {JSON} Object modified of CertUpload
        * @apiParamExample {json} Request-Example:
                    {
                    }
        * @apiSuccess {JSON} Result The CertUpload JSON object.
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
        public IActionResult Put(int id, [FromBody]CertUploadFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/CertUploads/{id} Delete CertUpload
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup CertUploads
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {Number}                         Id Upload identifier
         * @apiSuccess {JSON} Result The CertUpload JSON object.
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
        * @api {get} api/v1/CertUpload/storage Get Request path blob storage
        * @apiName GetUploadStorageUrl
        * @apiVersion 1.0.0
        * @apiGroup CertUploads
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
        * @api {post} api/v1/CertUploads/{id}/blocklist Post Commit file upload in blocks
        * @apiName PostBlockList
        * @apiVersion 1.0.0
        * @apiGroup CertUploads
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
        * @api {get} api/v1/CertUploads/{id}/blocklist Get Get the uncommitted blocks
        * @apiName GetBlockList
        * @apiVersion 1.0.0
        * @apiGroup CertUploads
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