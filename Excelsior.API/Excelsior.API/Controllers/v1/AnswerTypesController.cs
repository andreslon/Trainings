using Excelsior.Business.DtoEntities.Full;
using Excelsior.Business.DtoEntities.Request;
using Excelsior.Business.Gateways;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Excelsior.API.Controllers.v1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AnswerTypesController : Controller
    {
        public IAnswerTypesGateway Gateway { get; set; }

        public AnswerTypesController(IAnswerTypesGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/AnswerTypes Get all answer types
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Answer Types
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
        * @apiSuccess {JSON} Result                                                   The paginated array of answer types JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                    "Id" : 0,
                                    "Name" : "AnswerType",
                                    "IsActive" : false
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
        public IActionResult GetAll(int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new AnswerTypesRequestDto()
            {
                UserId = userId,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/AnswerTypes/{id} Get answer type by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Answer Types
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Answer type identifier
         * @apiSuccess {JSON} Result The answer type JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "Id" : 0,
                                    "Name" : "Answer Type Name",
                                    "IsActive" : false
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
         * @api {post} api/v1/AnswerTypes Post answer type
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Answer Types
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of LibraryAnswerType
         * @apiParamExample {json} Request-Example:
                            {
                                   "Name" : "Answer Type Name",
                                   "IsActive" : false
                           }
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "Id" : 0,
                                    "Name" : "Answer Type Name",
                                    "IsActive" : false
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
        public IActionResult Post([FromBody]AnswerTypeFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/AnswerTypes/{id} Put answer type
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Answer Types
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name,IsActive"  
         *  }
         * @apiParam (Request Parameters) {Number}   Id=0 Answer type identifier
         * @apiParam {JSON} Object modified of LibraryAnswerType
         * @apiParamExample {json} Request-Example:
                            {
                                   "Name" : "Answer Type Name",
                                   "IsActive" : false
                           }
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "Id" : 0,
                                    "Name" : "Answer Type Name",
                                    "IsActive" : false
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
         *
         */
        [ServiceFilter(typeof(ValidateModelAttribute))]
        [HttpPut("{id}")]
        [Route("{id}")]
        public IActionResult Put(int id, [FromBody]AnswerTypeFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/AnswerTypes/{id} Delete answer type
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Answer Types
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Answer type identifier
         * @apiSuccess {JSON} Result The GradingTemplate JSON object.
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
    }

}
