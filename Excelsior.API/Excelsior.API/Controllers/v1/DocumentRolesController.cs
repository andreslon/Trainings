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
    public class DocumentRolesController : Controller
    {
        public IDocumentRolesGateway Gateway { get; set; }

        public DocumentRolesController(IDocumentRolesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/documentroles Get assigned roles for a document
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup DocumentRoles
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     documentId=1 Document Identifier.
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiSuccess {JSON} Result                                                   The paginated array of documentVersions JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                  "id": 1,
                                  "documentId": 50,
                                  "roleId": "c4fdf120-00e3-43d5-a33c-d2d38edc98cd",
                                  "roleName": "Manager"
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
        public IActionResult GetAll(long documentId, int? page, int? pageSize, string search)
        {
            var request = new DocumentRolesRequestDto()
            {
                DocumentId = documentId,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }



        /**
         * @api {get} api/v1/documentRoles/{id} Get documnet by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup DocumentRoles
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 DocumentRole identifier
         * @apiSuccess {JSON} Result The documentRole JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                          "IsSuccess": false,
                          "Message": "successful"
                          "Exception": "Error",
                          "Result":     {
                                          "id": 1,
                                          "documentId": 50,
                                          "roleId": "c4fdf120-00e3-43d5-a33c-d2d38edc98cd",
                                          "roleName": "Manager"
                                        },
                                      "isActive": false
                                    }
                        }
         *
         */
        [HttpGet("{id}")]
        public IActionResult GetSingle(long id)
        {
            ResultInfo<DocumentRoleFullDto> result = null;
            result = Gateway.GetSingle(id);

            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/documentRoles Post DocumentRole
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup DocumentRoles
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Document
         * @apiParamExample {json} Request-Example:
         *     {
	             "RoleId":"C15347AF-490E-48D9-B655-8090D19CBC0A",
	             "DocumentId": 50
               }
         * @apiSuccess {JSON} Result The DocumentRole JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *      {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": {
                    "id": 6,
                    "documentId": 50,
                    "roleId": "c15347af-490e-48d9-b655-8090d19cbc0a",
                    "roleName": "Grader"
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
        public IActionResult Post([FromBody]DocumentRoleFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/documentRoles/{id} Put DocumentRole
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup DocumentRoles
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 DocumentRole identifier
         * @apiParam {JSON} Object modified of DocumentRole
         * @apiParamExample {json} Request-Example:
         *     {
	             "RoleId":"C15347AF-490E-48D9-B655-8090D19CBC0A",
	             "DocumentId": 15
               }
         * @apiSuccess {JSON} Result The DocumentRole JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                            "id": 17,
                            "documentId": 15,
                            "roleId": "c15347af-490e-48d9-b655-8090d19cbc0a",
                            "name": "Grader"
                        },
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
        public IActionResult Put(long id, [FromBody]DocumentRoleFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/documentRoles/{id} Delete DocumentRole
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup DocumentRoles
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                                       Id=0 DocumentRole identifier
         * @apiSuccess {JSON} Result The DocumentRole JSON object.
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
