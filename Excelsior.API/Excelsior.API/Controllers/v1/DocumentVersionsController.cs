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
    public class DocumentVersionsController : Controller
    {
        public IDocumentVersionsGateway Gateway { get; set; }
        public IUploadsHelper UploadsHelper { get; set; }
        public DocumentVersionsController(IDocumentVersionsGateway gateway, IUploadsHelper uploadsHelper)
        {
            Gateway = gateway;
            UploadsHelper = uploadsHelper;
        }

        /**
        * @api {get} api/v1/documentVersons Get documentVersons
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup DocumentVersions
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     studyId=33 Get documents in study 33.
        * @apiParam (Request Parameters) {Boolean=true,false}                         isActive=false Get only active trials?
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of DocumentVersons JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": [
                    {
                      "id": 13,
                      "version": "1.1",
                      "fileLocation": "36/Documents/15/1.1/Imaging_Procedure_Manual.pdf",
                      "documentId": 15,
                      "isActive": true,
                      "attachmentFileLocation": "36/Documents/15/1.1/Imaging_Procedure_Manual_Attachment..pdf",
                      "statusId": 3,
                      "attachemntStatusId": 3
                    }
                  ],
                  "pager": {
                    "itemCount": 21,
                    "pageIndex": 1,
                    "pageSize": 10,
                    "pageCount": 3
                  }
                }
        *
        */
        [HttpGet]
        [Route("")]
        public IActionResult GetAll(long studyId, long? documentId, bool? isActive, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new DocumentVersionsRequestDto()
            {
                UserId = userId,
                IsActive = isActive,
                Page = page,
                PageSize = pageSize,
                Search = search,
                StudyId = studyId,
                DocumentId = documentId
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/documentVersions/{id} Get documentVersion by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup DocumentVersions
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 DocumentVersion identifier
         * @apiSuccess {JSON} Result The documentVersion JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                        "id": 236,
                        "version": "4",
                        "fileLocation": "36/Documents/224/4/refsd.pdf",
                        "documentId": 224,
                        "isActive": true,
                        "attachmentFileLocation": "36/Documents/224/4/refsd_attachment.pdf",
                        "statusId": 3,
                        "attachemntStatusId": 3
                      },
                      "pager": null
                    }
         *
         */
        [HttpGet("{id}")]
        public IActionResult GetSingle(long id)
        {
            ResultInfo<DocumentVersionFullDto> result = null;
            result = Gateway.GetSingle(id);
          
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/documentVersions Post Document
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup DocumentVersions
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Document
         * @apiParamExample {json} Request-Example:
         *     {
                    "version": "14",
                    "fileLocation": "36/Documents/224/14/refsd.pdf",
                    "documentId": 30,
                    "isActive": true,
                    "attachmentFileLocation": "36/Documents/224/14/refsd_attachment.pdf"
                }
         * @apiSuccess {JSON} Result The DocumentVersion JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                        "id": 245,
                        "version": "14",
                        "fileLocation": "36/Documents/224/14/refsd.pdf",
                        "documentId": 30,
                        "isActive": false,
                        "attachmentFileLocation": "36/Documents/224/14/refsd_attachment.pdf",
                        "statusId": 1,
                        "attachemntStatusId": 0
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
                                  "key": "DocumentId",
                                  "ErrorMessage": "The DocumentId field is required."
                                }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]DocumentVersionFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/DocumentVersons/{id} Put DocumentVersion
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup DocumentVersons
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "FirstName,LastName,IsActive,RoleId,AffiliationId,JobTitle,Email" 
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 DocumentVersion identifier
         * @apiParam {JSON} Object modified of DocumentVersion
         * @apiParamExample {json} Request-Example:
         *     {
                    "version": "124",
                    "fileLocation": "36/Documents/224/14/refsd.pdf",
                    "documentId": 30,
                    "isActive": true,
                    "attachmentFileLocation": "36/Documents/224/14/refsd_attachment.pdf"
                }
         * @apiSuccess {JSON} Result The DocumentVersion JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                        "id": 236,
                        "version": "124",
                        "fileLocation": "36/Documents/224/14/refsd.pdf",
                        "documentId": 30,
                        "isActive": true,
                        "attachmentFileLocation": "36/Documents/224/14/refsd_attachment.pdf",
                        "statusId": 3,
                        "attachemntStatusId": 3
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
                                  "key": "DocumentId",
                                  "ErrorMessage": "The DocumentId field is required."
                                }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]DocumentVersionFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/DocumentVersons/{id} Delete DocumentVersons
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup DocumentVersons
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 DocumentVersons identifier
         * @apiSuccess {JSON} Result The DocumentVersion JSON object.
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
        * @api {get} api/v1/documentVersions Get Request path blob storage
        * @apiName GetUploadStorageUrl
        * @apiVersion 1.0.0
        * @apiGroup documentVersions
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
        * @api {post} api/v1/documentVersions/{id}/blocklist Post Commit file upload in blocks
        * @apiName SetBlockList
        * @apiVersion 1.0.0
        * @apiGroup DocumentVersions
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
            var url = uploadResult.Result.FileLocation;
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

        /**
        * @api {post} api/v1/documentVersions/{id}/attachment/blocklist Post Commit file upload in blocks
        * @apiName SetAttachmentBlockList
        * @apiVersion 1.0.0
        * @apiGroup DocumentVersions
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
        [Route("{id}/attachment/blocklist")]
        public IActionResult SetAttachmentBlockList(long id, [FromBody]BlockListRequestDto request)
        {
            var uploadResult = Gateway.GetSingleAttachment(id);
            var url = uploadResult.Result.attachemntStatusId;
            if (!uploadResult.IsSuccess)
            {
                return new OkObjectResult(uploadResult);
            }
            var uploadDto = uploadResult.Result;
            var pblr = UploadsHelper.PutBlockList(uploadDto.AttachmentFileLocation, request).Result;
            uploadDto.AttachmentFileLocation = pblr.FileLocation;
            var result = Gateway.UpdateAttachment(uploadDto, null);
            return new OkObjectResult(result);
        }
    }
}
