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
    public class DocumentsController : Controller
    {
        public IDocumentsGateway Gateway { get; set; }

        public DocumentsController(IDocumentsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/documents Get documents
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Documents
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
        * @apiSuccess {JSON} Result                                                   The paginated array of documents JSON objects.
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
                                 "fileLocation": "33/Documents/22/1.0/EXCELSIOR™_Upload_Manual.pdf",
                                 "documentId": 22,
                                 "isActive": true,
                                 "attachmentFileLocation": "33/Documents/22/1.0/EXCELSIOR™_Upload_Manual_Attachment..docx"
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
        public IActionResult GetAll(long studyId, bool? isActive, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new DocumentsRequestDto()
            {
                UserId = userId,
                IsActive = isActive,
                Page = page,
                PageSize = pageSize,
                Search = search,
                StudyId = studyId,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }



        /**
         * @api {get} api/v1/documents/{id} Get documnet by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Documents
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Document identifier
         * @apiSuccess {JSON} Result The document JSON object.
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
            ResultInfo<DocumentFullDto> result = null;
            result = Gateway.GetSingle(id);
          
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/documents Post Document
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Documents
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Document
         * @apiParamExample {json} Request-Example:
         *     {
	                "approvalDate":"2017-03-16T05:00:00.000Z",
	                "isActive":true,
	                "latestVersion":{"version": "12"},
	                "version":"12",
	                "name":"test",
	                "studyId":36
               }
         * @apiSuccess {JSON} Result The Document JSON object.
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
        public IActionResult Post([FromBody]DocumentFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/documents/{id} Put Document
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Documents
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Document identifier
         * @apiParam {JSON} Object modified of Document
         * @apiParamExample {json} Request-Example:
         *     {
	                "approvalDate":"2017-03-16T05:00:00.000Z",
	                "isActive":true,
	                "latestVersion":{"version": "12"},
	                "version":"12",
	                "name":"test",
	                "studyId":36
                }
         * @apiSuccess {JSON} Result The Document JSON object.
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
        public IActionResult Put(long id, [FromBody]DocumentFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/documents/{id} Delete Document
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Documents
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 Document identifier
         * @apiSuccess {JSON} Result The Document JSON object.
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
    }
}
