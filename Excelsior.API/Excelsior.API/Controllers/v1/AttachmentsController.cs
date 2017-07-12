using Excelsior.API.Helpers;
using Excelsior.Business.DtoEntities;
using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AttachmentsController : Controller
    {
        public IAttachmentsGateway Gateway { get; set; }
        public IUploadsHelper UploadsHelper { get; set; }

        public AttachmentsController(IAttachmentsGateway gateway, IUploadsHelper uploadsHelper)
        {
            Gateway = gateway;
            UploadsHelper = uploadsHelper;
        }

        /**
        * @api {get} api/v1/attachments Get attachments
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Attachments
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     studyId=33 Get attachments in study 33.
        * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active trials?
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of attachments JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                 "id": 22,
                                 "name": "EXCELSIOR™ Upload Manual",
                                 "studyId": 33,
                                 "approvalDate": "2016-02-05T06:00:00",
                                 "latestVersion": {
                                 "id": 29,
                                 "version": "1.0",
                                 "fileLocation": "33/attachments/22/1.0/EXCELSIOR™_Upload_Manual.pdf",
                                 "isActive": true,
                                 },
                                 "isActive": false
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
        public IActionResult GetAll(bool? isActive, long seriesId, string laterality, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new AttachementsRequestDto()
            {
                UserId = userId,
                IsActive = isActive,
                Page = page,
                PageSize = pageSize,
                Search = search,
                SeriesId = seriesId,
                Laterality = laterality
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }



        /**
         * @api {get} api/v1/attachments/{id} Get documnet by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Attachments
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Attachment identifier
         * @apiSuccess {JSON} Result The attachment JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result": {
                                      "id": 1,
                                      "name": "Eyekor SOP #100 - Standard Operating Procedure Policy and Form Development Implementation and Maintenance",
                                      "studyId": null,
                                      "approvalDate": "2012-09-03T05:00:00",
                                      "latestVersion": {
                                        "id": 1,
                                        "version": "1.0",
                                        "fileLocation": "Documents/SOPs/Eyekor_SOP_100.pdf",
                                        "documentId": 1,
                                        "isActive": true,
                                        "attachmentFileLocation": null
                                      },
                                      "isActive": false
                                    }
                        }
         *
         */
        [HttpGet("{id}")]
        public IActionResult GetSingle(long id)
        {
            ResultInfo<AttachmentFullDto> result = null;
            result = Gateway.GetSingle(id);
          
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/attachments Post Attachment
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Attachment
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Attachment
         * @apiParamExample {json} Request-Example:
         *     {
	                "approvalDate":"2017-03-16T05:00:00.000Z",
	                "isActive":true,
	                "latestVersion":{"version": "12"},
	                "version":"12",
	                "name":"test",
	                "studyId":36
               }
         * @apiSuccess {JSON} Result The Attachment JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *      {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": {
                    "id": 225,
                    "name": "test",
                    "studyId": 36,
                    "approvalDate": "2017-03-16T05:00:00",
                    "latestVersion": {
                      "id": 237,
                      "version": "12",
                      "fileLocation": "36/Documents/225/12/test.",
                      "documentId": 225,
                      "isActive": true,
                      "attachmentFileLocation": "36/Documents/225/12/test_attachment.",
                      "statusId": 1,
                      "attachemntStatusId": 1
                    },
                    "isActive": true
                  },
                  "pager": null
                }
         * @apiError BadRequest (400) The model state validation JSON object
         * @apiErrorExample {json} Error example:
         *     HTTP/1.1 400 Bad Request
                {
                  "isSuccess": false,
                  "message": "Invalid Model",
                  "exception": "",
                  "result": [
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]AttachmentFullDto request)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var result = Gateway.Add(request, userId);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/attachments/{id} Put Attachment
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Attachment
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Attachment identifier
         * @apiParam {JSON} Object modified of Attachment
         * @apiParamExample {json} Request-Example:
         *     {
	                "approvalDate":"2017-03-16T05:00:00.000Z",
	                "isActive":true,
	                "latestVersion":{"version": "12"},
	                "version":"12",
	                "name":"test",
	                "studyId":36
                }
         * @apiSuccess {JSON} Result The Attachment JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                        "id": 224,
                        "name": "changed nAme",
                        "studyId": 36,
                        "approvalDate": "2018-03-16T05:00:00",
                        "latestVersion": {
                          "id": 236,
                          "version": "4",
                          "fileLocation": "36/Documents/224/4/refsd.pdf",
                          "documentId": 224,
                          "isActive": true,
                          "attachmentFileLocation": "36/Documents/224/4/refsd_attachment.pdf",
                          "statusId": 1,
                          "attachemntStatusId": 1
                        },
                        "isActive": true
                      },
                      "pager": null
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
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]AttachmentFullDto request)
        {
            request.Id = id;
            var result = Gateway.Update(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/attachments/{id} Delete Attachment
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Attachments
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Attachment identifier
         * @apiSuccess {JSON} Result The Attachment JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": true,
                          "Message": "successful"
                          "Exception": "",
                          "Result": true
                        }
         *
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = Gateway.Delete(id);
            return new OkObjectResult(result);
        }

        /**
        * @api {get} api/v1/attachments Get Request path blob storage
        * @apiName GetUploadStorageUrl
        * @apiVersion 1.0.0
        * @apiGroup Attachmnents
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
                         "IsSuccess": true,
                         "Message": "successful"
                         "Exception": "",
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
        * @api {post} api/v1/attachments/{id}/blocklist Post Commit file upload in blocks
        * @apiName SetBlockList
        * @apiVersion 1.0.0
        * @apiGroup Attachments
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
            if (uploadResult.IsSuccess)
            {
                var url = uploadResult.Result.FileLocation;
            }
            if (!uploadResult.IsSuccess)
            {
                return new OkObjectResult(uploadResult);
            }
            var uploadDto = uploadResult.Result;
            var pblr = UploadsHelper.PutBlockList(uploadDto.FileLocation, request).Result;
            uploadDto.FileLocation = pblr.FileLocation;
            var result = Gateway.Update(uploadDto);
            return new OkObjectResult(result);
        }
    }
}
