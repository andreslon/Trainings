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
    public class CertQuestionsController : Controller
    {
        public ICertQuestionsGateway Gateway { get; set; }

        public CertQuestionsController(ICertQuestionsGateway gateway)
        {
            Gateway = gateway;
        }

        /**
        * @api {get} api/v1/CertQuestions Get all Cert Questions
        * @apiName GetAll
        * @apiVersion 1.0.0
        * @apiGroup Cert Questions
        *
        * @apiHeader (Header) {String} Authorization Authorization Bearer token.
        * @apiHeaderExample Header Example
        *  {
        *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
        *  }
        *
        * @apiParam (Request Parameters) {Number}                                     [procedureId] Procedure Id.
        * @apiParam (Request Parameters) {Number}                                     pageSize=10 Total items per page.
        * @apiParam (Request Parameters) {Number}                                     page=1 Current page.
        * @apiParam (Request Parameters) {String}                                     [search] Search text.
        * @apiSuccess {JSON} Result                                                   The paginated array of Cert Questions JSON objects.
        * @apiSuccessExample Success-Response
        *  HTTP/1.1 200 OK
        *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": [
                               {
                                    "Id" : 0,
                                    "Name" : "",
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
        public IActionResult GetAll(long? procedureId, int? page, int? pageSize, string search)
        {
            string userId = User.Claims.ToList().Find(s => s.Type == "sub").Value;
            var request = new CertQuestionsRequestDto()
            {
                ProcedureId = procedureId,
                UserId = userId,
                Page = page,
                PageSize = pageSize,
                Search = search,
            };

            var result = Gateway.GetAll(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {get} api/v1/CertQuestions/{id} Get Cert Question by Id
         * @apiName GetSingle   
         * @apiVersion 1.0.0
         * @apiGroup Cert Questions
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Cert Question identifier
         * @apiSuccess {JSON} Result The answer type JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "Id" : 0,
                                    "Name" : "",
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
         * @api {post} api/v1/CertQuestions Post Cert Question
         * @apiName Post
         * @apiVersion 1.0.0
         * @apiGroup Cert Questions
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam {JSON} Object created of CertQuestion
         * @apiParamExample {json} Request-Example:
                            {
                                   "name" : "",
                                   "procedureId": 0
                           }
         * @apiSuccess {JSON} Result The CertQuestion JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "id" : 0,
                                    "name" : "",
                                    "procedureId": 0
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
        public IActionResult Post([FromBody]CertQuestionFullDto request)
        {
            var result = Gateway.Add(request);
            return new OkObjectResult(result);
        }

        /**
         * @api {put} api/v1/CertQuestions/{id} Put Cert Question
         * @apiName Put
         * @apiVersion 1.0.0
         * @apiGroup Cert Question
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *       "fields": "Name,IsActive"  
         *  }
         * @apiParam (Request Parameters) {Number}   Id=0 Cert Question identifier
         * @apiParam {JSON} Object modified of CertQuestion
         * @apiParamExample {json} Request-Example:
                            {
                                   "Name" : "",
                                   "procedureId" : 0
                           }
         * @apiSuccess {JSON} Result The CertQuestion JSON object.
         * @apiSuccessExample Success-Response
         *  HTTP/1.1 200 OK
         *       {
                 "IsSuccess": true,
                 "Message": "Successful",
                 "Exception": "Exception Message",
                 "Result": {
                                    "id" : 0,
                                    "name" : "",
                                    "procedureId" : 0
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
        public IActionResult Put(int id, [FromBody]CertQuestionFullDto request, [FromHeader]string fields)
        {
            request.Id = id;
            var result = Gateway.Update(request, fields);
            return new OkObjectResult(result);
        }

        /**
         * @api {delete} api/v1/CertQuestions/{id} Delete Cert Question
         * @apiName Delete
         * @apiVersion 1.0.0
         * @apiGroup Cert Questions
         *
         * @apiHeader (Header) {String} Authorization Authorization Bearer token.
         * @apiHeaderExample Header Example
         *  {
         *      "Authorization": "Bearer FgYYhj9BSd47FI0aXTL8VggVNgJkIilBvBMAKk3y"
         *  }
         * @apiParam (Request Parameters) {Number}                                  Id=0 Cert Question identifier
         * @apiSuccess {JSON} Result success
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