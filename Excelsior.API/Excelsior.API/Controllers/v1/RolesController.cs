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
    public class RolesController : Controller
    {
        public IRolesGateway Gateway { get; set; }
        public RolesController(IRolesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/roles Get roles
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Roles
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of Roles JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                  "isSuccess": true,
                  "message": "",
                  "exception": "",
                  "result": [
                    {
                      "id": "c6ba7de9-ca99-4662-8388-21b3078aa9cc",
                      "name": "Administrator"
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
        public IActionResult GetAll(int? page, int? pageSize, string search)
        {
            var request = new RolesRequestDto()
            {
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/roles/{id} Get role by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Roles
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Guid}                                                       Id=0 Role identifier
         * @apiSuccess {JSON} Result The role JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                         "id": "c6ba7de9-ca99-4662-8388-21b3078aa9cc",
                         "name": "Administrator"
                      },
                      "pager": null
                    }
         *
         */
        [HttpGet("{id}")]
        public IActionResult GetSingle(long id)
        {
            ResultInfo<RoleFullDto> result = null;
            result = Gateway.GetSingle(id);
          
            return new OkObjectResult(result);
        }

        /**
         * @api {post} api/v1/roles Post Document
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Roles
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of Document
         * @apiParamExample {json} Request-Example:
         *     {
                    "id": "c6ba7de9-ca99-4662-8388-21b3078aa9cc",
                    "name": "Administrator"
                }
         * @apiSuccess {JSON} Result The Role JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                         "id": "c6ba7de9-ca99-4662-8388-21b3078aa9cc",
                         "name": "Administrator"
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
                                  "key": "RoleName",
                                  "ErrorMessage": "The RoleName field is required."
                                }
                  ],
                  "pager": null
                } 

         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPost]
        public IActionResult Post([FromBody]RoleFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/roles/{id} Put Role
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Roles
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "FirstName,LastName,IsActive,RoleId,AffiliationId,JobTitle,Email" 
         *  }
         * @apiParam (Request Parameters) {Guid}                                                       Id=0 Role identifier
         * @apiParam {JSON} Object modified of Role
         * @apiParamExample {json} Request-Example:
         *     {
                    "id": "c6ba7de9-ca99-4662-8388-21b3078aa9cc",
                    "name": "Administrator"
                }
         * @apiSuccess {JSON} Result The Role JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                      "isSuccess": true,
                      "message": "",
                      "exception": "",
                      "result": {
                        "id": "c6ba7de9-ca99-4662-8388-21b3078aa9cc",
                        "name": "Administrator"
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
        public IActionResult Put(Guid id, [FromBody]RoleFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request,fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/roles/{id} Delete Role
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Roles
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Guid}                                                       Id=0 Role identifier
         * @apiSuccess {JSON} Result The Role JSON object.
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
